using System;
using System.Linq;
using CookComputing.XmlRpc;
using Fudge.Framework.Database;
using Westwind.WebLog;

namespace Fudge {
    public class FudgeWebLogApi : XmlRpcService, IMetaWeblog {
        private FudgeDataContext db = new FudgeDataContext();

        /// <summary>
        /// Validates and returns a user
        /// </summary>               
        private User ValidateUser(string username, string password) {
            User user = db.Users.SingleOrDefault(u => u.Email == username && u.Password == password.ToMD5());
            if (user == null) {
                throw new XmlRpcException("Invalid login information");
            }
            else if (!user.Blogs.Any()) {
                throw new XmlRpcException(String.Format("User {0} has no blog", username));
            }
            return user;
        }

        private Topic GetBlogPost(string username, string password, string postid) {
            int id;
            if (Int32.TryParse(postid, out id)) {
                User user = ValidateUser(username, password);

                //get the specific topic for this user's blog
                Topic topic = user.Blogs.First().Forum.Topics.SingleOrDefault(p => p.TopicId == id);
                if (topic != null) {
                    return topic;
                }

                throw new XmlRpcException("Post does not exist " + postid);
            }
            throw new XmlRpcException("Post does not exist " + postid);
        }

        #region IMetaWeblog Members

        public bool deletePost(string appKey, string postid, string username, string password, bool publish) {
            Topic topic = GetBlogPost(username, password, postid);

            //we can't delete posts with comments
            if (topic.Posts.Count > 1) {
                return false;
            }

            //delete all posts for this topic
            db.Posts.DeleteAllOnSubmit(db.Posts.Where(p => p.TopicId == topic.TopicId));
            //delete the topic itself
            db.Topics.DeleteOnSubmit(topic);

            db.SubmitChanges();

            return true;
        }

        public bool editPost(string postid, string username, string password, Westwind.WebLog.Post post, bool publish) {
            Topic topic = GetBlogPost(username, password, postid);

            //update the message
            topic.Posts[0].Message = post.description;
            topic.Title = post.title;
            topic.Timestamp = DateTime.UtcNow;

            db.SubmitChanges();
            return true;
        }

        //no categories yet
        public CategoryInfo[] getCategories(object blogid, string username, string password) {
            return new CategoryInfo[] { };
        }

        public Westwind.WebLog.Post getPost(string postid, string username, string password) {
            //get the post for this users blog
            Topic topic = GetBlogPost(username, password, postid);

            //convert post
            return new Westwind.WebLog.Post {
                description = topic.Posts.First().Message,
                title = topic.Title,
                dateCreated = topic.Timestamp,
                userid = topic.User.DisplayName,
                postid = topic.TopicId.ToString()
            };

        }

        public Westwind.WebLog.Post[] getRecentPosts(object blogid, string username, string password, int numberOfPosts) {
            User user = ValidateUser(username, password);
            Blog blog = user.Blogs.First();

            //recent topics
            var topics = from t in blog.Forum.Topics
                         orderby t.Timestamp descending
                         select new Westwind.WebLog.Post {
                             title = t.Title,
                             dateCreated = t.Timestamp,
                             userid = t.User.DisplayName,
                             description = t.Posts.First().Message,
                             postid = t.TopicId.ToString()
                         };

            return topics.Take(numberOfPosts).ToArray();
        }

        public BlogInfo[] getUsersBlogs(string appKey, string username, string password) {
            User user = ValidateUser(username, password);
            //get the blog for this user
            Blog blog = user.Blogs.First();

            //one blog per user
            return new BlogInfo[] { 
                new BlogInfo {
                     blogid = blog.BlogId.ToString(),
                     blogName = blog.Name,
                     url = String.Format("http://{0}/Community/Blogs/{1}", Util.BaseUrl,blog.UrlName)
                }
            };
        }

        //only images supported
        public mediaObjectInfo newMediaObject(object blogid, string username, string password, mediaObject mediaobject) {
            //validate the user
            User user = ValidateUser(username, password);
            
            //create a new picture
            Picture pic = new Picture {
                Title = mediaobject.name,
                Data = mediaobject.bits
            };
            //add the picture to the database
            db.Pictures.InsertOnSubmit(pic);            

            db.SubmitChanges();
            //return the new image with the url
            return new mediaObjectInfo {
                url = "/Images/" + pic.PictureId
            };
        }

        public string newPost(object blogid, string username, string password, Westwind.WebLog.Post post, bool publish) {
            User user = ValidateUser(username, password);
            Blog blog = user.Blogs.First();

            //create the topic
            Topic newTopic = new Topic {
                UserId = user.UserId,
                Visible = false,
                Title = post.title,
                Timestamp = DateTime.UtcNow
            };

            blog.Forum.Topics.Add(newTopic);

            //add the post for this topic
            newTopic.Posts.Add(new Fudge.Framework.Database.Post {
                Message = post.description,
                Title = post.title,
                Visible = publish,
                Timestamp = DateTime.UtcNow,
                Rating = new Rating(),
                UserId = user.UserId
            });
            
            db.SubmitChanges();
            //subscribe to the topic
            user.SubscribeForReplies(newTopic.TopicId);

            return newTopic.TopicId.ToString();
        }

        #endregion
    }

}