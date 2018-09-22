using System;
using System.Linq;
using Fudge.Framework.Database;
using System.Xml.Linq;

public partial class Problems_Search : FudgePage {
    public Problems_Search()
        : base(MenuItem.Problems, false) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        Form.DefaultButton = buttonSearch.UniqueID;
    }

    protected void buttonSearch_Click(object sender, EventArgs e) {
        problemView.DataBind();
    }

    protected string GetStatementText(object statement) {
        XElement element = statement as XElement;
        return element.Element("description").Value.Truncate(400);
    }

    protected string FormatTags(int problemId) {        
        Problem problem = Problem.GetProblemById(problemId);      
        return problem.ProblemTags.Select(s => Html.LinkToTag(s.Tag.TagId)).Join(" ");
    }

    protected void searchSource_Selecting(object sender, System.Web.UI.WebControls.LinqDataSourceSelectEventArgs e) {
        FudgeDataContext db = new FudgeDataContext();
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

        e.Result = from p in problems
                   where p.Problem.Name.Contains(search.Text) ||
                   p.Problem.ProblemTags.Any(t => t.Tag.Keyword.Contains(search.Text))
                   select p;
    }
}
