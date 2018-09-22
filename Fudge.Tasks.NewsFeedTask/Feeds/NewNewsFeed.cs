using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Tasks.NewsFeedTask {
    public class NewNewsFeed : INewsFeedPool {

        public const int Expiration = 7;
        public static TimeSpan Priority = new TimeSpan(3, 0, 0, 0);        

        public ICollection<NewsFeed> GetFeeds() {
            FudgeDataContext db = new FudgeDataContext();
            List<NewsFeed> feeds = new List<NewsFeed>();

            var newes = from n in db.News
                       where SqlMethods.DateDiffDay(n.Timestamp, DateTime.UtcNow) <= Expiration
                       select n;

            foreach(var news in newes) {
                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.News,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(news.NewsId),
                    UserId = null,
                    Timestamp = news.Timestamp + Priority
                });
            }

            return feeds;
        }
    }
}
