using System;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Controls_MiniProfile : System.Web.UI.UserControl {
    public int? UserId {
        get {
            return (int?)ViewState["UserId"];
        }
        set {
            ViewState["UserId"] = value;
        }
    }

    public bool ShowLinks {
        get {
            object o = ViewState["ShowLinks"];
            return o != null ? (bool)o : false;
        }
        set {
            ViewState["ShowLinks"] = value;
        }
    }

    protected User CurrentUser {
        get {
            return User.GetUserById(UserId.Value);
        }
    }
   
    protected string Class {
        get {
            return ShowLinks ? "wide_profile" : "thin_profile";
        }
    }

    private event EventHandler<FriendStatusEventArgs> _friendAddedRemoved;
    public event EventHandler<FriendStatusEventArgs> FriendAddedRemoved {
        add {
            _friendAddedRemoved += value;
        }
        remove {
            _friendAddedRemoved -= value;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        Bind();
    }

    public void Bind() {
        if (UserId.HasValue) {
            userView.DataSource = new[] { CurrentUser };
            userView.DataBind();
        }
    }

    protected void userView_ItemCommand(object sender, FormViewCommandEventArgs e) {
        var user = User.LoggedInUser;
        int userId = Convert.ToInt32(e.CommandArgument);
        LinkButton button = e.CommandSource as LinkButton;
        if (e.CommandName == "AddFriend") {
            if (button != null) {
                button.Enabled = false;
            }
            user.AddFriend(userId);
        }
        else if (e.CommandName == "RemoveFriend") {
            if (button != null) {
                button.Enabled = false;
            }
            user.RemoveFriend(userId);
        }
        else if (e.CommandName == "Approve") {
            if (button != null) {
                button.Enabled = false;
            }
            user.ApproveFriend(userId);
        }
        else if (e.CommandName == "Reject") {
            if (button != null) {
                button.Enabled = false;
            }
            user.RejectFriend(userId);
        }

        if (_friendAddedRemoved != null) {
            _friendAddedRemoved(this, new FriendStatusEventArgs(userId));
        }
    }
}
