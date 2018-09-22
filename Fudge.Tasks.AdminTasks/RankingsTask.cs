using System;
using Fudge.Modules.TaskManager;

namespace Fudge.Tasks.AdminTasks {
    /// <summary>
    /// Updates fudge points, rankings and standings 
    /// </summary>
    public class RankingsTask : Task {                
        public override string Name {
            get { return "standings"; }
        }

        /// <summary>
        /// Check every minute
        /// </summary>
        public override TimeSpan Interval {
            get { return TimeSpan.FromMinutes(2); }
        }

        /// <summary>
        /// Updates the rankings and fudge points every 15 minutes, and standings every 12 hours
        /// </summary>
        public override void DoWork() {
            Rankings.Update(DateTime.UtcNow, DateTime.Now.Hour % 12 == 0 && DateTime.Now.Minute == 0);
            //Rankings.Update(DateTime.UtcNow, true);
            Log(DateTime.Now + " standings updated");
        }
       
        public override Func<DateTime, bool> Start {
            get { return t => t.Minute % 5 == 0 && t.Second == 0; }
        }
    }
}
