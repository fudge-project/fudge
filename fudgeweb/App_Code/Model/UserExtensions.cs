using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;


namespace Fudge.Framework.Database {
    public static class UserExtensions {

        public static HashSet<int> OnlineUsers {
            get {
                var users = HttpContext.Current.Application["users"] as HashSet<int>;
                if (users == null) {
                    users = new HashSet<int>();
                    HttpContext.Current.Application["users"] = users;
                }
                return users;
            }
        }

        public static bool IsMemberOf(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var team = Team.GetTeamById(teamId);
            return user.IsPartOf(teamId) && (user.IsAdmin(teamId) || team.GetUserStatus(user.UserId) == TeamUserStatus.Member);
        }

        public static bool IsAdmin(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var team = Team.GetTeamById(teamId);
            return user.IsPartOf(teamId) && team.GetUserStatus(user.UserId) == TeamUserStatus.Admin;
        }

        public static bool IsInvitedTo(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var team = Team.GetTeamById(teamId);
            return user.IsPartOf(teamId) && team.GetUserStatus(user.UserId) == TeamUserStatus.Invited;
        }

        public static bool IsPartOf(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            return db.TeamUsers.Any(u => u.UserId == user.UserId && u.TeamId == teamId);
        }

        public static bool IsBanned(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var team = Team.GetTeamById(teamId);
            return user.IsPartOf(teamId) && team.GetUserStatus(user.UserId) == TeamUserStatus.Banned;
        }

        public static void RequestJoinTeam(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            db.TeamUsers.InsertOnSubmit(new TeamUser {
                Status = TeamUserStatus.Requested,
                TeamId = teamId,
                UserId = user.UserId,
                Title = null
            });
            db.SubmitChanges();
        }

        public static void LeaveTeam(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var tu = (from u in db.TeamUsers
                      where u.UserId == user.UserId
                      && u.TeamId == teamId
                      select u).FirstOrDefault();
            db.TeamUsers.DeleteOnSubmit(tu);
            db.SubmitChanges();

            //unsubscribe from this team
            user.UnSubscribeFrom(Team.GetTeamById(teamId).TopicId);
        }

        public static void AcceptTeamInvite(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var team = Team.GetTeamById(teamId);
            team.ChangeUserStatus(user.UserId, TeamUserStatus.Member);
            //subscribe to this team
            user.SubscribeForReplies(team.TopicId);
        }

        public static void RejectTeamInvite(this User user, int teamId) {
            FudgeDataContext db = new FudgeDataContext();
            var team = Team.GetTeamById(teamId);
            team.ChangeUserStatus(user.UserId, TeamUserStatus.RejectedInvite);
        }

        public static bool IsFriend(this User user, int userId) {
            return user.ApprovedFriends.Any(u => u.UserId == userId);
        }

        public static bool IsRejected(this User user, int userId) {
            return user.RejectedFriends.Any(u => u.UserId == userId);
        }

        public static bool IsPendingApproval(this User user, int userId) {
            var friend = User.GetUserById(userId);
            return friend.PendingFriends.Any(u => u.UserId == user.UserId);
        }

        public static bool IsRegisteredFor(this User user, int contestId) {
            FudgeDataContext db = new FudgeDataContext();
            return db.ContestUsers.Any(cu => cu.UserId == user.UserId && cu.ContestId == contestId);
        }

        /// <summary>
        /// Creates the relation => (FriendId, UserId)
        /// </summary>
        /// <param name="friendId"></param>
        public static void AddFriend(this User user, int friendId) {
            FudgeDataContext db = new FudgeDataContext();

            if (user.IsRejected(friendId)) {
                var friendRelation = db.Friends.SingleOrDefault(f => f.UserId == friendId &&
                    f.FriendId == user.UserId);

                db.Friends.DeleteOnSubmit(friendRelation);
            }

            //add a new friend relation
            db.Friends.InsertOnSubmit(new Friend {
                UserId = user.UserId,
                FriendId = friendId,
                Status = FriendStatus.Pending
            });

            var friend = User.GetUserById(friendId);
            
            //send friend request email
            Email.AddFriendEmail.Send(friend, "Fudge Friend Request", Util.BaseUrl, user.FullName);            

            db.SubmitChanges();
        }

        /// <summary>
        /// Approves relation (Friend, User) and adds (User, Friend)
        /// </summary>
        /// <param name="friendId"></param>
        public static void ApproveFriend(this User user, int friendId) {
            FudgeDataContext db = new FudgeDataContext();

            //approve (FriendId, UserId)
            var friendRelation = db.Friends.Single(f => f.UserId == friendId && f.FriendId == user.UserId);
            friendRelation.Status = FriendStatus.Accepted;

            //add (UserId, FriendId) relation
            db.Friends.InsertOnSubmit(new Friend {
                FriendId = friendId,
                UserId = user.UserId,
                Status = FriendStatus.Accepted
            });

            db.SubmitChanges();
        }

        public static void RejectFriend(this User user, int friendId) {
            FudgeDataContext db = new FudgeDataContext();

            //reject the user
            var friendRelation = db.Friends.Single(f => f.UserId == friendId && f.FriendId == user.UserId);
            friendRelation.Status = FriendStatus.Rejected;

            db.SubmitChanges();
        }

        /// <summary>
        /// Deletes users from each other friend links i.e (Friend,User) and (User,Friend)
        /// </summary>
        /// <param name="friendId"></param>
        public static void RemoveFriend(this User user, int friendId) {
            FudgeDataContext db = new FudgeDataContext();
            
            var userToFriend = db.Friends.Single(f => f.UserId == user.UserId && f.FriendId == friendId);
            var friendToUser = db.Friends.Single(f => f.UserId == friendId && f.FriendId == user.UserId);

            db.Friends.DeleteOnSubmit(userToFriend);
            db.Friends.DeleteOnSubmit(friendToUser);

            db.SubmitChanges();
        }

        public static bool IsSubscribedTo(this User user, int topicId) {
            FudgeDataContext db = new FudgeDataContext();
            return db.TopicSubscriptions.Any(s => s.UserId == user.UserId && s.TopicId == topicId);
        }

        public static void SubscribeForReplies(this User user, int topicId) {
            FudgeDataContext db = new FudgeDataContext();
            db.TopicSubscriptions.InsertOnSubmit(new TopicSubscription {
                TopicId = topicId,
                UserId = user.UserId
            });
            db.SubmitChanges();
        }

        public static void UnSubscribeFrom(this User user, int topicId) {
            FudgeDataContext db = new FudgeDataContext();
            var subscription = db.TopicSubscriptions.SingleOrDefault(s => s.UserId == user.UserId &&
                s.TopicId == topicId);
            db.TopicSubscriptions.DeleteOnSubmit(subscription);
            db.SubmitChanges();
        }

        /// <summary>
        /// Determines if this user solved the problem with specified problemId
        /// </summary>
        /// <param name="problemId">id of the problem</param>
        /// <returns>true if the user solved the problem, false otherwise.</returns>
        public static bool Solved(this User user, int problemId) {
            return user.SolvedProblems.Any(p => p.ProblemId == problemId);
        }

        /// <summary>
        /// RSS for the solved problems
        /// </summary>
        public static string GetRss(this User user) {
            return new XElement("rss",
                            new XAttribute("version", "2.0"),
                            new XElement("channel",
                                new XElement("title", user.DisplayName + " Submissions"),
                                new XElement("link", "http://" + Util.BaseUrl + "/Users/Profile/" + user.UserId),
                                new XElement("description", "Solved problems for " + user.FullName),
                                from p in user.SolvedProblems.AsEnumerable()
                                select new XElement("item",
                                            new XElement("title", p.Name),
                                            new XElement("link", "http://" + Util.BaseUrl + "/Problems/Archive/" + p.UrlName),
                                            new XElement("description",
                                                p.Statement.Element("description").Value.Truncate(250) + "<br/><br/>" +
                                                Html.Link("http://" + Util.BaseUrl + "/Problems/Runs.aspx?pid=" + p.ProblemId + "&uid=" + user.UserId, "Submissions")),
                                            new XElement("pubDate", DateTime.Now)
                                            )
                                )
                ).ToString();

        }

        public static bool SolvedWhenNew(this User user, int problemId) {
            FudgeDataContext db = new FudgeDataContext();

            var problem = Problem.GetProblemById(problemId);
            var solvedRuns = from r in problem.SolvedRuns
                             where r.UserId == user.UserId &&
                             SqlMethods.DateDiffDay(problem.Timestamp.Date, r.Timestamp.Date) <= 20
                             select r;

            return solvedRuns.Any();
        }


        /// <summary>
        /// Converts a utc time to the user's timezone
        /// </summary>
        /// <param name="utc">universal time</param>
        /// <returns></returns>
        public static DateTime ToUserTimezone(this User user, DateTime utc) {
            TimeZoneInfo userTimezone = TimeZoneInfo.FindSystemTimeZoneById(user.Timezone);
            return TimeZoneInfo.ConvertTimeFromUtc(utc, userTimezone);
        }

        //For AutoComplete
        public static IEnumerable<string> GetUsersByName(string name) {
            var db = new FudgeDataContext();
            var users = from u in db.Users
                        let domain = u.School.Domain.Substring(0, u.School.Domain.IndexOf('.'))
                        where (u.FirstName + " " + u.LastName).StartsWith(name)
                        select (u.FirstName + " " + u.LastName) + " (" + domain + ")";
            return users.Take(10);
        }
    }
}