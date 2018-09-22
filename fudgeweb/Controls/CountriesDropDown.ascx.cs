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

public partial class Controls_CountryDropDown : System.Web.UI.UserControl {
    public int? SelectedCountryId {
        get {
            return AddDefaultCase && countries.SelectedIndex == 0 ? (int?)null :
                Int32.Parse(countries.SelectedValue);
        }
        set {
            countries.SelectedValue = value.ToString();
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

    protected void countries_DataBound(object sender, EventArgs e) {
        if (AddDefaultCase) {
            countries.Items.Insert(0, new ListItem("All"));
        }
    }    
}
