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

public partial class Controls_ProblemStatementView : System.Web.UI.UserControl {    
    public int? ProblemId {
        get {
            return (int?)ViewState["ProblemId"];
        }
        set {
            ViewState["ProblemId"] = value;
            Bind();
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        Bind();
    }

    public void Bind() {
        if (ProblemId.HasValue) {
            ProblemXml.DocumentContent = Problem.GetProblemById(ProblemId.Value).Statement.ToString();
        }
    }
}
