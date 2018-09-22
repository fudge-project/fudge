using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;
using System.Data.Linq;
using Fudge.Framework.Database;
using Fudge.Modules.Standings;

public partial class Contests_Environment : FudgePage {
    public Contests_Environment()
        : base(MenuItem.Contests) {
        VerifyQueryString("name", name => db.Contests.Any(c => c.UrlName == name), "Contest not found");

        VerifyQueryString("name", name => db.Contests.Any(c => c.UrlName == name &&
            SqlMethods.DateDiffSecond(c.StartTime, DateTime.UtcNow) > 0), "Contest has not started.");

        VerifyQueryString("name", name => FudgeUser.IsRegisteredFor(Contest.ContestId), "You are not registered for this contest.");
    }

    protected string ContestEnded {
        get {
            return Contest != null ? String.Format("The contest has ended. Click {0} to see the results", Html.LinkToScoreboard(Contest.ContestId, "here")) :
                String.Empty;
        }
    }

    protected int SelectedProblemId {
        get {
            int? id = (int?)ViewState["SelectedProblemId"];
            return id.HasValue ? id.Value : Contest.ContestProblems.First().ProblemId;
        }
        set {
            ViewState["SelectedProblemId"] = value;
        }
    }

    FudgeDataContext db = new FudgeDataContext();
    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Contests." + Contest.Name;

        var scriptManager = ScriptManager.GetCurrent(this);

        if (scriptManager.IsInAsyncPostBack) {
            /*string id = scriptManager.AsyncPostBackSourceElementID;            
            if (id.EndsWith("solveProblem")) {
                var tip = UpdateProgress1.TemplateControl.FindControl<Controls_Tooltip>("submitTip");
                tip.Text = "Compiling Submission...";
            }*/

        }

        if (!scriptManager.IsInAsyncPostBack && !Page.IsPostBack) {
            //the the initial problem and language 
            problemView.ProblemId = SelectedProblemId;
            languages.SelectedLanguageId = FudgeUser.LanguageId;

            //calculate remaining time of contest
            var timeSpan = Contest.EndTime.Subtract(DateTime.UtcNow);

            string javaScript = String.Format("var time = {{ h : {0}, m : {1}, s : {2} }}; GetCurrentContestTime();",
                Math.Max(timeSpan.Hours, 0),
                Math.Max(timeSpan.Minutes, 0),
                Math.Max(timeSpan.Seconds, 0));
            Page.ClientScript.RegisterStartupScript(typeof(Page), "gettime", javaScript, true);
        }
    }


    protected Contest Contest {
        get {
            return db.Contests.SingleOrDefault(c => c.UrlName == Request.QueryString["name"]);
        }
    }

    protected void contestProblems_ItemCommand(object sender, ListViewCommandEventArgs e) {
        if (e.CommandName == "ChangeProblem") {
            SelectedProblemId = Convert.ToInt32(e.CommandArgument);
            //change the problem
            problemView.ProblemId = SelectedProblemId;
            
            //rebind the controls
            contestProblems.DataBind();
            contestRuns.DataBind();

            source.Text = String.Empty;
            //update the runs panel
            //update the problem update panel
            runsPanel.Update();
            sourcePanel.Update();
            problemPanel.Update();            
            
            //reset the selected index
            contestRuns.SelectedIndex = -1;
            //hide the error message
            compilerErrorTip.Hide();

            sourceViewPanel.Update();
        }
    }

    protected void solveProblem_Click(object sender, EventArgs e) {

        //reset the selected index
        contestRuns.SelectedIndex = -1;
        //hide the error message
        compilerErrorTip.Hide();

        //create the compiler
        fudge.fit.edu.Compiler compiler = new fudge.fit.edu.Compiler();
        //don't allow submissions on compiler errors
        string result = compiler.Compile(languages.SelectedLanguageId.Value, source.Text);
        if (result != "Compiled Successfully") {
            compilerErrorTip.Text = FormatHelper.TextToMarkup(result + "\n" + "Submission failed..");
            compilerErrorTip.Show();
        }
        else {            
            //don't allow submissions until after
            if (Contest.IsRunning) {
                //create a new contest run
                Run run = new Run {
                    UserId = FudgeUser.UserId,
                    Timestamp = DateTime.Now.ToUniversalTime(),
                    LanguageId = languages.SelectedLanguageId.Value,
                    Status = RunStatus.Pending,
                    ProblemId = SelectedProblemId,
                    ContestId = Contest.ContestId,
                    Code = source.Text,
                    Size = source.Text.Length
                };
                db.Runs.InsertOnSubmit(run);
                db.SubmitChanges();

                contestRuns.DataBind();
            }
        }
    }

    protected string FormatSubmissionTime(DateTime submissionTime) {
        return FormatHelper.FormatTime(submissionTime.Subtract(Contest.StartTime));
    }

    protected void contestRunsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        //show only submission from the logged in user
        e.Result = from r in db.Runs
                   where Contest.ContestId == r.ContestId && r.ProblemId == SelectedProblemId &&
                   r.UserId == FudgeUser.UserId
                   orderby r.Timestamp descending
                   select r;
    }

    class ScoreboardTuple {
        public User User { get; set; }
        public int Submitted { get; set; }
    }


    protected void scoreBoardSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        //score board will show the user and amount of problems submitted
        var scoreboard = from u in db.ContestUsers
                         where Contest.ContestId == u.ContestId
                         let submitted = (from r in db.Runs
                                          where r.UserId == u.UserId && r.ContestId == Contest.ContestId
                                          select r.ProblemId).Distinct().Count()
                         orderby u.User.GlobalRank
                         orderby submitted descending
                         select new ScoreboardTuple { User = u.User, Submitted = submitted };

        e.Result = Rankings.GetRankingsList(scoreboard, u => u.Submitted);
    }

    protected void scoreBoardTimer_Tick(object sender, EventArgs e) {
        if (Contest.IsRunning) {
            //update the scoreboard
            scoreBoard.DataBind();
        }
    }

    protected void contestRuns_ItemCommand(object sender, ListViewCommandEventArgs e) {
        if (e.CommandName == "Select") {
            sourceView.RunId = Convert.ToInt32(e.CommandArgument);
            sourceViewPanel.Update();
            compilerErrorTip.Hide();
        }
    }
}
