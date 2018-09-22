using System;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;
using System.Xml.Linq;

public partial class Controls_Posts : System.Web.UI.UserControl {
    FudgeDataContext db = new FudgeDataContext();

    private event EventHandler<CommentPostedArgs> _posted;
    public event EventHandler<CommentPostedArgs> Posted {
        add {
            _posted += value;
        }
        remove {
            _posted -= value;
        }
    }

    public int? TopicId {
        get {
            return (int?)ViewState["TopicId"];
        }
        set {
            ViewState["TopicId"] = value;
        }
    }

    protected Topic Topic {
        get {
            return db.Topics.SingleOrDefault(t => t.TopicId == TopicId.Value);
        }
    }

    public int MaxPosts {
        get {
            return Pager.PageSize;
        }
        set {
            Pager.PageSize = value;
        }
    }

    public bool CanPost {
        get {
            object o = ViewState["PostsVisible"];
            return o != null ? (bool)o : true;
        }
        set {
            ViewState["PostsVisible"] = value;
        }
    }

    public bool CanModifyPost {
        get {
            object o = ViewState["CanModifyPost"];
            return o != null ? (bool)o : true;
        }
        set {
            ViewState["CanModifyPost"] = value;
        }
    }

    public bool ShowEmpty {
        get {
            object o = ViewState["ShowEmpty"];
            return o != null ? (bool)o : true;
        }
        set {
            ViewState["ShowEmpty"] = value;
        }
    }

    public bool CanSubscribe {
        get {
            object o = ViewState["CanSubscribe"];
            return o != null ? (bool)o : true;
        }
        set {
            ViewState["CanSubscribe"] = value;
        }
    }

    public bool CanPostTags {
        get {
            object o = ViewState["CanPostTags"];
            return o != null ? (bool)o : false;
        }
        set {
            ViewState["CanPostTags"] = value;
        }
    }

    public bool ShowHeader {
        get {
            object o = ViewState["ShowHeader"];
            return o != null ? (bool)o : false;
        }
        set {
            ViewState["ShowHeader"] = value;
        }
    }


    public string PostButtonText {
        get {
            return (string)ViewState["PostButtonText"] ?? "Post";
        }
        set {
            ViewState["PostButtonText"] = value;
        }
    }

    public string DeleteButtonText {
        get {
            return (string)ViewState["DeleteButtonText"] ?? "Delete";
        }
        set {
            ViewState["DeleteButtonText"] = value;
        }
    }

    public int StartFrom {
        get {
            object o = ViewState["StartFrom"];
            return o != null ? (int)o : 0;
        }
        set {
            ViewState["StartFrom"] = value;
        }
    }

    protected void Posts_ItemInserted(object sender, FormViewInsertedEventArgs e) {
        Posts.DataBind();
    }

    protected void Posts_ItemInserting(object sender, FormViewInsertEventArgs e) {
        var textBox = insertPost.FindControl<TextBox>("postBody");
        e.Cancel = String.IsNullOrEmpty(textBox.Text) || Fudge.Framework.Database.User.LoggedInUser == null;

        if (!e.Cancel) {
            //add rating for this post
            var rating = new Rating {
                Sum = 0,
                Count = 0
            };
            db.Ratings.InsertOnSubmit(rating);
            db.SubmitChanges();

            e.Values["RatingId"] = rating.RatingId;
            e.Values["TopicId"] = Topic.TopicId;
            e.Values["Timestamp"] = DateTime.Now.ToUniversalTime();
            e.Values["Visible"] = true;
            e.Values["Title"] = Topic.Title;
            e.Values["UserId"] = Fudge.Framework.Database.User.LoggedInUser.UserId;
        }
        else {
            var error = insertPost.FindControl<HtmlGenericControl>("insertError");
            error.Visible = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void postsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        var posts = from p in Topic.Posts
                    where p.TopicId == Topic.TopicId
                    select p;

        e.Result = posts.Skip(StartFrom).OrderByDescending(p => p.Timestamp);
    }

    protected string SubscribeText {
        get {
            if (User.LoggedInUser != null) {
                return User.LoggedInUser.IsSubscribedTo(Topic.TopicId) ? "unsubscribe" : "subscribe";
            }
            return String.Empty;
        }
    }

    protected string SubscribeImage {
        get {
            if (User.LoggedInUser != null) {
                return User.LoggedInUser.IsSubscribedTo(Topic.TopicId) ?
                    Html.Image("unsubscribe.png", "unsubscribe", new XAttribute("align", "middle")) 
                    : Html.Image("subscribe.png", "subscribe", new XAttribute("align", "middle"));
            }
            return String.Empty;
        }
    }

    protected void postsSource_Inserted(object sender, LinqDataSourceStatusEventArgs e) {
        if (_posted != null) {
            _posted(this, new CommentPostedArgs(e.Result as Post));
        }
    }

    protected void subscribe_Click(object sender, EventArgs e) {
        if (User.LoggedInUser != null) {
            if (User.LoggedInUser.IsSubscribedTo(Topic.TopicId)) {
                User.LoggedInUser.UnSubscribeFrom(Topic.TopicId);
            }
            else {
                User.LoggedInUser.SubscribeForReplies(Topic.TopicId);
            }
        }
    }
}
