using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Contests_Schedule : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Contests_Schedule()
        : base(MenuItem.Contests, false) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Contests.Schedule";
    }

    protected void contestSchedule_DayRender(object sender, DayRenderEventArgs e) {
        var contests = from c in db.Contests.AsEnumerable()
                       let date = FudgeUser == null ? c.StartTime.Date :
                       FudgeUser.ToUserTimezone(c.StartTime).Date
                       where date == e.Day.Date
                       select c;

        e.Cell.Text = String.Empty;
        HtmlGenericControl div = new HtmlGenericControl("div");
        div.Attributes["class"] = "daytext";
        div.InnerHtml = Html.Link(e.SelectUrl, e.Day.DayNumberText);
        e.Cell.Controls.Add(div);

        if (contests.Any()) {
            e.Cell.CssClass = "event";
        }
    }

    protected void contests_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        DateTime date = contestSchedule.SelectedDate == DateTime.MinValue ? DateTime.UtcNow : contestSchedule.SelectedDate.ToUniversalTime();
        e.Result = from c in db.Contests
                   where c.StartTime.Day == date.Day && c.StartTime.Month == date.Month
                   select c;
    }

    protected void contestSchedule_SelectionChanged(object sender, EventArgs e) {
        selectedContests.DataBind();
    }
}
