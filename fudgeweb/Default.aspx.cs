using System;
using System.Linq;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;

public partial class _Default : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public _Default()
        : base(MenuItem.None, false) {

    }

    protected int UserCount {
        get {
            return db.Users.Count(u => u.Status == UserStatus.Activated);
        }
    }

    protected int SchoolCount {
        get {
            return db.Users.Where(u => u.Status == UserStatus.Activated).GroupBy(u => u.School).Count();
        }
    }

    protected int CountryCount {
        get {
            return db.Users.Where(u => u.Status == UserStatus.Activated).GroupBy(u => u.Country).Count();
        }
    }

    protected int ProblemCount {
        get {
            return db.Problems.Count(p => p.Visible);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        if (Fudge.Framework.Database.User.LoggedInUser == null) {
            Form.DefaultButton = Master.FindControl<Button>("loginButton").UniqueID;
        }
        else {
            Form.DefaultButton = searchUser.UniqueID;
        }
        Title += ".The Social Programming Network";

        //default query as there are no rankings as yet                     
        toptenRankings.DataSource = db.Users.OrderBy(u => u.GlobalRank).Take(10);
        toptenRankings.DataBind();
    }

    protected void searchUser_Click(object sender, EventArgs e) {
        Response.Redirect("/Users/Search?name=" + userSearch.Text);
    }

    protected void referUser_Click(object sender, EventArgs e) {
        message.Visible = true;
        if (FudgeUser != null) {
            var user = db.Users.SingleOrDefault(u => u.Email == email.Text.Trim());
            if (user != null) {
                message.InnerHtml = String.Format("{0} is already a member on fudge.", Html.LinkToProfile(user.UserId));
            }
            else {
                //send the invitation email
                if (FudgeUser.Invite(email.Text)) {
                    message.Attributes["class"] = "fudge_message";
                    message.InnerText = "Invitation sent successfully";
                }
                else {
                    message.InnerText = "There was an error sending invite, make sure the email is valid";
                }
            }
        }
        else {
            message.InnerText = "You must be logged in to invite a user";
        }
        email.Text = String.Empty;
    }
}
