using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Modules.TaskManager;
using Fudge.Framework.Database;

namespace Fudge.Tasks.AdminTasks {
    public class ContestPointsTask : Task {
        public override string Name {
            get { return "contest points"; }
        }

        public override TimeSpan Interval {
            get { return TimeSpan.FromMinutes(5); }
        }

        public override void DoWork() {
            FudgeDataContext db = new FudgeDataContext();

            foreach (Contest contest in db.Contests) {                                                
                contest.UpdatePoints();                
            }
        }
    }
}
