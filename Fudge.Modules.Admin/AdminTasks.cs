using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Modules.Admin {
    public static class AdminTasks {
        static FudgeDataContext db = new FudgeDataContext();
        public static void UpdatePendingUsers() {
            //get a list of all users awaiting activation
            var pendingUsers = from u in db.Users
                               where u.Status == UserStatus.Pending
                               group u by SqlMethods.DateDiffDay(u.Timestamp, DateTime.UtcNow) into g
                               select new { DaysPending = g.Key, Users = g };


            foreach (var group in pendingUsers) {
                if (group.DaysPending == 3) {
                    SendActivationEmails(group.Users);
                }
                else if(group.DaysPending > 5) {
                    db.Users.DeleteAllOnSubmit(group.Users);
                }
            }
            db.SubmitChanges();
        }

        private static void SendActivationEmails(IGrouping<int, User> iGrouping) {
            
        }

        public static void SendContestReminders() {
            var upcomingContests = from c in db.Contests
                                   where SqlMethods.DateDiffDay(c.StartTime, DateTime.UtcNow) == 1
                                   select c;

            foreach (var contest in upcomingContests) {
                foreach (var u in contest.ContestUsers) {

                }
            }
        }
    }
}
