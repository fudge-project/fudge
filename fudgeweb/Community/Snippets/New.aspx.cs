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
using Extensions;

public partial class Community_Snippets_New : FudgePage {
    public Community_Snippets_New()
        : base(MenuItem.Community) {        
    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Snippets.New()";                
    }

    protected void snippetForm_ItemInserting(object sender, FormViewInsertEventArgs e) {
        Page.Validate();
        e.Cancel = !Page.IsValid;
        if (!e.Cancel) {
            e.Values["UserId"] = FudgeUser.UserId;
            e.Values["Timestamp"] = DateTime.UtcNow;
            e.Values["RatingId"] = Rating.NewRating();
            e.Values["TopicId"] = Topic.CreateStackTopic("Snippet " + e.Values["Name"]);
        }
    }

    protected void snippetForm_ItemInserted(object sender, FormViewInsertedEventArgs e) {
        snippetTip.Show();
        snippetTip.Text = "Your code snippet has been added! back to " + Html.Link("/Community/Snippets/", "snippets") + ".";
        snippetForm.Visible = false;
    }
}
