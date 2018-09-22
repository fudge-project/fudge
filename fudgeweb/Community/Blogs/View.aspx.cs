using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Community_Blogs_View : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Community_Blogs_View()
        : base(MenuItem.Community, false) {
        VerifyQueryString("name", name => db.Blogs.Any(b => b.UrlName == name), "Blog does not exist!");
        VerifyQueryStringInt("month", m => m >= 1 && m <= 12, "Invalid date range", false);
    }
    
    protected void Page_Load(object sender, EventArgs e) {
        
    }

    protected int GetComments(int topicId) {
        //count all posts without the first post
        Topic topic = db.Topics.SingleOrDefault(t => t.TopicId == topicId);
        return topic.Posts.Count - 1;
    }

    protected Blog Blog {
        get {
            return db.Blogs.SingleOrDefault(b => b.UrlName == Request.QueryString["name"]);
        }
    }

    protected void blogArchiveSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = from t in Blog.Forum.Topics
                   group t by t.Timestamp.ToString("yyyy") into yearGroup
                   select new {
                       Year = yearGroup.Key,
                       Links = from t in yearGroup
                               group t by new {
                                   t.Timestamp.Month,
                                   MonthName = t.Timestamp.ToString("MMMM")
                               } into monthGroup
                               select new {
                                   Month = monthGroup.Key.MonthName,
                                   Link = String.Format("/Community/Blogs/{0}/Archive/{1}/{2}",
                                   Blog.UrlName, yearGroup.Key, monthGroup.Key.Month),
                                   Count = monthGroup.Count()
                               }
                   };
    }

    protected void blogTopicsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        IEnumerable<Topic> topics = Blog.Forum.Topics;

        if (!Request.IsQueryStringNull("year")) {
            topics = topics.Where(t => t.Timestamp.Year == Int32.Parse(Request.QueryString["year"]));
        }
        if (!Request.IsQueryStringNull("month")) {
            topics = topics.Where(t => t.Timestamp.Month == Int32.Parse(Request.QueryString["month"]));
        }

        e.Result = from t in topics
                   orderby t.Timestamp descending
                   select new {
                       t.TopicId,
                       t.Title,
                       t.UserId,
                       Post = t.Posts.Any() ? t.Posts.First().Message : String.Empty,
                       t.Timestamp                       
                   };

    }

    protected void recentPostsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = (from t in Blog.Forum.Topics
                    orderby t.Timestamp descending
                    select new {
                        Link = Html.Link(String.Format("/Community/Blogs/{0}/{1}", Blog.UrlName, t.TopicId), t.Title)
                    }).Take(6);
    }
}
