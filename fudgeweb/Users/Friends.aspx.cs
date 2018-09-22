using System;
using System.Linq;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;

public partial class Users_Friends : FudgePage {
    public Users_Friends()
        : base(MenuItem.MyProfile) {

    }

    public User CurrentUser {
        get {
            if (Request.IsQueryStringNull("id")) {
                return FudgeUser;
            }
            return Fudge.Framework.Database.User.GetUserById(Int32.Parse(Request.QueryString["id"]));
        }
    }

    protected bool Pending {
        get {
            return !Request.IsQueryStringNull("pending");
        }
    }

    protected string Heading {
        get {
            int count = Friends.Count();
            if (count == 0) {
                return "No pending requests";
            }
            return count == 1 ? "1 friend request" : Friends.Count() + " friend requests";
        }
    }

    protected IQueryable<User> Friends {
        get {
            var friends = Pending ? CurrentUser.PendingFriends : CurrentUser.ApprovedFriends;
            return friends;
        }
    }

    protected void OnFriendStatusChanged(object sender, FriendStatusEventArgs e) {
        Bind();
        message.Visible = true;
        message.InnerText = String.Format("You and {0} are now friends.", Fudge.Framework.Database.User.GetUserById(e.FriendId).FirstName);
    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Users[" + FudgeUser.FullName + "].Friends";
    }

    private void Bind() {
        if (Pending) {
            pendingFriends.DataBind();
        }
        else {
            friends.DataBind();
        }
    }

    protected void friendsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = Friends;
    }

    protected void Friends_ItemDataBound(object sender, ListViewItemEventArgs e) {
        var miniProfile = e.Item.FindControl<Controls_MiniProfile>("friend");
        if (miniProfile != null) {
            miniProfile.Bind();
        }
    }
}
