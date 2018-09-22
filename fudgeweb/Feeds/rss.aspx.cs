using System;
using System.Linq;
using Fudge.Framework.Database;

public partial class Users_rss : System.Web.UI.Page {
    FudgeDataContext db = new FudgeDataContext();
    protected void Page_Load(object sender, EventArgs e) {
        Response.ContentType = "application/rss+xml";
        if (!Request.IsQueryStringNull("uid")) {
            int id;
            if (Int32.TryParse(Request.QueryString["uid"], out id)) {
                var user = Fudge.Framework.Database.User.GetUserById(Int32.Parse(Request.QueryString["uid"]));
                if (user != null) {
                    Response.Write(user.GetRss());
                }
            }
        }
        else if (!Request.IsQueryStringNull("new_problems")) {
            Response.Write(ProblemExtensions.GetRss());
        }
        else if (!Request.IsQueryStringNull("newsfeed")) {
            Response.Write(NewsFeedExtensions.GetRss());
        }
        else if (!Request.IsQueryStringNull("blog")) {
            int id;
            if (Int32.TryParse(Request.QueryString["blog"], out id)) {
                var blog = db.Blogs.SingleOrDefault(b => b.BlogId == id);
                if (blog != null) {
                    Response.Write(blog.GetRss());
                }
            }
        }
        else if (!Request.IsQueryStringNull("topic")) {
            int id;
            if (Int32.TryParse(Request.QueryString["topic"], out id)) {
                var topic = db.Topics.SingleOrDefault(t => t.TopicId == id);
                if (topic != null) {
                    Response.Write(topic.GetRss());
                }
            }
        }
    }
}
