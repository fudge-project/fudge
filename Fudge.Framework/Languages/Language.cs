using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using Fudge.Framework.Database;
using System.Text;
using Fudge.Framework.ModuleInterface;
using System.Threading;

namespace Fudge.Framework {
    public class Language {

        private XDocument languageXml;

        public Language(string xmlFile, string fileName) {
            string xml = File.ReadAllText(xmlFile);

            xml = xml.Replace("%FILENAME%", fileName);
            xml = xml.Replace("%DIRECTORYNAME%", Path.GetDirectoryName(fileName));
            xml = xml.Replace("%FILENAMEWITHOUTEXTENSION%", Path.GetFileNameWithoutExtension(fileName));
            xml = xml.Replace("%EXENAME%", Path.ChangeExtension(fileName, "exe"));
            xml = xml.Replace("%SIMPLENAME%", Path.GetFileName(fileName));

            languageXml = XDocument.Parse(xml, LoadOptions.None);
        }

        public void Compile() {
            XElement compileXml = languageXml.Element("language").Element("compile");

            if (compileXml != null) {
                bool errorsOnStdOut = Boolean.Parse(compileXml.Attribute("errors-on-std-out").Value);
                string errors = null;

                Process compiler = new Process();
                compiler.StartInfo = GetDefaultCompiler(compileXml.Element("path").Value,
                                                        compileXml.Element("arguments").Value,
                                                        errorsOnStdOut);

                if (compileXml.Element("working-directory") != null) {
                    compiler.StartInfo.WorkingDirectory = compileXml.Element("working-directory").Value;
                }

                compiler.Start();
                compiler.WaitForExit();

                errors = Framework.FormatError(errorsOnStdOut ? compiler.StandardOutput.ReadToEnd() : compiler.StandardError.ReadToEnd());

                if (!String.IsNullOrEmpty(errors))
                    throw new CompilationErrorException(errors);
            }
        }

        public static ProcessStartInfo GetDefaultCompiler() {
            return GetDefaultCompiler(String.Empty, String.Empty);
        }

        public static ProcessStartInfo GetDefaultCompiler(string fileName, string arguments) {
            return GetDefaultCompiler(fileName, arguments, false);
        }

        public static ProcessStartInfo GetDefaultCompiler(string fileName, string arguments, bool errorsOnStandardOut) {
            ProcessStartInfo compiler = new ProcessStartInfo();
            compiler.FileName = fileName;
            compiler.Arguments = arguments;
            compiler.RedirectStandardOutput = errorsOnStandardOut;
            compiler.RedirectStandardError = !errorsOnStandardOut;
            compiler.UseShellExecute = false;
            compiler.CreateNoWindow = true;

            return compiler;
        }

        public static ProcessStartInfo GetDefaultExecutable() {
            return GetDefaultExecutable(String.Empty);
        }

        public static ProcessStartInfo GetDefaultExecutable(string fileName) {
            ProcessStartInfo executable = new ProcessStartInfo();
            executable.FileName = fileName;
            executable.WorkingDirectory = Path.GetDirectoryName(fileName);

            executable.RedirectStandardOutput = true;
            executable.RedirectStandardInput = true;
            executable.UseShellExecute = false;
            executable.CreateNoWindow = true;

            executable.Domain = Framework.GetDomain();
            executable.UserName = Framework.GetExecUserName();
            executable.Password = Framework.GetExecUserPassword();
            
            executable.LoadUserProfile = false;
            executable.ErrorDialog = false;

            return executable;
        }

        public static ProcessStartInfo GetDefaultInterpreter(string fileName, string arguments) {
            ProcessStartInfo interpreter = GetDefaultExecutable(fileName);

            interpreter.RedirectStandardError = true;
            interpreter.Arguments = arguments;

            return interpreter;
        }

        private ProcessResult NormalizeResult(ProcessResult result, double factor) {
            result.ExecutionTime = (long)(result.ExecutionTime / factor);
            return result;
        }

        public ProcessResult Execute(int memoryLimit, int timeLimit, int outputLimit, string input) {
            XElement runXml = languageXml.Element("language").Element("run");
            ProcessStartInfo runStartInfo = null;

            string type = runXml.Attribute("type").Value.ToString();
            string arguments = String.Empty;
            int extraTime = 0;
            double extraTimeFactor = 1;
            int extraMemory = 0;
           
            if (runXml.Attribute("extra-time") != null) {
                extraTime = Int32.Parse(runXml.Attribute("extra-time").Value);
            }

            if (runXml.Attribute("extra-time-factor") != null) {
                extraTimeFactor = Double.Parse(runXml.Attribute("extra-time-factor").Value);
            }           

            if (runXml.Attribute("extra-memory") != null) {
                extraMemory = Int32.Parse(runXml.Attribute("extra-memory").Value);
            }

            if (runXml.Element("arguments") != null) {
                arguments = runXml.Element("arguments").Value;
            }

            switch (type) {
                case "interpreted": {
                        runStartInfo = GetDefaultInterpreter(runXml.Element("path").Value, arguments);
                    } break;

                case "native": {
                        runStartInfo = GetDefaultExecutable(runXml.Element("path").Value);
                    } break;
            }

            if (runXml.Element("working-directory") != null) {
                runStartInfo.WorkingDirectory = runXml.Element("working-directory").Value;
            }

            return NormalizeResult(Execute(runStartInfo, memoryLimit + extraMemory, (int)(extraTimeFactor * timeLimit), extraTime, outputLimit, input), extraTimeFactor);
        }

        private ProcessResult Execute(ProcessStartInfo processInfo, int memoryLimit, int timeLimit, int extraTime, int outputLimit, string input) {
            Process runner = new Process();
            ProcessResult result = new ProcessResult();
            
            runner.StartInfo = processInfo;

            StringBuilder stdOut = new StringBuilder();
            StringBuilder stdErr = null;

            runner.OutputDataReceived += (sender, e) => { if (e.Data != null) { stdOut.AppendLine(e.Data); } };

            if (runner.StartInfo.RedirectStandardError) {
                stdErr = new StringBuilder();
                runner.ErrorDataReceived += (sender, e) => { if (e.Data != null) { stdErr.AppendLine(e.Data); } };
            }

            try {
                runner.Start();

                int loadTime = 0;
                runner.WaitForExit(extraTime);
                loadTime = runner.TotalProcessorTime.Milliseconds;                

                runner.BeginOutputReadLine();

                if (runner.StartInfo.RedirectStandardError) {
                    runner.BeginErrorReadLine();
                }

                runner.StandardInput.Write(input);
                runner.StandardInput.Close();

                do {
                    if (!runner.HasExited) {
                        result.ExecutionTime = (long)runner.TotalProcessorTime.TotalMilliseconds - loadTime;
                        result.WorkingSet = Math.Max(result.WorkingSet, runner.PeakWorkingSet64);

                        // Avoid processes running for long, also if they are not CPU intensive
                        if ((DateTime.Now - runner.StartTime).TotalMilliseconds > timeLimit * 4) {
                            runner.KillAndWait();
                            throw new TimeLimitExceededException();
                        }

                        if (result.ExecutionTime > timeLimit) {
                            runner.KillAndWait();
                            throw new TimeLimitExceededException();
                        }

                        if (result.WorkingSet > memoryLimit) {
                            runner.KillAndWait();
                            throw new MemoryLimitExceededException();
                        }

                        if (stdOut.Length > outputLimit) {
                            runner.KillAndWait();
                            throw new OutputLimitExceededException();
                        }
                    }
                } while (!runner.WaitForExit(Framework.GetProcessQueryRate()));
            }
            catch (System.ComponentModel.Win32Exception ex) {
                throw new RuntimeErrorException(Framework.FormatError(ex.Message));
            }
            catch (InvalidOperationException) {

            }
            
            runner.WaitForExit();
            runner.CancelOutputRead();
            
            if(runner.StartInfo.RedirectStandardError) {
                runner.CancelErrorRead();
                
                if(!String.IsNullOrEmpty(stdErr.ToString())) {                
                    throw new RuntimeErrorException(Framework.FormatError(stdErr.ToString()));
                }
            }
            
            result.Output = stdOut.ToString();
            
            return result;
        }
    }
}
