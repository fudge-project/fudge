using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Tasks.NewsFeedTask {
    public class NewSnippetsFeed : INewsFeedPool {

        public const int Expiration = 30;        

        public ICollection<NewsFeed> GetFeeds() {
            FudgeDataContext db = new FudgeDataContext();
            List<NewsFeed> feeds = new List<NewsFeed>();

            var snippets = from s in db.CodeSnippets
                           where SqlMethods.DateDiffDay(s.Timestamp, DateTime.UtcNow) <= Expiration
                           select s;

            foreach (var snippet in snippets) {
                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.CodeSnippet,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(snippet.SnippetId),
                    UserId = snippet.UserId,
                    Timestamp = snippet.Timestamp
                });
            }

            return feeds;
        }
    }
}
