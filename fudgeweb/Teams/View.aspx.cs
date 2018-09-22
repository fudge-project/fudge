using System;
using System.Linq;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;

public partial class Teams_View : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Teams_View()
        : base(MenuItem.Teams) {
        VerifyQueryStringInt("id", id => db.Teams.Any(t => t.TeamId == id), "Team does not exist!");

        //users cannot view teams they were banned from
        VerifyQueryStringInt("id", id => !FudgeUser.IsBanned(id), "You have been banned from this team.");

    }

    public Team Team {
        get {
            return Team.GetTeamById(Int32.Parse(Request.QueryString["id"]));
        }
    }

    //Open => Anyone in your {0} can join.
    //Invitation => Users can join only by invitation.
    //Private => Hide from anyone who is not a member.
    protected string StatusDescription {
        get {
            string status = String.Empty;
            switch (Team.Status) {
                case TeamStatus.Open:
                    if (Team.Scope == TeamScope.Global || Team.Scope == TeamScope.Friends) {
                        return "Anyone can join.";
                    }
                    status = "Anyone from {0} can join.";
                    break;
                case TeamStatus.Invitation:
                    if (Team.Scope == TeamScope.Global || Team.Scope == TeamScope.Friends) {
                        return "Users can join by invitation only.";
                    }
                    status = "Users from {0} can join by invitation only.";
                    break;
                case TeamStatus.Private:
                    status = "Hide from anyone who is not a member.";
                    break;
                default:
                    break;
            }
            switch (Team.Scope) {
                case TeamScope.Country:
                    status = String.Format(status, Team.User.Country.Name);
                    break;
                case TeamScope.School:
                    status = String.Format(status, Team.User.School.Name);
                    break;
                case TeamScope.Friends:
                    break;
            }
            return status;
        }
    }

    protected void OnCommentPosted(object sender, CommentPostedArgs e) {
        foreach (var u in Team.Members) {
            if (u.UserId != FudgeUser.UserId && u.IsSubscribedTo(Team.TopicId)) {
                Email.NotifyTeamPost.Send(u, FudgeUser.FirstName + " left a message on " + Team.Name + "' Message Pump",
                     FudgeUser.FirstName, Team.Name, e.Post.Message, Team.TeamId);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Teams[\"" + Team.Name + "\"]";
    }
    protected void usersSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        //randomize the team members
        e.Result = Team.Members;
    }

    protected void teamUsers_ItemDataBound(object sender, ListViewItemEventArgs e) {
        var miniProfile = e.Item.FindControl<Controls_MiniProfile>("user");
        if (miniProfile != null) {
            miniProfile.Bind();
        }
    }

    protected void requestTeam_Click(object sender, EventArgs e) {
        FudgeUser.RequestJoinTeam(Team.TeamId);
        teamView.DataBind();

        ShowMessage("Waiting on " + Team.Name + "'s admin to approve request");
    }

    protected void leaveTeam_Click(object sender, EventArgs e) {
        FudgeUser.LeaveTeam(Team.TeamId);
        teamView.DataBind();

        ShowMessage("Left " + Team.Name);
    }

    protected void joinTeam_Click(object sender, EventArgs e) {
        FudgeUser.AcceptTeamInvite(Team.TeamId);
        teamView.DataBind();

        ShowMessage("Successfully joined " + Team.Name);
    }

    protected void rejectTeam_Click(object sender, EventArgs e) {
        FudgeUser.RejectTeamInvite(Team.TeamId);
        teamView.DataBind();
    }

    private void ShowMessage(string text) {
        var message = teamView.FindControl<Controls_Tooltip>("message");
        message.Text = text;
        message.Show();
    }

    protected void adminSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = from u in Team.TeamUsers
                   where u.Status == TeamUserStatus.Admin
                   select u;
    }
}
