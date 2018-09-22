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

public partial class Community_Snippets_View : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Community_Snippets_View()
        : base(MenuItem.Community, false) {
        VerifyQueryStringInt("id", id => db.CodeSnippets.Any(c => c.SnippetId == id), "No Snippet exists");
    }

    protected CodeSnippet Snippet {
        get {
            return db.CodeSnippets.SingleOrDefault(c => c.SnippetId == Int32.Parse(Request.QueryString["id"]));
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        source.SnippetId = Snippet.SnippetId;
        snippetComments.TopicId = Snippet.TopicId;
        snippetRating.RatingId = Snippet.RatingId;
    }
}
