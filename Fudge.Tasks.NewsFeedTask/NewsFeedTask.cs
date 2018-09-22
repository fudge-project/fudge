using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Modules.TaskManager;
using Fudge.Framework.Database;

namespace Fudge.Tasks.NewsFeedTask {
    public class NewsFeedTask : Task {

        private const string name = "newsfeed";        

        private List<INewsFeedPool> pools = new List<INewsFeedPool>();

        public override string Name {
            get { return name; }
        }

        public override TimeSpan Interval {
            get { return Task.HalfHour; }
        }

        public NewsFeedTask() {
            pools.Add(new NewContestsFeed());
            pools.Add(new NewNewsFeed());
            pools.Add(new NewPostsNewsFeed());
            pools.Add(new NewProblemsNewsFeed());
            pools.Add(new NewSnippetsFeed());
            pools.Add(new SolvedProblemsNewsFeed());
        }

        public override void DoWork() {
            FudgeDataContext db = new FudgeDataContext();
            int count = 0;

            db.NewsFeeds.DeleteAllOnSubmit(db.NewsFeeds);

            foreach (INewsFeedPool feed in pools) {
                var feeds = feed.GetFeeds();
                count += feeds.Count;

                db.NewsFeeds.InsertAllOnSubmit(feeds);
            }

            db.SubmitChanges();

            Log("Added {0} feeds", count);
        }
    }
}
