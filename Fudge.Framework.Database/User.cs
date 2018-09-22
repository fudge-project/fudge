using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data.Linq;
using System.Data.Linq.SqlClient;

namespace Fudge.Framework.Database {
    public partial class User {
        FudgeDataContext db = new FudgeDataContext();

        [Flags]
        public enum UserOptions {
            HideLastName = 1,
            NoNewsletter = 2,
            PlainTextNewsletter = 4,
            NoEmailNotifications = 8,
            AutomaticallySubscribeToMyTopics = 16,
            AutomaticallySubscribeToTopicsIReplyTo = 32
        }

        [Flags]
        public enum UserToolTips {
            NewProblems = 1,
            JavaGotcha = 2
        }

        [Flags]
        public enum UserPermissions {

        }

        public bool IsOptionSet(UserOptions option) {
            return (OptionFlag & option) == option;
        }

        public bool IsTooltipSet(UserToolTips type) {
            return (TooltipFlag & type) == type;
        }

        public void SetOption(UserOptions option) {
            OptionFlag |= option;
        }

        public void UnSetOption(UserOptions option) {
            OptionFlag &= ~option;
        }

        public bool IsLoggedOn {
            get {
                var user = LoggedInUser;
                return user != null && user.UserId == UserId;
            }
        }

        public TimeZoneInfo TimeZoneInfo {
            get {
                return TimeZoneInfo.FindSystemTimeZoneById(Timezone);
            }
        }

        /// <summary>
        /// Gets the current user logged in
        /// </summary>
        public static User LoggedInUser {
            get {
                return HttpContext.Current.Session["user"] as User;
            }
        }

        /// <summary>
        /// The display name for this user
        /// </summary>
        public string DisplayName {
            get {
                return ShortName + " (" + Affiliation.Entity.Domain.Substring(0, Affiliation.Entity.Domain.IndexOf('.')) + ")";
            }
        }

        /// <summary>
        /// Full name of the user
        /// </summary>
        public string FullName {
            get {
                return FirstName + " " + (IsOptionSet(UserOptions.HideLastName) ? LastName[0] + "." : LastName);
            }
        }

        public IQueryable<User> RejectedFriends {
            get {
                return from f in db.Friends
                       where f.FriendId == UserId && f.Status == FriendStatus.Rejected &&
                       f.User.Status == UserStatus.Activated
                       select f.User;
            }
        }

        public IQueryable<User> ApprovedFriends {
            get {
                return from f in db.Friends
                       where f.UserId == UserId && f.Status == FriendStatus.Accepted &&
                       f.User1.Status == UserStatus.Activated
                       select f.User1;
            }
        }

        public IQueryable<User> PendingFriends {
            get {
                return from f in db.Friends
                       where f.FriendId == UserId && f.Status == FriendStatus.Pending &&
                       f.User.Status == UserStatus.Activated
                       select f.User;
            }
        }

        public IQueryable<Run> GoodRuns {
            get {
                //return (from r in Runs
                //        let solved = from p in r.Problem.Runs
                //                     where p.UserId == UserId && !p.TestRuns.Any() && p.Status == RunStatus.Done && SqlMethods.DateDiffDay(p.Problem.Timestamp, p.Timestamp) <= Problem.ExpirationTime
                //                     select p
                //        where solved.Min(p => p.RunId) == r.RunId
                //        select r).AsQueryable();

                var runs = from r in Runs
                           let solvedProblemRuns = from pr in r.Problem.Runs
                                                   where pr.UserId == UserId
                                                   && !pr.TestRuns.Any() && pr.Status == RunStatus.Done
                                                   select pr
                           where !r.TestRuns.Any() && r.Status == RunStatus.Done
                                && SqlMethods.DateDiffDay(r.Problem.Timestamp, r.Timestamp) <= 20
                                && solvedProblemRuns.Min(pr => pr.RunId) == r.RunId
                           select r;

                return runs.AsQueryable();
            }
        }

        /// <summary>
        /// A list of solved problems for this user
        /// </summary>    
        public IQueryable<Problem> SolvedProblems {
            get {
                var solved = (from r in db.Runs
                              where !r.TestRuns.Any() && r.Status == RunStatus.Done &&
                                    r.UserId == UserId && r.Problem.Visible
                              select r.ProblemId).Distinct();

                var problems = from pid in solved
                               select db.Problems.Single(p => p.ProblemId == pid);

                return problems;
            }
        }

        public IQueryable<ProblemStats> ProblemStats {
            get {
                return from p in SolvedProblems
                       let attempts = (from r in db.Runs
                                       where r.ProblemId == p.ProblemId && r.UserId == UserId
                                       select r).Count()
                       let solved = (from r in db.Runs
                                     where r.ProblemId == p.ProblemId &&
                                     r.UserId == UserId &&
                                           !r.TestRuns.Any() && r.Status == RunStatus.Done
                                     select r).Count()
                       let compileError = (from r in db.Runs
                                           where r.ProblemId == p.ProblemId &&
                                                 r.UserId == UserId &&
                                                 r.Status == RunStatus.CompilationError
                                           select r).Count()
                       let wrongAnswer = (from r in db.Runs
                                          where r.ProblemId == p.ProblemId &&
                                                r.UserId == UserId &&
                                                r.TestRuns.Any(tr => tr.Status == TestRunStatus.WrongAnswer)
                                          select r).Count()
                       let languages = (from r in db.Runs
                                        where r.ProblemId == p.ProblemId && r.UserId == UserId
                                        select r.LanguageId).Distinct().Count()
                       let solvedRuns = from r in db.Runs
                                        where !r.TestRuns.Any() && r.Status == RunStatus.Done &&
                                              r.UserId == UserId && r.ProblemId == p.ProblemId
                                        select r
                       let firstSolved = solvedRuns.Min(r => r.RunId)
                       orderby firstSolved descending
                       select new ProblemStats {
                           Problem = p,
                           Attempts = attempts,
                           Solved = solved,
                           WrongAnswer = wrongAnswer,
                           CompileError = compileError,
                           Languages = languages
                       };
            }
        }

        public static User GetUserById(int id) {
            var db = new FudgeDataContext();
            return GetUserByIdCompiled(db, id);
        }

        //compiled queries
        public static Func<FudgeDataContext, int, IQueryable<User>> GetRejectedFriendsCompiled = CompiledQuery.Compile(
            (FudgeDataContext db, int userId) =>
                 from f in db.Friends
                 where f.FriendId == userId && f.Status == FriendStatus.Rejected &&
                 f.User.Status == UserStatus.Activated
                 select f.User
        );

        public static Func<FudgeDataContext, int, IQueryable<User>> GetApprovedFriendsCompiled = CompiledQuery.Compile(
            (FudgeDataContext db, int userId) =>
                from f in db.Friends
                where f.UserId == userId && f.Status == FriendStatus.Accepted &&
                f.User1.Status == UserStatus.Activated
                select f.User1
            );


        public static Func<FudgeDataContext, int, IQueryable<User>> GetPendingFriendsCompiled = CompiledQuery.Compile(
            (FudgeDataContext db, int userId) =>
                from f in db.Friends
                where f.FriendId == userId && f.Status == FriendStatus.Pending &&
                f.User.Status == UserStatus.Activated
                select f.User
        );

        private static Func<FudgeDataContext, int, IQueryable<Problem>> GetSolvedProblemsCompiled = CompiledQuery.Compile(
        (FudgeDataContext db, int userId) =>
            from pid in
                (from r in db.Runs
                 where !r.TestRuns.Any() && r.Status == RunStatus.Done &&
                       r.UserId == userId
                 select r.ProblemId).Distinct()
            select db.Problems.Single(p => p.ProblemId == pid)
            );

        private static Func<FudgeDataContext, int, User> GetUserByIdCompiled =
            CompiledQuery.Compile((FudgeDataContext db, int id) => db.Users.SingleOrDefault(u => u.UserId == id));

        private static Func<FudgeDataContext, int, IQueryable<ProblemStats>> GetProblemStats =
            CompiledQuery.Compile((FudgeDataContext db, int userId) =>
                 from p in
                     (from pid in
                          (from r in db.Runs
                           where !r.TestRuns.Any() && r.Status == RunStatus.Done &&
                                 r.UserId == userId
                           select r.ProblemId).Distinct()
                      select db.Problems.Single(p => p.ProblemId == pid))
                 let attempts = (from r in db.Runs
                                 where r.ProblemId == p.ProblemId && r.UserId == userId
                                 select r).Count()
                 let solved = (from r in db.Runs
                               where r.ProblemId == p.ProblemId &&
                               r.UserId == userId &&
                                     !r.TestRuns.Any() && r.Status == RunStatus.Done
                               select r).Count()
                 let compileError = (from r in db.Runs
                                     where r.ProblemId == p.ProblemId &&
                                           r.Status == RunStatus.CompilationError
                                     select r).Count()
                 let wrongAnswer = (from r in db.Runs
                                    where r.ProblemId == p.ProblemId &&
                                          r.UserId == userId &&
                                          r.TestRuns.Any(tr => tr.Status == TestRunStatus.WrongAnswer)
                                    select r).Count()
                 let languages = (from r in db.Runs
                                  where r.ProblemId == p.ProblemId &&
                                  r.UserId == userId
                                  select r.LanguageId).Distinct().Count()
                 select new ProblemStats {
                     Problem = p,
                     Attempts = attempts,
                     Solved = solved,
                     WrongAnswer = wrongAnswer,
                     CompileError = compileError,
                     Languages = languages
                 }
            );
    }
}
