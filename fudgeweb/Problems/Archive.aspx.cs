using System;
using System.Linq;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;
using System.Data.Linq;
using System.Web.UI.MobileControls;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Web.UI;

public partial class Problems_Archive : FudgePage {
    public Problems_Archive()
        : base(MenuItem.Problems, false) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Problems";
    }

    class ProblemTuple {
        public string Name { get; set; }
        public int ProblemId { get; set; }
        public DateTime Timestamp { get; set; }
        public int Attempts { get; set; }
        public int Solved { get; set; }
        public double Accuracy { get; set; }
    }

    protected void problemsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        FudgeDataContext db = new FudgeDataContext();
        //solved ratio is (solved / total) submissions per problem        
        if (groupBy.SelectedIndex == 0) {
            var problems = from p in db.Problems
                           where p.Visible
                           let attempts = p.Runs.Count(r => r.Status != RunStatus.InternalError)
                           let solved = (from r in p.Runs
                                         where !r.TestRuns.Any() && r.Status == RunStatus.Done
                                         select r).Count()
                           let percent = attempts == 0 ? 0 : (solved / (1.0 * attempts)) * 100
                           orderby p.Timestamp descending
                           select new {
                               Problem = p,
                               Attempts = attempts,
                               Solved = solved,
                               Accuracy = percent
                           };

            if (!Request.IsQueryStringNull("tag")) {
                Tag tag = db.Tags.SingleOrDefault(t => t.UrlName == Request.QueryString["tag"]);
                problems = from p in problems                           
                           where p.Problem.ProblemTags.Any(t => t.TagId == tag.TagId || t.TagId == tag.ParentTagId)
                           select p;
            }

            e.Result = problems;
        }
    }

    protected void pageSize_SelectedIndexChanged(object sender, EventArgs e) {
        //change the page size
        problemPager.PageSize = Int32.Parse(pageSize.SelectedValue);
        problemPager.DataBind();
        problemView.DataBind();
    }

    protected void groupingSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        FudgeDataContext db = new FudgeDataContext();
        if (groupBy.SelectedIndex > 0) {
            var problems = from p in db.Problems
                           where p.Visible
                           let attempts = p.Runs.Count(r => r.Status != RunStatus.InternalError)
                           let solved = (from r in p.Runs
                                         where !r.TestRuns.Any() && r.Status == RunStatus.Done
                                         select r).Count()
                           let percent = attempts == 0 ? 0 : (solved / (1.0 * attempts)) * 100
                           orderby p.Timestamp descending
                           select new {
                               Problem = p,
                               Attempts = attempts,
                               Solved = solved,
                               Accuracy = percent
                           };

            if (groupBy.SelectedIndex == 1) {
                e.Result = from p in problems
                           group p by p.Problem.Source into g
                           orderby g.Key.Name
                           select new { Key = g.Key.Name, Problems = g };
            }
            else if (groupBy.SelectedIndex == 2) {
                //TODO:fix pager for groups
                e.Result = from p in problems.AsEnumerable()
                           let isNew = SqlMethods.DateDiffDay(p.Problem.Timestamp, DateTime.UtcNow) <= 20
                           group p by isNew into g
                           orderby g.First().Problem.Timestamp descending
                           select new { Key = g.Key ? "New" : "Archived", Problems = g };
            }
        }
        else {
            e.Result = new List<Problem>();
        }
    }

    protected void groupBy_SelectedIndexChanged(object sender, EventArgs e) {
        groupView.DataBind();
    }
}
