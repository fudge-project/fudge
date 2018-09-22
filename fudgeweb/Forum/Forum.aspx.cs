using System;
using System.Linq;
using Fudge.Framework.Database;

public partial class Community_Forum_Topic_Default : FudgePage {
    protected ForumCategory Category {
        get {
            return db.ForumCategories
                .SingleOrDefault(f => f.CategoryId == Int32.Parse(Request.QueryString["id"]));
        }
    }

    protected string ForumTitle {
        get {
            return Category.Title;
        }
    }

    FudgeDataContext db = new FudgeDataContext();

    public Community_Forum_Topic_Default()
        : base(MenuItem.Community, false) {

        VerifyQueryStringInt("id", id => db.ForumCategories.Any(c => c.CategoryId == id),
        "Requested forum category does not exist!");
    }

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void forumDataSource_Selecting(object sender, System.Web.UI.WebControls.LinqDataSourceSelectEventArgs e) {
        e.Result = from f in db.Forums
                   let lastPostId = f.Topics.Max(t => t.Posts.Where(p => p.Visible).Max(p => p.PostId))
                   where f.Visible && f.CategoryId == Category.CategoryId
                   select new {
                       f.ForumId,
                       TopicCount = f.Topics.Count,
                       PostCount = (from t in f.Topics
                                    from p in t.Posts
                                    select p).Count(),
                       LastPost = (from p in db.Posts
                                   where p.PostId == lastPostId
                                   select new { p.UserId, p.Timestamp }).FirstOrDefault(),
                       f.Title,
                       Date = f.Timestamp
                   };
    }
}
