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

public partial class Controls_News : System.Web.UI.UserControl {
    FudgeDataContext db = new FudgeDataContext();

    public int? MaxArticles {
        get {
            return (int?)ViewState["MaxArticles"];
        }
        set {
            ViewState["MaxArticles"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void newsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        DataPager Pager = newsList.FindControl<DataPager>("Pager");
        //show 3 pages max
        e.Result = (from n in db.News
                    orderby n.Timestamp descending
                    select n).Take(MaxArticles.HasValue ? MaxArticles.Value : Pager.PageSize * 3);
    }
}
