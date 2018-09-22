using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Xml.Linq;
using Fudge.Framework.Database;
using Fudge.Modules.Standings;
using Resources;
using System.Data.Linq;

public partial class Contests_Scoreboard : FudgePage {
    private const int PageSize = 30;

    //css styles for content
    private static XAttribute BadRunStyle = new XAttribute("class", "badrun");
    private static XAttribute AcceptedRunStyle = new XAttribute("class", "accepted");
    public static XAttribute InteralErrorStyle = new XAttribute("style", "font-weight:bold;color:black");


    public Contests_Scoreboard()
        : base(MenuItem.Contests, false) {
        VerifyQueryString("name", name => db.Contests.Any(c => c.UrlName == name), "Contest does not exist");
    }

    FudgeDataContext db = new FudgeDataContext();
    public Contest Contest {
        get {
            return db.Contests.SingleOrDefault(c => c.UrlName == Request.QueryString["name"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        ClientScript.RegisterStartupScript(typeof(Page), "k", "initPolling();", true);
    }

    class ScoreTuple {
        public User User { get; set; }
        public int Submitted { get; set; }
        public IEnumerable<ProblemTuple> Problems { get; set; }
    }

    class ProblemTuple {
        public string Name { get; set; }
        public Run Run { get; set; }
    }

    class UserRunTuple {
        public User User { get; set; }
        public IGrouping<User, Run> Runs { get; set; }
    }

    static Func<FudgeDataContext, int, IQueryable<UserRunTuple>> compiledScoreboard = CompiledQuery.Compile(
        (FudgeDataContext db, int contestId) => from r in db.Runs
                                                where r.ContestId == contestId
                                                group r by r.User into g
                                                orderby g.Key.GlobalRank
                                                select new UserRunTuple { User = g.Key, Runs = g });

    [WebMethod]
    public static string GetScoreboard(string urlName, int page) {
        FudgeDataContext db = new FudgeDataContext();
        Contest contest = db.Contests.Single(c => c.UrlName == urlName);
        var userRunGroups = compiledScoreboard(db, contest.ContestId);

        var query = from u in userRunGroups
                    let submitted = u.Runs.Select(r => r.ProblemId).Distinct()
                    let hasEnded = DateTime.UtcNow >= contest.EndTime
                    let problems = from pid in submitted
                                   let problem = db.Problems.Single(p => p.ProblemId == pid)
                                   let lastSubmitted = u.Runs.Where(r => r.ProblemId == pid).Max(r => r.RunId)
                                   let lastRun = u.Runs.Single(r => r.RunId == lastSubmitted)
                                   orderby pid
                                   select new ProblemTuple {
                                       Name = problem.Name,
                                       Run = lastRun
                                   }
                    let order = !hasEnded ? submitted.Count() : problems.Count(p => p.Run.Solved)
                    select new ScoreTuple { User = u.User, Problems = problems, Submitted = order };

        var scoreboard = Rankings.GetRankingsList(query, u => u.Submitted);
        var links = Html.CreateNumericPagerLinks(PageSize, page, scoreboard.Count(), "javascript:selectPage({0})");

        var rows = from rankItem in scoreboard.ToPagedList(page - 1, PageSize)
                   let userProfile = Html.AsLink("/Users/Profile/" + rankItem.Item.User.UserId, rankItem.Item.User.DisplayName)
                   let schoolProfile = Html.AsLink("/Schools/" + rankItem.Item.User.SchoolId, rankItem.Item.User.School.Name)
                   let active = rankItem.Item.Problems.Any(p => p.Run.Status != RunStatus.Done && p.Run.Status != RunStatus.CompilationError)
                   let problemTable = from p in rankItem.Item.Problems
                                      let numerator = GetNumerator(p.Run)
                                      let denominator = (int)RunStatus.Done
                                      let progressColor = GetProgressColor(p.Run)
                                      let runStatus = GetStatusForRun(p.Run)
                                      let progressBar = CreateProgressBar(runStatus, "#FFFFFF", 200, "#CCC", "#CCC",
                                                        progressColor, numerator, denominator)
                                      select new XElement("tr",
                                                    new XElement("td", new XElement("b", p.Name),
                                                        new XAttribute("style", "text-right:left;width:40%")),
                                                    new XElement("td", progressBar,
                                                        new XAttribute("style", "text-align:left"))
                                                        )
                   select new XElement("tr", new XAttribute("class", active ? "active" : ""),
                           new XElement("td", rankItem.Rank + ".", new XAttribute("style", "width:5%")),
                           new XElement("td", userProfile, new XAttribute("style", "width:20%")),
                           new XElement("td", schoolProfile),
                           new XElement("td",
                               new XElement("div",
                                   new XAttribute("class", "scoreboard_problems"),
                                   new XElement("table", new XElement("tbody", problemTable)))));

        var table = new XElement("table",
                        new XElement("tr",
                             new XElement("th", "Rank"),
                             new XElement("th", "User"),
                             new XElement("th", "School"),
                             new XElement("th", "Problems Submitted"))
                             , rows).ToString();
        return table + "<br/>" + links;
    }

    private static XElement CreateProgressBar(XElement status, string backcolor, int maxValue, string border, string textcolor, string progressColor, int num, int denom) {
        //never go past the done state
        //RunStatus.InternalError > RunStatus.Done, make the maximum width relative to RunStatus.Done
        int progressWidth = denom == 0 ? maxValue : (maxValue * Math.Min(num, (int)RunStatus.Done)) / denom;
        //set progress styles and numbers
        var outerDivStyle = String.Format("height: 20px; background-color:{0}; width:{1}px;border: 1px solid {2}; text-align: left; cursor: default;", backcolor, maxValue, border);
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
            statusElement = Resource.CompilationError.ToXElement(BadRunStyle);
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
                    statusElement = Resource.RunTimeError.ToXElement(BadRunStyle);
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

    private static int GetNumerator(Run run) {
        if (run.Status == RunStatus.CompilationError) {
            return (int)RunStatus.Done;
        }
        if (run.Status == RunStatus.InternalError) {
            return (int)RunStatus.Done;
        }
        return (int)run.Status;
    }

}
