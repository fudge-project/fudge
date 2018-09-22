using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Tasks.NewsFeedTask {
    public class NewContestsFeed : INewsFeedPool {

        public const int Expiration = 7;                

        public ICollection<NewsFeed> GetFeeds() {
            FudgeDataContext db = new FudgeDataContext();
            List<NewsFeed> feeds = new List<NewsFeed>();

            var contests = from c in db.Contests
                           let d = SqlMethods.DateDiffDay(DateTime.UtcNow, c.StartTime)
                           where d >= 0 && d <= Expiration && !c.UserId.HasValue                           
                           select c;

            foreach (var contest in contests) {                

                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.NewContest,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(contest.ContestId),
                    UserId = null,
                    Timestamp = contest.StartTime
                });
            }

            return feeds;
        }
    }
}
