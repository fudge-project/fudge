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

public partial class Controls_Pager : System.Web.UI.UserControl {
    public string PagerControlID {
        get {
            return Pager.PagedControlID;
        }
        set {
            Pager.PagedControlID = value;
        }
    }

    public int TotalRowCount {
        get {
            return Pager.TotalRowCount;
        }
    }

    public int StartRowIndex {
        get {
            return Pager.StartRowIndex;
        }
    }

    public int PageSize {
        get {
            return Pager.PageSize;
        }
        set {
            Pager.PageSize = value;
        }
    }
    
    public int CurrentPage {
        get {
            return (Pager.StartRowIndex / Pager.PageSize) + 1;
        }
        set {
            Pager.SetPageProperties((value - 1) * Pager.PageSize, Pager.PageSize, true);
        }
    }

    public string QueryStringField {
        get {
            return Pager.QueryStringField;
        }
        set {
            Pager.QueryStringField = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        
    }
}
