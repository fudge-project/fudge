using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;

namespace Fudge.Tasks.NewsFeedTask {
    public class NewPostsNewsFeed : INewsFeedPool {

        public const int Expiration = 30;               

        public ICollection<NewsFeed> GetFeeds() {
            FudgeDataContext db = new FudgeDataContext();
            List<NewsFeed> feeds = new List<NewsFeed>();

            var normalPosts = from p in db.Posts
                              where p.Topic.Visible && p.Topic.Forum.Visible && p.Topic.Forum.ForumCategory.Visible &&
                              SqlMethods.DateDiffDay(p.Timestamp, DateTime.UtcNow) <= Expiration
                              select p;

            var problemPosts = from problem in db.Problems
                               from topics in problem.Forum.Topics
                               from post in topics.Posts
                               where SqlMethods.DateDiffDay(post.Timestamp, DateTime.UtcNow) <= Expiration
                               select new { Post = post, Problem = problem };

            var snippetPosts = from snippet in db.CodeSnippets
                               from post in snippet.Topic.Posts
                               where SqlMethods.DateDiffDay(post.Timestamp, DateTime.UtcNow) <= Expiration
                               select new { Post = post, Snippet = snippet };

            foreach (var post in normalPosts) {
                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.ForumPost,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(post.PostId),
                    UserId = post.UserId,
                    Timestamp = post.Timestamp
                });
            }

            foreach (var post in problemPosts) {
                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.ProblemPost,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(post.Post.PostId, post.Problem.ProblemId),
                    UserId = post.Post.UserId,
                    Timestamp = post.Post.Timestamp
                });
            }

            foreach (var post in snippetPosts) {
                feeds.Add(new NewsFeed {
                    Type = NewsFeedType.CodeSnippetPost,
                    Parameters = NewsFeedDescriptor.BuildNewsFeedDescriptorList(post.Post.PostId, post.Snippet.SnippetId),
                    UserId = post.Post.UserId,
                    Timestamp = post.Post.Timestamp
                });
            }
                
            return feeds;
        }
    }
}
