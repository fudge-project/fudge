using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;

namespace Fudge.Tasks.NewsFeedTask {
    interface INewsFeedPool {
        ICollection<NewsFeed> GetFeeds();              
    }
}
