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
using System.Data.Linq.SqlClient;

public partial class Contests_List : FudgePage {
    public Contests_List()
        : base(MenuItem.Contests, false) {        

    }
    FudgeDataContext db = new FudgeDataContext();
    protected void Page_Load(object sender, EventArgs e) {
        if (!Request.IsQueryStringNull("past")) {
            Title += ".Contests.Past";
        }
        else if (!Request.IsQueryStringNull("active")) {
            Title += ".Contests.Active";
        }
        else if (!Request.IsQueryStringNull("upcoming")) {
            Title += ".Contests.Upcoming";
        }
    }

    public bool IsPast {
        get {
            return !Request.IsQueryStringNull("past");
        }
    }

    public bool IsActive {
        get {
            return !Request.IsQueryStringNull("active");
        }
    }

    public bool IsUpcoming {
        get {
            return !Request.IsQueryStringNull("upcoming");
        }
    }

    protected void contestSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        if (IsPast) {
            e.Result = from c in db.Contests
                       let hasEnded = SqlMethods.DateDiffSecond(c.EndTime, DateTime.UtcNow) > 0
                       where hasEnded
                       select c;
        }
        else if (IsActive) {
            e.Result = from c in db.Contests
                       let hasEnded = SqlMethods.DateDiffSecond(c.EndTime, DateTime.UtcNow) > 0
                       let hasStarted = SqlMethods.DateDiffSecond(c.StartTime, DateTime.UtcNow) > 0
                       where hasStarted && !hasEnded
                       select c;
        }
        else if (IsUpcoming) {
            e.Result = from c in db.Contests
                       let hasStarted = SqlMethods.DateDiffSecond(c.StartTime, DateTime.UtcNow) > 0
                       where !hasStarted
                       select c;
        }
    }
}
