using System;
using System.Linq;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Controls_FriendsList : System.Web.UI.UserControl {
    public int? UserId {
        get {
            return (int?)ViewState["UserId"];
        }
        set {
            ViewState["UserId"] = value;            
        }
    }

    public FriendStatus Filter {
        get {
            return (FriendStatus)ViewState["Filter"];
        }
        set {
            ViewState["Filter"] = value;
        }
    }

    public int? MaxFriends {
        get {
            return (int?)ViewState["MaxFriends"];
        }
        set {
            ViewState["MaxFriends"] = value;
        }
    }   

    private IQueryable<User> Friends {
        get {
            var user = User.GetUserById(UserId.Value);                        
            var friends = Filter == FriendStatus.Accepted ? user.ApprovedFriends : user.PendingFriends;
            return MaxFriends.HasValue ? friends.ToRandom().Take(MaxFriends.Value) : friends.ToRandom();
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        Bind();
    }

    public void Bind() {
        if (UserId.HasValue) {
            friends.DataSource = Friends;
            friends.DataBind();
        }
    }    
}
