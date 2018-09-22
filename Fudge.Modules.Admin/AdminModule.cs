using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.ModuleInterface;
using System.Reflection;
using System.Threading;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Modules.Admin {
    public class AdminModule : IModule {
        #region IModule Members

        public IModuleHost Host { get; set; }

        private static TimeSpan WaitPeriod = new TimeSpan(24, 0, 0);
        private static TimeSpan CheckPeriod = new TimeSpan(0, 15, 0);

        public string Name {
            get { return Assembly.GetExecutingAssembly().GetName().Name; }
        }

        public string Version {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public void Initialize() {
            while (true) {
                if (DateTime.Now.Hour % 12 == 0 && DateTime.Now.Minute == 0) {                                        
                    AdminTasks.SendContestReminders();
                    AdminTasks.UpdatePendingUsers();                    
                    //sleep for a day
                    Thread.Sleep(WaitPeriod);
                }
                //sleep for 15 minutes
                Thread.Sleep(CheckPeriod);
            }
        }

        public void Dispose() {            
        }

        #endregion
    }
}
