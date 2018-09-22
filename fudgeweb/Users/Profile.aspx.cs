using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;
using WebChart;

public partial class Users_Profile : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Users_Profile()
        : base(HttpContext.Current.Request.IsQueryStringNull("id") ? MenuItem.MyProfile : MenuItem.Community) {

        VerifyQueryStringInt("id", id => db.Users.Any(u => u.UserId == id), "User does not exist!", false);
    }

    protected string UserImage {
        get {
            return "/Images/" + CurrentUser.PictureId;
        }
    }

    public User CurrentUser {
        get {
            int id = FudgeUser.UserId;
            if (!Request.IsQueryStringNull("id")) {
                id = Int32.Parse(Request.QueryString["id"]);
            }
            return Fudge.Framework.Database.User.GetUserById(id);
        }
    }

    public void FriendStatusChanged(object sender, EventArgs e) {
        BindProfile();
    }

    protected string GetFriendDescription(int count) {
        string user = CurrentUser.IsLoggedOn ? "You have" : CurrentUser.FirstName + " has";
        if (count == 0) {
            return user + " no friends";
        }
        return count == 1 ? user + " 1 friend" : user + " " + count + " friends";
    }

    protected void OnCommentPosted(object sender, CommentPostedArgs e) {
        if (!CurrentUser.IsLoggedOn) {

            Notification.Notify(Notification.StackPost(CurrentUser.UserId));

            Email.NotifyStackPost.Send(CurrentUser, FudgeUser.FirstName + " pushed a comment on your stack!",
                            FudgeUser.FirstName, e.Post.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this);
        //only update the necessary stuff
        if (!scriptManager.IsInAsyncPostBack) {
            if (Request.IsQueryStringNull("id")) {
                userData.WhereParameters["UserId"].DefaultValue = FudgeUser.UserId.ToString();
            }

            if (!Request.IsQueryStringNull("edit")) {
                userProfile.DefaultMode = FormViewMode.Edit;
            }
            else {
                userProfile.DefaultMode = FormViewMode.ReadOnly;
            }
        }
    }


    protected void userProfile_ItemUpdating(object sender, FormViewUpdateEventArgs e) {
        var avatar = userProfile.FindControl<FileUpload>("avatarupload");

        if (avatar != null && avatar.HasFile) {
            string extension = Path.GetExtension(avatar.FileName);
            var avatarImage = System.Drawing.Image.FromStream(avatar.FileContent);
            var imageData = avatarImage.Resize(96, 96).ToByteArray(extension);

            if (CurrentUser.PictureId != Picture.ProfileDefaultPicture) {
                //get current picture and update it
                var current = db.Pictures.SingleOrDefault(p => p.PictureId == FudgeUser.PictureId);
                current.Data = imageData;

                db.SubmitChanges();
            }
            else {
                e.NewValues["PictureId"] = Picture.CreateFrom(CurrentUser.DisplayName + "'s Avatar", imageData);
            }
        }
    }

    public void BindProfile() {
        if (CurrentUser != null) {
            Title += ".Users[\"" + CurrentUser.FullName + "\"]";
            userProfile.DataBind();
        }
    }

    protected void userProfile_ItemCommand(object sender, FormViewCommandEventArgs e) {
        if (e.CommandName == "AddFriend") {
            int userId = Convert.ToInt32(e.CommandArgument);
            FudgeUser.AddFriend(userId);
            BindProfile();
        }
        else if (e.CommandName == "RemoveFriend") {
            int userId = Convert.ToInt32(e.CommandArgument);
            FudgeUser.RemoveFriend(userId);
            BindProfile();
        }
        if (e.CommandName == "Approve") {
            int userId = Convert.ToInt32(e.CommandArgument);
            FudgeUser.ApproveFriend(userId);
            BindProfile();
        }
        else if (e.CommandName == "Reject") {
            int userId = Convert.ToInt32(e.CommandArgument);
            FudgeUser.RejectFriend(userId);
            BindProfile();
        }

    }

    protected void userProfile_DataBound(object sender, EventArgs e) {
        var friends = userProfile.FindControl<Controls_FriendsList>("friends");
        if (friends != null) {
            friends.Bind();
        }
        BindCharts();
    }

    private void BindCharts() {
        var standings = userProfile.FindControl<ChartControl>("standings");
        if (standings != null) {
            //plot line chart showing users standings
            var points = from s in CurrentUser.Standings
                         group s by s.Timestamp.ToString("MMM dd") into g
                         select new {
                             g.Key,
                             Points = g.Average(a => (float)a.Points)
                         };

            standings.Charts[0].DataSource = points;
            standings.Charts[0].DataBind();

            //redraw the chart            
            standings.RedrawChart();
        }
    }

    protected void solvedProblemSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = CurrentUser.ProblemStats;
    }


    protected void teamsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = from t in db.TeamUsers
                   where t.UserId == CurrentUser.UserId &&
                   (t.Status == TeamUserStatus.Admin || t.Status == TeamUserStatus.Member)
                   select t;
    }

    protected void notificationsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        FudgeDataContext db = new FudgeDataContext();
        e.Result = (from n in db.Notifications
                    where n.UserId == FudgeUser.UserId
                    orderby n.Timestamp descending
                    select n).Take(5);

    }

    protected void userProfile_ItemUpdated(object sender, FormViewUpdatedEventArgs e) {
        Response.Redirect("/Users/Profile");
    }

    protected void recentPostsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = (from p in db.Posts
                    where p.Topic.Visible && p.UserId == CurrentUser.UserId
                    orderby p.Timestamp descending
                    select p).Take(5);
    }
}
