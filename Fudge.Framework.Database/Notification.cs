using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Fudge.Framework.Database {
    public partial class Notification {
        public static Notification ShareProblem(int userId, int problemId) {
            return new Notification {
                Link = "http://fudge.fit.edu/Problems/Archive/" + Problem.GetProblemById(problemId).UrlName,
                Text = String.Format("{0} wants to share a problem with you", User.LoggedInUser.FirstName),
                Timestamp = DateTime.UtcNow,
                Type = 0,
                UserId = userId
            };
        }

        public static Notification InviteToTeam(int userId, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var Team = db.Teams.Single(t => t.TeamId == teamId);
            return new Notification {
                Link = "http://fudge.fit.edu/Teams/" + teamId,
                Text = String.Format("You have been invited to join {0}", Team.Name),
                Timestamp = DateTime.UtcNow,
                Type = NotificationType.Default,
                UserId = userId
            };
        }

        public static Notification PromoteToAdmin(int userId, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var Team = db.Teams.Single(t => t.TeamId == teamId);
            return new Notification {
                Link = "http://fudge.fit.edu/Teams/" + teamId,
                Text = String.Format("You have been promted to admin of {0}", Team.Name),
                Timestamp = DateTime.UtcNow,
                Type = NotificationType.Default,
                UserId = userId
            };
        }

        public static Notification DemoteToMember(int userId, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var Team = db.Teams.Single(t => t.TeamId == teamId);
            return new Notification {
                Link = "http://fudge.fit.edu/Teams/" + teamId,
                Text = String.Format("You have been demoted by {0}", User.LoggedInUser.FirstName),
                Timestamp = DateTime.UtcNow,
                Type = NotificationType.Default,
                UserId = userId
            };
        }

        public static Notification StackPost(int userId) {
            return new Notification {
                Link = "http://fudge.fit.edu/Users/Profile",
                Text = String.Format("{0} pushed a comment on your Stack", User.LoggedInUser.FirstName),
                Timestamp = DateTime.UtcNow,
                Type = NotificationType.Default,
                UserId = userId
            };
        }

        public static Notification SourcePost(int runId) {
            FudgeDataContext db = new FudgeDataContext();
            var run = db.Runs.SingleOrDefault(r => r.RunId == runId);
            return new Notification {
                Link = "http://fudge.fit.edu/Problems/SourceView/" + run.RunId,
                Text = String.Format("{0} commented on your solution", User.LoggedInUser.FirstName),
                Timestamp = DateTime.UtcNow,
                Type = NotificationType.Default,
                UserId = run.UserId
            };
        }

        public static Notification TopicReply(int topicId) {
            FudgeDataContext db = new FudgeDataContext();
            var topic = db.Topics.SingleOrDefault(t => t.TopicId == topicId);
            return new Notification {
                Link = "http://fudge.fit.edu/Community/Forum/Posts/" + topicId,
                Text = String.Format("{0} replied your post on {1}", User.LoggedInUser.FirstName, topic.Title),
                Timestamp = DateTime.UtcNow,
                Type = NotificationType.Default,
                UserId = topic.UserId
            };
        }

        public static void Notify(Notification notification) {
            FudgeDataContext db = new FudgeDataContext();
            db.Notifications.InsertOnSubmit(notification);
            db.SubmitChanges();
        }
    }
}
