using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;
using System.IO;

public partial class Teams_Settings : FudgePage {
    FudgeDataContext db = new FudgeDataContext();

    public Teams_Settings()
        : base(MenuItem.Teams) {
        VerifyQueryStringInt("id", id => db.Teams.Any(t => t.TeamId == id), "Team does not exist!");

        //user cannot access this page without being an admin
        VerifyQueryStringInt("id", id => FudgeUser.IsAdmin(id), "You do not have permission to edit the settings for this team.");
    }

    public Team Team {
        get {
            return Team.GetTeamById(Int32.Parse(Request.QueryString["id"]));
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        if (!ScriptManager.GetCurrent(this).IsInAsyncPostBack) {
            newTeamForm.DataBound += new EventHandler(newTeamForm_DataBound);
        }
        Title += ".Teams[\"" + Team.Name + "\"].Settings";
    }

    void newTeamForm_DataBound(object sender, EventArgs e) {
        //set the selected index for the edit
        newTeamForm.FindControl<DropDownList>("scope").SelectedIndex = (int)Team.Scope;
        newTeamForm.FindControl<DropDownList>("privacy").SelectedIndex = (int)Team.Status;
    }

    protected void newTeamForm_ItemUpdating(object sender, FormViewUpdateEventArgs e) {
        e.NewValues["Scope"] = newTeamForm.FindControl<DropDownList>("scope").SelectedIndex;
        e.NewValues["Status"] = newTeamForm.FindControl<DropDownList>("privacy").SelectedIndex;

        FileUpload avatar = newTeamForm.FindControl<FileUpload>("avatarupload");
        if (avatar.HasFile) {
            string extension = Path.GetExtension(avatar.FileName);
            var avatarImage = System.Drawing.Image.FromStream(avatar.FileContent);
            var imageData = avatarImage.Resize(96, 96).ToByteArray(extension);

            //change default pic
            if (Team.PictureId != Picture.TeamDefaultPicture) {
                //delete old picture
                var current = db.Pictures.SingleOrDefault(p => p.PictureId == Team.PictureId);
                current.Data = imageData;

                db.SubmitChanges();
            }
            else {
                e.NewValues["PictureId"] = Picture.CreateFrom(Team.Name + "'s Avatar", imageData);
            }
        }
    }

    protected string GetPromotionCommand(TeamUserStatus status) {
        if (status == TeamUserStatus.Admin) {
            return "Demote";
        }
        else if (status == TeamUserStatus.Member) {
            return "Promote";
        }
        else if (status == TeamUserStatus.Requested ||
                status == TeamUserStatus.RejectedRequest ||
                status == TeamUserStatus.RejectedInvite) {
            return "Approve";
        }
        return String.Empty;
    }

    protected string GetBannedCommand(TeamUserStatus status) {
        if (status == TeamUserStatus.Banned) {
            return "Unban";
        }
        else if (status == TeamUserStatus.Member || status == TeamUserStatus.Admin) {
            return "Ban";
        }
        else if (status == TeamUserStatus.Requested) {
            return "Reject";
        }
        return String.Empty;
    }

    protected string GetStatus(TeamUser user) {
        if (user.Status == TeamUserStatus.RejectedInvite) {
            return "Pending Approval";
        }
        else if (user.Status == TeamUserStatus.RejectedRequest) {
            return "Rejected";
        }
        else if (user.Status == TeamUserStatus.Admin && user.Team.UserId == user.UserId) {
            return "Creator";
        }
        return user.Status.ToString();
    }

    protected string GetRowColor(TeamUserStatus status) {
        return status == TeamUserStatus.Banned ? "#FFEBE8" : "#FFFFFF";
    }

    protected void SendInvites(object sender, EventArgs e) {        
        var users = new List<int>();
        foreach (ListItem item in friendList.Items) {
            if (item.Selected) {
                users.Add(Int32.Parse(item.Value));
            }
        }        

        if (!users.Any()) {            
            inviteMessage.Text = "No names selected.";
            inviteMessage.RenderAsError = true;
        }
        else {
            foreach (var uid in users) {                  
                Team.InviteMember(uid);
            }

            members.DataBind();
            friendList.DataBind();

            inviteMessage.RenderAsError = false;
            inviteMessage.Text = "Your Friends have been invited.";
        }
        inviteMessage.Show();
    }

    protected void newTeamForm_ItemUpdated(object sender, FormViewUpdatedEventArgs e) {
        message.Show();
        newTeamForm.Visible = false;
    }

    protected void userFriends_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        //approved friends
        var approvedFriends = from f in db.Friends
                              where f.UserId == FudgeUser.UserId && f.Status == FriendStatus.Accepted &&
                              f.User1.Status == UserStatus.Activated
                              select f.User1;

        //current team members
        var currentMembers = from tu in db.TeamUsers
                             where tu.TeamId == Team.TeamId
                             select tu.User;
        
        var users = approvedFriends.Except(currentMembers);

        switch (Team.Scope) {
            case TeamScope.Country:
                users = users.Where(u => u.CountryId == FudgeUser.CountryId);
                break;
            case TeamScope.School:
                users = users.Where(u => u.SchoolId == FudgeUser.SchoolId);
                break;
            default:
                break;
        }
        e.Result = users;
    }

    protected void members_ItemDeleted(object sender, ListViewDeletedEventArgs e) {
        friendList.DataBind();
    }

    protected bool IsMember(int userId) {
        var user = db.Users.SingleOrDefault(u => u.UserId == userId);

        return user.IsMemberOf(Team.TeamId);
    }

    protected void members_ItemCommand(object sender, ListViewCommandEventArgs e) {
        if (e.CommandName == "Promote") {
            int userId = Convert.ToInt32(e.CommandArgument);

            Team.ChangeUserStatus(userId, TeamUserStatus.Admin);
            Notification.Notify(Notification.PromoteToAdmin(userId, Team.TeamId));

            members.DataBind();
        }
        else if (e.CommandName == "Demote") {
            int userId = Convert.ToInt32(e.CommandArgument);

            Team.ChangeUserStatus(userId, TeamUserStatus.Member);
            Notification.Notify(Notification.DemoteToMember(userId, Team.TeamId));

            members.DataBind();
        }
        else if (e.CommandName == "Ban") {
            int userId = Convert.ToInt32(e.CommandArgument);

            Team.ChangeUserStatus(userId, TeamUserStatus.Banned);

            var user = Fudge.Framework.Database.User.GetUserById(userId);

            if (user.IsSubscribedTo(Team.TopicId)) {
                //subscribe to this team
                user.UnSubscribeFrom(Team.TopicId);
            }

            members.DataBind();
        }
        else if (e.CommandName == "Unban" || e.CommandName == "Approve") {
            int userId = Convert.ToInt32(e.CommandArgument);

            Team.ChangeUserStatus(userId, TeamUserStatus.Member);
            var user = Fudge.Framework.Database.User.GetUserById(userId);

            if (!user.IsSubscribedTo(Team.TopicId)) {
                //subscribe to this team
                user.SubscribeForReplies(Team.TopicId);
            }

            members.DataBind();
        }
        else if (e.CommandName == "Reject") {
            int userId = Convert.ToInt32(e.CommandArgument);

            Team.ChangeUserStatus(userId, TeamUserStatus.RejectedRequest);

            members.DataBind();
        }
    }

    protected void memberSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        var teamUsers = from tu in db.TeamUsers
                        where tu.TeamId == Team.TeamId
                        select tu;

        switch (memberFilter.SelectedIndex) {
            case 1:
                teamUsers = teamUsers.Where(u => u.Status == TeamUserStatus.Admin);
                break;
            case 2:
                teamUsers = teamUsers.Where(u => u.Status == TeamUserStatus.Admin || u.Status == TeamUserStatus.Member);
                break;
            case 3:
                teamUsers = teamUsers.Where(u => u.Status == TeamUserStatus.Invited || u.Status == TeamUserStatus.Requested);
                break;
            case 4:
                teamUsers = teamUsers.Where(u => u.Status == TeamUserStatus.Banned);
                break;
            default:
                break;
        }

        //filter members by dropdownlist selection
        e.Result = teamUsers;

    }

    protected void memberFilter_SelectedIndexChanged(object sender, EventArgs e) {
        members.DataBind();
    }
}