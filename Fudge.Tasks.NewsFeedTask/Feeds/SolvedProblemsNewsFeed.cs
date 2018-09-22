using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Tasks.NewsFeedTask {
    public class SolvedProblemsNewsFeed : INewsFeedPool {

        public const int Expiration = 30;

        private IQueryable<Run> GetRecentSolutions(FudgeDataContext db) {
            var recent = from r in db.Runs
                         where r.Contest == null && SqlMethods.DateDiffDay(r.Problem.Timestamp, DateTime.UtcNow) <= Problem.ExpirationTime
                               && SqlMethods.DateDiffDay(r.Timestamp, DateTime.UtcNow) <= Expiration
                               && !r.TestRuns.Any() && r.Status == RunStatus.Done
                         select r;

            //recent = from r in recent
            //         group r by r.UserId into byUser
            //         from p in byUser
            //         group p by p.ProblemId into byProblem
            //         from x in byProblem
            //         let m = byProblem.Min(m => m.Code.Length)
            //         where x.Code.Length == m
            //         select x;

            return recent;
        }

        private IQueryable<Run> GetRecentShortestSolutions(FudgeDataContext db, IQueryable<Run> recent) {
            return from p in db.Problems
                   let record = (from r in p.Runs.Except(recent)
                                 where !r.TestRuns.Any() && r.Status == RunStatus.Done
                                 orderby r.Timestamp, r.Size
                                 select r).FirstOrDefault()
                   let current = (from r in recent
                                  where r.ProblemId == p.ProblemId
                                  orderby r.Timestamp, r.Size
                                  select r).FirstOrDefault()
                   where record == null || (current != null && current.Size < record.Size)
                   select current;
        }

        public ICollection<NewsFeed> GetFeeds() {
            FudgeDataContext db = new FudgeDataContext();
            List<NewsFeed> feeds = new List<NewsFeed>();

            var recents = GetRecentSolutions(db);

            foreach (var recent in recents) {
                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.SolvedProblem,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(recent.RunId),
                    UserId = recent.UserId,
                    Timestamp = recent.Timestamp
                });
            }

            var shortests = GetRecentShortestSolutions(db, recents);

            foreach (var shortest in shortests) {
                if (shortest == null) {
                    continue;
                }

                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.ShortestSolution,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(shortest.RunId),
                    UserId = shortest.UserId,
                    Timestamp = shortest.Timestamp
                });
            }

            return feeds;
        }
    }
}
