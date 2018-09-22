using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.ModuleInterface;
using System.Threading;
using System.Reflection;

namespace Fudge.Modules.Standings {
    public class StandingsModule : IModule {

        public IModuleHost Host { get; set; }

        public string Name {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        public string Version {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public void Initialize() {
            while (true) {

                if (DateTime.Now.Minute % 15 == 0) {
                    Rankings.Update(DateTime.Now, DateTime.Now.Hour % 12 == 0 && DateTime.Now.Minute == 0);
                    Console.WriteLine("[standings] " + DateTime.Now + " standings updated");

                    Thread.Sleep(60000);
                }

                Thread.Sleep(30000);
            }
        }

        public void Dispose() {
            
        }
    }
}
