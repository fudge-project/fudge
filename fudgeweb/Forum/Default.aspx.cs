using System;
using System.Linq;
using Fudge.Framework.Database;

public partial class Forum_Default : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Forum_Default()
        : base(MenuItem.Community, false) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        //all visible categories
        Categories.DataSource = from f in db.ForumCategories
                           where f.Visible
                           select f;
        Categories.DataBind();

        //5 latest posts
        latestPosts.DataSource = (from c in db.ForumCategories
                                  from f in c.Forums
                                  from t in f.Topics
                                  where t.Visible
                                  from p in t.Posts                                  
                                  orderby p.Timestamp descending
                                  select p).Take(5);
        latestPosts.DataBind();
    }
}
