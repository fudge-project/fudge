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
using System.IO;

public partial class Controls_SourceView : System.Web.UI.UserControl {
    FudgeDataContext db = new FudgeDataContext();    
    public int? SnippetId {
        get {
            return (int?)ViewState["SnippetId"];
        }
        set {
            var snippet = db.CodeSnippets.Single(c => c.SnippetId == value);
            highlighter.LanguageKey = snippet.Language.SourceId;
            highlighter.Text = snippet.Snippet;
            ViewState["SnippetId"] = value;
        }
    }

    public int? RunId {
        get {
            return (int?)ViewState["RunId"];
        }
        set {
            var run = db.Runs.Single(r => r.RunId == value);
            highlighter.LanguageKey = run.Language.SourceId;
            highlighter.Text = run.Code;
            ViewState["RunId"] = value;
        }
    }    

    public bool EnableScrolling {
        get {
            object o = ViewState["EnableScrolling"];
            return o != null ? (bool)o : false;
        }
        set {
            ViewState["EnableScrolling"] = value;
        }
    }

    public bool ShowBorder {
        get {
            object o = ViewState["ShowBorder"];
            return o != null ? (bool)o : true;
        }
        set {
            ViewState["ShowBorder"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {

    } 
}
