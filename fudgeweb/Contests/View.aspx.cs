using System;
using System.Linq;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Contests_View : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Contests_View()
        : base(MenuItem.Contests, false) {
        VerifyQueryString("name", name => db.Contests.Any(c => c.UrlName == name), "Contest does not exists!");
    }

    protected Contest Contest {
        get {
            return db.Contests.SingleOrDefault(c => c.UrlName == Request.QueryString["name"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Contests." + Contest.Name + ".Details";
        if (Contest.IsRunning) {
            contestTip.Text = "Contest is running.";
            contestTip.IsClosable = false;
            if (FudgeUser != null && FudgeUser.IsRegisteredFor(Contest.ContestId) && !Contest.HasEnded) {
                contestTip.Text += String.Format(" Click {0} to enter", Html.Link("/Contests/Environment/" + Contest.UrlName, "here"));
            }
            contestTip.Show();
        }
        else if (Contest.HasEnded) {
            contestTip.Text = "Contest has ended.";
            contestTip.IsClosable = false;
            contestTip.Show();
        }
    }

    protected string GetScoring(object o) {
        ContestScoring scoring = (ContestScoring)o;
        switch (scoring) {
            case ContestScoring.DeferredJudging:
                return "Deferred Judging";
        }
        return String.Empty;
    }

    protected string GetScoringDescription(object o) {
        ContestScoring scoring = (ContestScoring)o;
        switch (scoring) {
            case ContestScoring.DeferredJudging:
                return "Judging will take place at the end of the contest.";
        }
        return String.Empty;
    }

    protected void contestUsers_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        //order contest users by global rank
        e.Result = from u in Contest.ContestUsers
                   orderby u.User.GlobalRank
                   select u;
    }

    protected void SignUp_Click(object sender, EventArgs e) {
        //only let users register if contest has not started
        if (!Contest.IsRunning) {
            db.ContestUsers.InsertOnSubmit(new ContestUser {
                ContestId = Contest.ContestId,
                UserId = FudgeUser.UserId
            });

            db.SubmitChanges();

            //show tooltip
            contestTip.Text = "Successfully registered for the contest";
            contestTip.Show();

            contestView.DataBind();
            contestUsers.DataBind();
        }
        else {
            //otherwise tell the user they are too late
            contestTip.RenderAsError = true;
            contestTip.Text = "Sorry the contest is running!";
            contestTip.Show();
        }
    }
}
