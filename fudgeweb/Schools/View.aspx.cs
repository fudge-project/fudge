using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Fudge.Framework.Database;
using System.Web.UI.WebControls;
using Extensions;
using WebChart;
using System.Drawing;

public partial class Schools_View : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    protected School School {
        get {
            return db.Schools.SingleOrDefault(s => s.SchoolId == Int32.Parse(Request.QueryString["id"]));
        }
    }

    public Schools_View()
        : base(MenuItem.Community, false) {

        VerifyQueryStringInt("id", id => db.Schools.Any(s => s.SchoolId == id), "The requested school does not exist in out records!");
    }


    protected void Page_Load(object sender, EventArgs e) {
        Title += " - " + School.Name;
        var scriptManager = ScriptManager.GetCurrent(this);
        if (!scriptManager.IsInAsyncPostBack) {
            var schoolStandings = schoolView.FindControl<ChartControl>("schoolStandings");
            if (schoolStandings != null) {
                //generate pie chart of langauges and users
                schoolStandings.Charts[0].DataSource = from u in School.Users
                                                       where u.Language.Visible && u.Status == UserStatus.Activated
                                                       group u by u.Language.Name into g
                                                       select new { Language = g.Key, Count = g.Count() };

                ((PieChart)schoolStandings.Charts[0]).Colors = new[] {  Color.Red,
                                                                    Color.Blue, 
                                                                    Color.Green, 
                                                                    Color.Brown, 
                                                                    Color.GhostWhite, 
                                                                    Color.Gold, Color.SteelBlue,
                                                                    Color.Yellow };

                schoolStandings.Charts[0].DataBind();
                schoolStandings.RedrawChart();

            }
        }
    }

    protected string UserDisplay {
        get {
            int count = School.Users.Where(u => u.Status == UserStatus.Activated).Count();
            return "This school has " + count + (count == 1 ? " user" : " users");
        }
    }

    protected void usersSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = School.Users.Where(u => u.Status == UserStatus.Activated).AsQueryable();
    }

    protected void SchoolUsers_ItemDataBound(object sender, ListViewItemEventArgs e) {
        var miniProfile = e.Item.FindControl<Controls_MiniProfile>("user");
        if (miniProfile != null) {
            miniProfile.Bind();
        }
    }

    protected void postsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = (from u in School.Users
                    from p in u.Posts
                    where p.Topic.Visible
                    orderby p.Timestamp descending
                    select p).Take(10);
    }
}
