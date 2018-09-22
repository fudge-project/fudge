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

public partial class Controls_PreferenceRadioButton : System.Web.UI.UserControl {
    FudgeDataContext db = new FudgeDataContext();
    public User.UserOptions Option {
        get {
            object o = ViewState["Option"];
            return o != null ? (User.UserOptions)o : default(User.UserOptions);
        }
        set {            
            preference.SelectedIndex = User.IsOptionSet(value) ? 0 : 1;
            ViewState["Option"] = value;
        }
    }

    public User User {
        get {
            return db.Users.SingleOrDefault(u => u.UserId == User.LoggedInUser.UserId);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void preference_SelectedIndexChanged(object sender, EventArgs e) {               
        if (preference.SelectedIndex == 0) {
            User.OptionFlag |= (int)Option;
        }
        else {
            User.OptionFlag &= ~(int)Option;
        }
        db.SubmitChanges();
    }
}
