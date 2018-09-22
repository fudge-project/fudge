using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.ModuleInterface;
using Fudge.Modules.Judge;
using System.Threading;
using Fudge.Framework.Database;
using System.Reflection;

namespace Fudge.Modules.Contests {
    public class ContestsModule : IModule {        

        public IModuleHost Host { get; set; }
        public JudgeModule Judge { get; private set; }
        public FudgeDataContext DataContext { get; private set; }

        public string Name {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        public string Version {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public void Initialize() {

            Console.WriteLine("[contest] Looking for a Judge module...");

            foreach (IModule module in Host.Modules) {
                Judge = module as JudgeModule;

                if (Judge != null) {
                    break;
                }
            }

            if (Judge == null) {
                Console.WriteLine("[contest] There is no Judge module loaded. Contest module will unload.");
                Dispose();
            }
            else {
                Console.WriteLine("[contest] Contest module will use {0} as Judge.", Judge.Name);                

                while (true) {
                    DataContext = new FudgeDataContext();

                    // Get all contests that have ended and are waiting for deferred judging
                    var contests = DataContext.Contests.Where(c => (c.Scoring & ContestScoring.DeferredJudging) == ContestScoring.DeferredJudging
                                                                   && c.EndTime < DateTime.UtcNow
                                                                   && c.Status != ContestStatus.Closed);
                    foreach (Contest contest in contests) {

                        Console.WriteLine("[contest] Judging contest {0}", contest.ContestId);

                        foreach (ContestProblem problem in contest.ContestProblems) {
                            var runs = problem.Problem.Runs.Where(p => p.ContestId == contest.ContestId);

                            Console.WriteLine("[contest] Judging contest problem {0}", problem.Problem.Name);

                            foreach (Run run in runs) {
                                Thread judge = new Thread(Judge.Run);
                                judge.Start(run.RunId);
                            }
                        }

                        contest.Status = ContestStatus.Closed;
                        DataContext.SubmitChanges();                        
                    }

                    Thread.Sleep(30000);
                }
            }
        }

        public void Dispose() {
            
        }
    }
}
