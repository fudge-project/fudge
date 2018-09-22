using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using Fudge.Framework.Database;
using Fudge.Modules.TaskManager;

namespace Fudge.Tasks.AdminTasks {
    /// <summary>
    /// This task manages pending users on the website
    /// </summary>
    public class PendingUsersTask : Task {                
        public override string Name {
            get { return "pending users"; }
        }

        /// <summary>
        /// Run every day
        /// </summary>
        public override TimeSpan Interval {
            get { return Task.Day; }
        }

        /// <summary>
        /// Users that have been pending for 3 days, resend activation emails
        /// Users pending longer than 5 days, delete from the system
        /// </summary>
        public override void DoWork() {
            Log("Checking for pending users...");
            FudgeDataContext db = new FudgeDataContext();
            var pendingUsers = from u in db.Users
                               where u.Status == UserStatus.Pending
                               group u by SqlMethods.DateDiffDay(u.Timestamp, DateTime.UtcNow) into g
                               select new { DaysPending = g.Key, Users = g };

            int deleted = 0, emails = 0;
            foreach (var userGroup in pendingUsers) {
                if (userGroup.DaysPending == 3) {
                    foreach (var u in userGroup.Users) {
                        String body = String.Format(Emails.ActivationEmail, u.FirstName, u.ActivationCode);
                        if (Emails.SendEmail(u, "Fudge Account Activation", body)) {
                            Log("Successfully resent activation email to {0}", u.Email);
                            emails++;
                        }
                        else {
                            //if we failed to send an activation email, its probably a bad address
                            Log("Failed to send activation email to {0}", u.Email);
                            db.Topics.DeleteAllOnSubmit(u.Topics);
                            db.Users.DeleteOnSubmit(u);
                            deleted++;
                        }

                    }
                }
                else if (userGroup.DaysPending > 5) {                    
                    foreach (var user in userGroup.Users) {
                        db.Topics.DeleteAllOnSubmit(user.Topics);
                    }
                    foreach (var user in userGroup.Users) {
                        //delete all friend relations
                        var friendsToDelete = from fr in db.Friends
                                              where fr.UserId == user.UserId ||
                                              fr.FriendId == user.UserId
                                              select fr;
                        db.Friends.DeleteAllOnSubmit(friendsToDelete);
                    }
                    db.Users.DeleteAllOnSubmit(userGroup.Users);
                    deleted += userGroup.Users.Count();                    
                }
            }
            if (deleted > 0) {
                Log("Deleted {0} users", deleted);
                db.SubmitChanges();
            }
            else if(emails > 0) {
                Log("{0} activation emails sent", emails);
            }
            else {
                Log("No users updated");
            }
        }        
    }


}
