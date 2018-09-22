using System;
using System.Linq;
using System.Xml.Linq;
using Fudge.Framework.Database;
using System.Web.UI.WebControls;

public partial class Community_Forum_Topic : FudgePage {
    FudgeDataContext db = new FudgeDataContext();    

    public Community_Forum_Topic()
        : base(MenuItem.Community, false) {

        VerifyQueryStringInt("id", id => db.Forums.Any(f => f.ForumId == id), "Forum not found!");
    }

    private Forum Forum {
        get {
            return db.Forums
                .SingleOrDefault(f => f.ForumId == Int32.Parse(Request.QueryString["id"]));
        }
    }

    protected string ForumTitle {
        get {            
            if (Problem != null) {
                return Forum.Title + new XElement("div",
                            new XAttribute("class", "description"),
                            Html.AsLink("/Problems/Archive/" + Problem.UrlName, "View Problem Statement")).ToString();
            }
            return Forum.Title;
        }
    }


    protected Problem Problem {
        get {
            return Problem.GetProblemByName(Forum.Title);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void topicsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = from t in db.Topics
                   let lastPost = t.Posts.Any() ? (int?)t.Posts.Max(p => p.PostId) : null
                   where t.ForumId == Forum.ForumId && t.Visible
                   select new {
                       t.TopicId,
                       t.User,
                       Replies = Math.Max(0, t.Posts.Count - 1),
                       LastPost = !lastPost.HasValue ? null :
                                      (from p in db.Posts
                                       where p.PostId == lastPost.Value
                                       select new { p.UserId, p.Timestamp }).FirstOrDefault(),
                       t.Title,
                       Date = t.Timestamp
                   };
    }
}
