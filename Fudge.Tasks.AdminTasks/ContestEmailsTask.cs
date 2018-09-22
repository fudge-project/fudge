using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Modules.TaskManager;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Tasks.AdminTasks {
    public class ContestEmailsTask : Task {        
        public override string Name {
            get { return "contest emails"; }
        }

        /// <summary>
        /// Run this task daily
        /// </summary>
        public override TimeSpan Interval {
            get { return Task.Day; }
        }

        public override void DoWork() {
            int reminders = 0;
            FudgeDataContext db = new FudgeDataContext();
            
            var upcomingContests = from c in db.Contests
                                   where SqlMethods.DateDiffDay(DateTime.UtcNow, c.StartTime) == 1
                                   select c;

            Log("{0} upcoming contests", upcomingContests.Count());
            foreach (var contest in upcomingContests) {
                foreach (var user in contest.ContestUsers) {
                    DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(contest.StartTime, user.User.TimeZoneInfo);
                    String body = String.Format(Emails.ContestNotificationEmail, user.User.FirstName,
                        contest.Name, startTime.ToString("f"), user.User.Timezone, contest.UrlName);
                    if (Emails.SendEmail(user.User, "Contest Reminder", body)) {
                        reminders++;
                    }
                }
                Log("{0}/{1} Reminders sent", reminders, contest.ContestUsers.Count);
            }
        }        

    }
}
