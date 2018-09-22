using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Framework.Database {
    public partial class Topic {
        public static int CreateStackTopic(string title) {
            return CreateStackTopic(User.LoggedInUser.UserId, title);
        }

        public static int CreateStackTopic(int userId, string title) {
            FudgeDataContext db = new FudgeDataContext();
            var topic = new Topic {
                ForumId = Forum.WallPostId,
                Status = 0,
                UserId = userId,
                Visible = false,
                Title = title,
                Timestamp = DateTime.UtcNow
            };
            db.Topics.InsertOnSubmit(topic);
            db.SubmitChanges();
            return topic.TopicId;
        }
    }
}
