using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Community_Blogs_Post : FudgePage {
    public Community_Blogs_Post()
        : base(MenuItem.Community, false) {
        VerifyQueryString("name", name => db.Blogs.Any(b => b.UrlName == name), "Blog does not exist!");
    }

    FudgeDataContext db = new FudgeDataContext();
    protected void Page_Load(object sender, EventArgs e) {
        comments.TopicId = Topic.TopicId;        
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
            "key",
            String.Format(@"
digg_url = '{0}';
digg_title = '{1}';
digg_skin = 'compact';           
digg_media = 'news';
digg_topic = 'programming';
", DiggLink, Topic.Title),
            true);
    }

    protected Blog Blog {
        get {
            return db.Blogs.Single(b => b.UrlName == Request.QueryString["name"]);
        }
    }

    protected Topic Topic {
        get {
            return Blog.Forum.Topics.Single(t => t.TopicId == Int32.Parse(Request.QueryString["id"]));
        }
    }

    protected string DiggLink {
        get {
            return String.Format("http://fudge.fit.edu/Community/Blogs/{0}/{1}", Blog.UrlName, Topic.TopicId);
        }
    }

    protected void recentPostsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = (from t in Blog.Forum.Topics
                    orderby t.Timestamp descending
                    select new {                        
                        Link = Html.Link(String.Format("/Community/Blogs/{0}/{1}", Blog.UrlName, t.TopicId), t.Title)
                    }).Take(5);
    }

    protected void blogArchiveSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        FudgeDataContext db = new FudgeDataContext();
        e.Result = from t in Blog.Forum.Topics
                   group t by t.Timestamp.ToString("yyyy") into yearGroup
                   select new {
                       Year = yearGroup.Key,
                       Links = from t in yearGroup
                               group t by new {
                                   t.Timestamp.Month,
                                   MonthName = t.Timestamp.ToString("MMMM")
                               } into yearMonth
                               select new {
                                   Month = yearMonth.Key.MonthName,
                                   Link = String.Format("/Community/Blogs/{0}/Archive/{1}/{2}",
                                   Blog.UrlName, yearGroup.Key, yearMonth.Key.Month),
                                   Count = yearMonth.Count()
                               }
                   };
    }

    protected void tagSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = from t in Blog.BlogTags
                   group t by t.Tag into g
                   select new {
                       Url = "#",
                       Count = g.Count(),
                       g.Key.Keyword
                   };
    }

    protected void CommentPosted(object sender, CommentPostedArgs e) {
        if (!Blog.User.IsLoggedOn) {            
            Email.NotifyBlogPost.Send(Blog.User, FudgeUser.FirstName + " commented on your blog post!",
                FudgeUser.FirstName, e.Post.Topic.Title, e.Post.Message, Blog.UrlName, e.Post.Topic.TopicId);
        }
    }
}
