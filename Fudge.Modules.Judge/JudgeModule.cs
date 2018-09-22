using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fudge.Framework.Database;
using Fudge.Framework.ModuleInterface;
using Fudge.Framework.Network;
using System.Threading;

namespace Fudge.Modules.Judge {
    public class JudgeModule : IModule {
        private const int OutputLimit = 262144;
        private const int MemoryLimit = 16777216;
        private const int TimeLimit = 8192;

        public JudgeModule() {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            Thread.CurrentThread.Abort();
        }

        public void Dispose() {
            //TODO: listener.Stop();
            //todo: make nullable shit david was blabblin about            
        }             

        public void Initialize() {
            Listener<int> listener = new Listener<int>(4, BitConverter.ToInt32);

            listener.ReadValue += new EventHandler<Listener<int>.ReadValueEventArgs>(listener_ReadValue);
            listener.Start(5555);
        }

        private void listener_ReadValue(object sender, Listener<int>.ReadValueEventArgs e) {
            Run(e.value);
        }

        public string Name {
            get { return "Judge"; }
        }

        public void Run(object runId) {
            Run((int)runId);
        }

        public static string FixNewLines(string text) {
            return text.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
        }

        public void Run(int runId) {

            FudgeDataContext db = new FudgeDataContext();            
            var run = db.Runs.SingleOrDefault(r => r.RunId == runId);

            Console.WriteLine("[judge] Received run " + runId); 

            long totalTime = 0;
            long peakWorkingSet = 0;

            if (run != null) {
                run.Status = RunStatus.Compiling;
                db.SubmitChanges();

                int sourceCodeHandle = Host.CreateFile(run.Code);
                int executableHandle = 0;

                try {
                    executableHandle = Host.Compile(run.Language, sourceCodeHandle);
                }
                catch (CompilationErrorException ex) {
                    run.Error = ex.Message;
                    run.Status = RunStatus.CompilationError;

                    Host.DeleteFile(sourceCodeHandle);
                    db.SubmitChanges();

                    Console.WriteLine("[judge] Judged run " + runId);
                    
                    return;
                }

                run.Status = RunStatus.Running;
                db.SubmitChanges();

                run.ExecutionTime = 0;
                run.Memory = 0;

                var testCases = run.Problem.TestCases;
                List<TestRun> testRuns = new List<TestRun>();
                bool stop = false;

                foreach (var testCase in testCases) {
                    if (stop) {
                        break;
                    }

                    TestRun testRun = new TestRun();
                           
                    testRun.TestCaseId = testCase.TestCaseId;
                    testRun.RunId = run.RunId;
                    testRun.Output = String.Empty;            

                    try {                                                
                        ProcessResult result = Host.Execute(executableHandle, MemoryLimit, TimeLimit, OutputLimit, String.Concat(testCase.Input.TrimEnd(null), Environment.NewLine));
            
                        totalTime += result.ExecutionTime;
                        peakWorkingSet = Math.Max(peakWorkingSet, result.WorkingSet);

                        testRun.Output = result.Output.TrimEnd(null);                        

                        if (totalTime > TimeLimit) {
                            throw new TimeLimitExceededException();
                        }

                        if (FixNewLines(testRun.Output) == FixNewLines(testCase.Output)) {
                            testRun.Status = TestRunStatus.Accepted;                            
                        }
                        else {
                            testRun.Status = TestRunStatus.WrongAnswer;
                        }
                    }
                    catch (RuntimeErrorException ex) {
                        testRun.Error = ex.Message;
                        testRun.Status = TestRunStatus.RuntimeError;                                                                       
                    }
                    catch (TimeLimitExceededException) {
                        testRun.Status = TestRunStatus.TimeLimitExceeded;                        
                    }
                    catch (MemoryLimitExceededException) {
                        testRun.Status = TestRunStatus.MemoryLimitExceeded;                        
                    }
                    catch (OutputLimitExceededException) {
                        testRun.Status = TestRunStatus.OutputLimitExceeded;                        
                    }
                    finally {
                        if (testRun.Status == TestRunStatus.Accepted) {
                            run.ExecutionTime = (int)totalTime;
                            run.Memory = (int)peakWorkingSet;
                        }
                        else {                        
                            testRuns.Add(testRun);

                            if (run.Type == RunType.RunUntilFail) {
                                stop = true;
                            }
                        }
                    }
                }
            
                Host.DeleteFile(sourceCodeHandle);

                run.Status = RunStatus.Done;

                db.TestRuns.InsertAllOnSubmit(testRuns);                
                db.SubmitChanges();

                if (testRuns.Count == 0 || run.Type == RunType.RunAll) {
                    db.sp_SubmitAcceptedRun(run.RunId);
                }

                Console.WriteLine("[judge] Judged run " + runId);
            }
            else {
                throw new Exception("Not a valid run");
            }
        }

        public IModuleHost Host { get; set; }
        public string Version { get { return "1.0"; } }
    }
}
