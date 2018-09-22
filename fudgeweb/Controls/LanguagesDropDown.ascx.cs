using System;
using System.Linq;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Controls_LanguagesDropdown : System.Web.UI.UserControl {
    public int? SelectedLanguageId {
        get {
            return AddDefaultCase && languages.SelectedIndex == 0 ? (int?)null :
                Int32.Parse(languages.SelectedValue);
        }
        set {
            if(value.HasValue) {
                FudgeDataContext db = new FudgeDataContext();
                var lang = db.Languages.SingleOrDefault(l => l.LanguageId == value.Value);
                if (lang != null && lang.Visible) {
                    languages.SelectedValue = value.ToString();
                }
            }
        }
    }

    public bool AddDefaultCase {
        get {
            object o = ViewState["AddDefaultCase"];
            return o != null ? (bool)o : false;
        }
        set {
            ViewState["AddDefaultCase"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        
    }

    protected void languages_DataBound(object sender, EventArgs e) {
        if (AddDefaultCase) {
            languages.Items.Insert(0, new ListItem("All"));
        }
    }
    
}
