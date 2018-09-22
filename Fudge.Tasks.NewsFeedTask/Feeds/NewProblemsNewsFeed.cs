using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Tasks.NewsFeedTask {
    public class NewProblemsNewsFeed : INewsFeedPool {

        public static int Expiration = Problem.ExpirationTime;        

        public ICollection<NewsFeed> GetFeeds() {
            FudgeDataContext db = new FudgeDataContext();
            List<NewsFeed> feeds = new List<NewsFeed>();

            var problems = from p in db.Problems
                           where p.Visible
                           where SqlMethods.DateDiffDay(p.Timestamp, DateTime.UtcNow) <= Expiration                           
                           select p;

            foreach (var problem in problems) {
                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.NewProblem,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(problem.ProblemId),
                    UserId = null,
                    Timestamp = problem.Timestamp
                });                
            }

            return feeds;
        }
    }
}
