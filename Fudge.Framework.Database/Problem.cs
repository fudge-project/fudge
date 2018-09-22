using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Xml.Linq;
using System.Data.Linq.Mapping;
using Fudge.Framework;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Framework.Database {
    /// <summary>
    /// Summary description for Problem
    /// </summary>
    public partial class Problem {

        public const int ExpirationTime = 20;

        FudgeDataContext db = new FudgeDataContext();
        /// <summary>
        /// The accuracy rate of this problem
        /// </summary>
        public double Accuracy {
            get {
                int attempts = Attempts;
                return attempts == 0 ? 0 : (Solved / (double)attempts) * 100.0;
            }
        }

        /// <summary>
        /// The number of users that solved this problem
        /// </summary>    
        public int Solved {
            get {
                return GetSolvedRuns(db, ProblemId).Count();
            }
        }

        /// <summary>
        /// The number of users that attempted this problem
        /// </summary>
        public int Attempts {
            get {
                return Runs.Count;
            }
        }

        /// <summary>
        /// The list of users who solved this problem
        /// </summary>
        public IQueryable<User> SolvedUsers {
            get {
                var users = (from r in SolvedRuns
                             select r.User).Distinct();
                return users;
            }
        }

        public IQueryable<Run> SolvedRuns {
            get {
                var runs = from r in db.Runs
                           where r.Status == RunStatus.Done && !r.TestRuns.Any()
                           && r.ProblemId == ProblemId
                           select r;
                return runs;
            }
        }

        /// <summary>
        /// If the problem is less than 3 weeks old it is considered new
        /// </summary>
        public bool IsNew {
            get {
                return SqlMethods.DateDiffDay(Timestamp, DateTime.UtcNow) <= 20;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsArchived {
            get {
                return !IsNew;
            }
        }

        /// <summary>
        /// Gets a problem by id or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Problem GetProblemById(int id) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Problems.SingleOrDefault(p => p.ProblemId == id);
        }

        public static Problem GetProblemByUrlName(string urlName) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Problems.SingleOrDefault(p => p.UrlName == urlName);
        }

        //locates problems with
        public static IQueryable<string> GetProblemsByName(string name) {
            FudgeDataContext db = new FudgeDataContext();
            return GetProblemsByNameCompiled(db, name);
        }

        public static Problem GetProblemByName(string name) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Problems.SingleOrDefault(p => p.Name == name);
        }

        //commonly used queries are compiled
        private static Func<FudgeDataContext, string, IQueryable<string>> GetProblemsByNameCompiled =
            CompiledQuery.Compile((FudgeDataContext db, string name) =>
                db.Problems.Where(p => p.Name.StartsWith(name)).Select(p => p.Name));

        private static Func<FudgeDataContext, int, IQueryable<Run>> GetRunsForProblem =
            CompiledQuery.Compile((FudgeDataContext db, int pid) =>
                db.Runs.Where(r => r.ProblemId == pid).Select(r => r));

        private static Func<FudgeDataContext, int, IQueryable<User>> GetSolvedUsers =
            CompiledQuery.Compile((FudgeDataContext db, int pid) => (from r in db.Runs
                                                                     where !r.TestRuns.Any() && r.Status == RunStatus.Done
                                                                           && r.ProblemId == pid
                                                                     select r.User).Distinct());

        private static Func<FudgeDataContext, int, IQueryable<Run>> GetSolvedRuns =
           CompiledQuery.Compile((FudgeDataContext db, int pid) =>
               db.Runs.Where(r => !r.TestRuns.Any() && r.Status == RunStatus.Done)
               .Where(r => r.ProblemId == pid));        
        
    }
}