using System.Linq;
using System.Data.Linq;
using System;
using Fudge.Framework;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Framework.Database {

    public partial class Run : IRateable {
        public const int ForumId = 7;

        public const int NormalRunPoints = 6;
        public const int DeferredContestRunPoints = 30;
        public const int ImmediateContestRunPoints = 15;
        
        public bool Solved {
            get {
                return !TestRuns.Any() && Status == RunStatus.Done;
            }
        }

        public bool ProblemAndUserInContest {
            get {
                FudgeDataContext db = new FudgeDataContext();

                var contests = db.ContestProblems.Where(cp => cp.ProblemId == ProblemId);
                return db.ContestUsers.Any(cu => cu.UserId == UserId && contests.Any(c => c.ContestId == cu.ContestId));
            }
        }

        public int AvgPoints {
            get {
                if (Contest == null) {
                    if (ProblemAndUserInContest) {
                        return 0;
                    }

                    return NormalRunPoints;
                }
                else {
                    if (Points.HasValue) {
                        return Points.Value;
                    }

                    return 0;
                }
            }
        }

        public TestRunStatus TestRunStatus {
            get {
                return Solved ? TestRunStatus.Accepted : TestRuns.First().Status;
            }
        }

        public static IQueryable<Run> GetSolvedRuns() {
            FudgeDataContext db = new FudgeDataContext();
            return GetSolvedRunsCompiled(db);
        }

        private static Func<FudgeDataContext, IQueryable<Run>> GetSolvedRunsCompiled =
            CompiledQuery.Compile((FudgeDataContext db) => from r in db.Runs
                                                           where !r.TestRuns.Any() && r.Status == RunStatus.Done
                                                           select r
            );

        public static Run GetRunById(int id) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Runs.SingleOrDefault(r => r.RunId == id);
        }

        public void UpdatePoints() {
            if (Contest == null) {
                return;
            }

            FudgeDataContext db = new FudgeDataContext();
            int runPoints = Contest.IsScoringSet(ContestScoring.DeferredJudging) ? DeferredContestRunPoints : ImmediateContestRunPoints;
            Run run = db.Runs.SingleOrDefault(r => r.RunId == RunId);

            if (db.Runs.Any(r => r.UserId == UserId && r.ContestId == ContestId && r.ProblemId == ProblemId && r.Timestamp > Timestamp)) {
                run.Points = 0;
            }
            else {
                if (Contest.IsScoringSet(ContestScoring.TestCaseScoring)) {
                    int attempted = db.Runs.Count(r => r.ContestId == ContestId && r.ProblemId == ProblemId);
                    double avgPoints = runPoints / (double)db.TestCases.Count(tc => tc.ProblemId == ProblemId);

                    var totalFailedTestCases = from tr in db.TestRuns
                                               where tr.Run.ContestId == ContestId && tr.Run.ProblemId == ProblemId
                                               group tr by tr.TestCaseId into g
                                               select new { TestcaseId = g.Key, Failed = g.Count() };

                    var userFailedTestCases = from tr in db.TestRuns
                                              where tr.Run.ContestId == ContestId && tr.Run.ProblemId == ProblemId && tr.Run.Timestamp < Timestamp && tr.Run.UserId == UserId
                                              group tr by tr.TestCaseId into g
                                              select new { TestcaseId = g.Key, Failed = g.Count() };

                    var totalTestCases = from tc in db.TestCases
                                         where tc.ProblemId == ProblemId
                                         let g = totalFailedTestCases.SingleOrDefault(g => g.TestcaseId == tc.TestCaseId)
                                         let failed = g == null ? 0 : g.Failed
                                         let accepted = attempted - failed
                                         let ratio = Math.Max(0.10, accepted == 0 ? 0 : 1 - accepted / (double)attempted)
                                         select new { tc.TestCaseId, Ratio = ratio };

                    var userTestCases = from tc in db.TestCases
                                        where tc.ProblemId == ProblemId
                                        let g = userFailedTestCases.SingleOrDefault(g => g.TestcaseId == tc.TestCaseId)
                                        let solved = !db.TestRuns.Any(tr => tr.RunId == RunId && tr.TestCaseId == tc.TestCaseId)
                                        let failed = g == null ? 0 : g.Failed
                                        select new { tc.TestCaseId, Failed = failed, Solved = solved };

                    var testCases = from tc in totalTestCases
                                    join uc in userTestCases on tc.TestCaseId equals uc.TestCaseId
                                    select new { Points = uc.Solved ? avgPoints * tc.Ratio * Math.Pow(0.99, uc.Failed) : 0 };

                    run.Points = (int)Math.Min(10, testCases.Sum(s => s.Points));
                }
                else {
                    if (!Solved) {
                        run.Points = 0;
                    }
                    else {
                        int wrong = db.Runs.Count(r => r.ContestId == ContestId && r.ProblemId == ProblemId && r.UserId == UserId && r.Timestamp < Timestamp);
                        int solved = db.Runs.Count(r => r.ContestId == ContestId && r.ProblemId == ProblemId && r.Status == RunStatus.Done && !r.TestRuns.Any() && r.Timestamp < Timestamp);

                        run.Points = (int)Math.Max(3, runPoints * Math.Pow(0.90, wrong + solved));
                    }
                }
            }

            db.SubmitChanges();            
        }
    }
}