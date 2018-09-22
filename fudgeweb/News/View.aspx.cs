using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fudge.Framework.Database;

public partial class News_View : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public News_View()
        : base(MenuItem.News, false) {
        VerifyQueryString("name", name => db.News.Any(n => n.UrlName == name), "News article does not exist!");
    }

    public News News {
        get {
            return db.News.SingleOrDefault(n => n.UrlName == Request.QueryString["name"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {

    }
}
