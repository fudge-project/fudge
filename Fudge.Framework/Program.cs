using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Reflection;
using Fudge.Framework.ModuleInterface;
using System.Diagnostics;
using System.Security;
using Fudge.Framework.Database;

//todo: define eventlog categories and events

namespace Fudge.Framework {
    class Program {

        static Mutex singleInstanceMutex;
        static ModuleServices moduleServices;

        static void Main(string[] args) {

            bool createdNew;
            singleInstanceMutex = new Mutex(true, "FudgeMutex", out createdNew);

            if (!createdNew) {
                return;
            }

            GC.KeepAlive(singleInstanceMutex);

            if (!EventLog.SourceExists("Fudge Framework")) {
                EventLog.CreateEventSource(new EventSourceCreationData("Fudge Framework", "Fudge"));
            }

            EventLog.WriteEntry("Fudge Framework", "Framework started", EventLogEntryType.Information, 0, 0);

            moduleServices = new ModuleServices();

            FileSystemWatcher moduleWatcher = new FileSystemWatcher(Framework.GetModuleDirectory(), "*.dll");
            moduleWatcher.EnableRaisingEvents = true;
            moduleWatcher.Created += new FileSystemEventHandler(moduleWatcher_Created);
            moduleWatcher.IncludeSubdirectories = false;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            
            Console.WriteLine("[fx] Fudge Framework v{0} started", Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("[fx] Module directory is {0}", Framework.GetModuleDirectory());
            Console.WriteLine("[fx] Clearing temp directory ({0})", Framework.GetTempDirectory());

            try {
                foreach (string directory in Directory.GetDirectories(Framework.GetTempDirectory())) {
                    Directory.Delete(directory, true);
                }

                foreach (string file in Directory.GetFiles(Framework.GetTempDirectory())) {
                    File.Delete(file);
                }
            }

            catch {
                Console.WriteLine("[fx] Could not clear temp directory");
            }

            foreach (string file in Directory.GetFiles(Framework.GetModuleDirectory())) {
                if (Path.GetExtension(file) == ".dll") {
                    LoadModule(file);                        
                }
            }

            if (moduleServices.Modules.Count == 0) {
                Console.WriteLine("[fx] No modules loaded! Framework will exit.");
            }
            else {
                foreach (IModule module in moduleServices.Modules) {
                    InitializeModule(module);
                }
            }
        }

        static void InitializeModule(IModule module) {
            Console.WriteLine("[fx] Initializing module {0}", module.Name);

            new Thread(new ThreadStart(() => {
                try {
                    module.Initialize();
                }
                catch (Exception ex) {
                    Console.WriteLine("[fx] {0} failed: {1}", module.Name, ex.InnerException != null ? ex.InnerException.StackTrace : ex.StackTrace);
                }
            })).Start();
        }

        static IModule LoadModule(string fileName) {
            Assembly moduleAssembly = Assembly.Load(File.ReadAllBytes(fileName));            
            //Assembly moduleAssembly = Assembly.LoadFile(fileName);     

            foreach (Type moduleType in moduleAssembly.GetTypes().Where(t => t.IsPublic)) {
                if (typeof(IModule).IsAssignableFrom(moduleType)) {
                    try {
                        IModule module = (IModule)Activator.CreateInstance(moduleType);

                        module.Host = moduleServices;
                        moduleServices.Modules.Add(module);

                        Console.WriteLine("[fx] Loaded module {0} v{1}", module.Name, module.Version);

                        return module;
                    }
                    catch (Exception ex) {
                        Console.WriteLine("[fx] Failed to load: {0} ({1})", Path.GetFileName(fileName), ex.InnerException != null ? ex.InnerException.Message : ex.Message);          
                    }
                }
            }

            return null;
        }

        static void moduleWatcher_Created(object sender, FileSystemEventArgs e) {
            if (e.ChangeType == WatcherChangeTypes.Created) {
                InitializeModule(LoadModule(e.FullPath));
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {

            try {
                FudgeDataContext db = new FudgeDataContext();
                var runs = db.Runs.Where(r => r.Status != RunStatus.Done && r.Status != RunStatus.CompilationError);

                foreach (Run run in runs) {
                    run.Status = RunStatus.InternalError;
                }

                db.SubmitChanges();
            }
            catch (Exception) {
                EventLog.WriteEntry("Fudge Framework", "Database exception in unhandled exception handler", EventLogEntryType.Error, 1, 1);
            }

            EventLog.WriteEntry("Fudge Framework", e.ExceptionObject.ToString(), EventLogEntryType.Error, 1, 1);

            ProcessStartInfo restarter = new ProcessStartInfo(Assembly.GetExecutingAssembly().GetName().Name + ".exe");

            restarter.Domain = "MILK";
            restarter.UserName = "FUDGE_FX_COMPILER";

            restarter.Password = new SecureString();
            restarter.Password.Append("fxcompiler");

            restarter.UseShellExecute = false;
            restarter.CreateNoWindow = false;
            restarter.ErrorDialog = true;

            EventLog.WriteEntry("Fudge Framework", "Attempting to restart " + restarter.FileName, EventLogEntryType.Information, 2, 0);

            Process.Start(restarter);
        }
    }
}
