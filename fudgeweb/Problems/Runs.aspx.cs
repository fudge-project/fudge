using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Fudge.Framework;
using Fudge.Framework.Database;
using Resources;

public partial class Problems_Runs : FudgePage {
    private const int PageSize = 15;

    //css styles for content
    private static XAttribute BadRunStyle = new XAttribute("class", "badrun");
    private static XAttribute AcceptedRunStyle = new XAttribute("class", "accepted");
    public static XAttribute InteralErrorStyle = new XAttribute("style", "font-weight:bold;color:black");

    public Problems_Runs()
        : base(MenuItem.Problems, false) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        //enable page methods
        ScriptManager.GetCurrent(this).EnablePageMethods = true;

        Page.ClientScript.RegisterStartupScript(typeof(Page), "getrunspolling", "initPolling();", true);

        Title += ".Submissions";
    }

    public string PageTitle {
        get {
            return "Submissions";
        }
    }

    public class PageState {
        public int? UserId { get; set; }
        public int? ProblemId { get; set; }
        public int? LanguageId { get; set; }
        public string SortExpression { get; set; }
        public int SortDirection { get; set; }
        public int CurrentPage { get; set; }
    }

    [WebMethod]
    public static object GetRunTable(PageState state) {

        //build the query string      
        string queryString = Util.CreateQueryString(new Dictionary<string, object> { 
                                    { "pid", state.ProblemId }, 
                                    { "uid", state.UserId }
                                }
                             );

        var db = new FudgeDataContext();

        //filter the runs based on the state
        var filteredRuns = (from r in db.Runs
                            where r.ProblemId == (state.ProblemId ?? r.ProblemId) &&
                                  r.LanguageId == (state.LanguageId ?? r.LanguageId) &&
                                  r.UserId == (state.UserId ?? r.UserId) &&
                                  !r.ContestId.HasValue
                            select r);

        int recordSize = filteredRuns.Count();

        //generate the paging UI
        var links = Html.CreateNumericPagerLinks(PageSize, state.CurrentPage, recordSize, "javascript:selectPage({0})");

        if (String.IsNullOrEmpty(state.SortExpression)) {
            //default sort is by runId
            state.SortExpression = "RunId";
            state.SortDirection = 1;
        }

        //force get records on this page
        var runs = filteredRuns.SortBy(state.SortExpression, (SortDirection)state.SortDirection).
                    ToPagedList(state.CurrentPage - 1, PageSize);

        if (!runs.Any()) {
            return new XElement("div",
                        new XAttribute("class", "error"),
                        new XText("There are no submissions!")).ToString();
        }

        //create list of errors as html elements, passing by javascript is inefficient        
        var errors = (from r in runs.AsEnumerable()
                      let hasCompileError = r.Status == RunStatus.CompilationError
                      let hasRuntimeError = !r.TestRuns.Any() ? false : r.TestRunStatus == TestRunStatus.RuntimeError
                      let errorIsVisible = hasCompileError || (hasRuntimeError && r.Problem.IsArchived)
                      where errorIsVisible
                      let error = r.Error == null ? String.Empty : FormatHelper.FormatError(r.Error.Trim())
                      let errorDiv = String.Format(@"<div id=""{0}_error"" style=""display:none"">{1}</div>", r.RunId, error)
                      select errorDiv).Join();
        errors = errors.Replace("\r\n", "<br />");

        return
            new XElement("table",
               new XElement("tr",
               new XElement("th",
                   Html.AsLink("javascript:sortBy('Problem')", "Problem")),
               new XElement("th",
                   Html.AsLink("javascript:sortBy('User')", "User")),
               new XElement("th",
                   Html.AsLink("javascript:sortBy('Language')", "Language")),
               new XElement("th",
                   new XText("Source")),
               new XElement("th",
                   new XText("Status")),
               new XElement("th",
                   new XText("Submitted")),
               new XElement("th",
                   new XText("Memory(kb)")),
               new XElement("th",
                   new XText("Time(ms)")),
               new XElement("th",
                   new XText("Test Cases"))
               ),
               from r in runs.AsEnumerable()
               let showSource = r.Problem.IsArchived || r.User.IsLoggedOn
               let solvedCases = r.TestRuns.Any() ?
                                 r.Problem.TestCases.TakeWhile(tc => tc.TestCaseId != r.TestRuns.First().TestCaseId).Count() :
                                 r.Status == RunStatus.Done ? r.Problem.TestCases.Count : 0
               let testCaseText = solvedCases + "/" + r.Problem.TestCases.Count
               let sourceSize = String.Format("{0:0.00} kB", r.Size / 1024.0)
               let languageQueryString = String.IsNullOrEmpty(queryString) ?
                                        "?lid=" + r.LanguageId :
                                        queryString + "&lid=" + r.LanguageId
               select new XElement("tr",
                           new XElement("td",
                               Html.AsLink("/Problems/Archive/" + r.Problem.UrlName, r.Problem.Name)),
                           new XElement("td",
                               Html.AsLink("/Users/Profile/" + r.UserId, r.User.DisplayName)),
                           new XElement("td",
                               Html.AsLink("Runs.aspx" + languageQueryString, r.Language.Name)),
                           new XElement("td",
                               !showSource ? sourceSize.ToXElement() : Html.AsLink("SourceView/" + r.RunId, sourceSize)),
                           new XElement("td",
                               CreateProgressBar(GetStatusForRun(r), "#FFFFFF", 150, "#CCC", "#CCC", GetProgressColor(r), (int)(r.Status == RunStatus.CompilationError ? RunStatus.Done : r.Status), 4)),
                           new XElement("td",
                               new XText(FormatHelper.FormatDateTime(r.Timestamp))),
                           new XElement("td",
                               r.Memory.HasValue ? String.Format("{0}", r.Memory.Value / 1024.0) : "-"),
                           new XElement("td",
                               r.ExecutionTime.HasValue ? r.ExecutionTime.Value.ToString() : "-"),
                           new XElement("td",
                               r.Status <= RunStatus.Running ? "-".ToXElement() :
                               r.Problem.IsArchived && r.Status == RunStatus.Done ? Html.AsLink("/Problems/TestRuns/" + r.RunId, testCaseText) :
                               testCaseText.ToXElement())
        )).ToString() + "<br/>" + links + "<br/>" + errors;
    }

    private static XElement CreateProgressBar(XElement status, string backcolor, int maxValue, string border, string textcolor, string progressColor, int num, int denom) {
        //never go past the done state
        //RunStatus.InternalError > RunStatus.Done, make the maximum width relative to RunStatus.Done
        int progressWidth = denom == 0 ? maxValue : (maxValue * Math.Min(num, (int)RunStatus.Done)) / denom;
        //set progress styles and numbers
        var outerDivStyle = String.Format("height: 20px; background-color:{0}; width:{1}px;border: 1px solid {2}; text-align: left; cursor: default;margin-left:auto;margin-right:auto", backcolor, maxValue, border);
        var innerTextStyle = String.Format("position: relative; text-align: center; z-index: 2; height: 20px;color:{0}", textcolor);
        var progressStyle = String.Format("position: relative; height: 20px; z-index: 1;text-align: center; margin-top: -20px; background-color: {0};width: {1}px", progressColor, progressWidth);
        var textDiv = new XElement("div", new XAttribute("style", innerTextStyle), status);
        var progressDiv = new XElement("div", new XAttribute("style", progressStyle));
        var progress = new XElement("div",
            new XAttribute("style", outerDivStyle),
            textDiv,
            progressDiv);
        return progress;
    }

    private static string GetProgressColor(Run run) {
        if ((run.Status < RunStatus.Done && run.Status != RunStatus.CompilationError) || run.Status == RunStatus.InternalError) {
            return "#F5F5F5";
        }
        if (run.Solved) {
            return "#CCFFCC";
        }
        return "#FFEBE8";
    }

    private static XElement GetStatusForRun(Run run) {
        XElement statusElement = run.Status.ToString().ToXElement();
        if (run.Status == RunStatus.CompilationError) {
            statusElement = Html.LinkToCompileError(run);
        }
        else if (run.Status == RunStatus.InternalError) {
            statusElement = Resource.InternalError.ToXElement(InteralErrorStyle);
        }
        else if (run.Status == RunStatus.Done) {
            switch (run.TestRunStatus) {
                case TestRunStatus.PresentationError:
                    statusElement = Resource.PresentationError.ToXElement(BadRunStyle);
                    break;
                case TestRunStatus.RuntimeError:
                    if (run.Problem.IsNew) {
                        statusElement = Resource.RunTimeError.ToXElement(BadRunStyle);
                    }
                    else {
                        statusElement = Html.LinkToRuntimeError(run);
                    }
                    break;
                case TestRunStatus.TimeLimitExceeded:
                    statusElement = Resource.TimeLimitExceeded.ToXElement(BadRunStyle);
                    break;
                case TestRunStatus.MemoryLimitExceeded:
                    statusElement = Resource.MemoryLimitExceeded.ToXElement(BadRunStyle);
                    break;
                case TestRunStatus.OutputLimitExceeded:
                    statusElement = Resource.OutputLimitExceeded.ToXElement(BadRunStyle);
                    break;
                case TestRunStatus.Accepted:
                    statusElement = Resource.Accepted.ToXElement(AcceptedRunStyle);
                    break;
                case TestRunStatus.WrongAnswer:
                    statusElement = Resource.WrongAnswer.ToXElement(BadRunStyle);
                    break;
                default:
                    break;
            }
        }
        return statusElement;
    }

    protected void runFilter_Click(object sender, EventArgs e) {
        var db = new FudgeDataContext();        
        var prob = db.Problems.SingleOrDefault(p => p.Name == problem.Text);
        var user = db.Users.SingleOrDefault(u => u.Email == email.Text);
        
        string pid = (prob == null ? null : prob.ProblemId.ToString()) ?? Request.QueryString["pid"];
        string uid = (user == null ? null : user.UserId.ToString()) ?? Request.QueryString["uid"];
        string lid = (!language.SelectedLanguageId.HasValue ? null : language.SelectedLanguageId.ToString()) ?? Request.QueryString["lid"];

        string queryString = Util.CreateQueryString(new Dictionary<string, object> {
            { "pid", String.IsNullOrEmpty(pid) ? null : pid },
            { "uid", String.IsNullOrEmpty(uid) ? null : uid },
            { "lid", String.IsNullOrEmpty(lid) ? null : lid }
        });

        Response.Redirect("Runs.aspx" + queryString);
    }
}
