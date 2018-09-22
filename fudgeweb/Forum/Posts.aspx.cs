using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using FudgeDatabase = Fudge.Framework.Database;

public partial class Community_Forum_Posts : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    protected Topic Topic {
        get {
            return db.Topics
                .SingleOrDefault(t => t.TopicId == Int32.Parse(Request.QueryString["id"]));
        }
    }

    public string PostPath {
        get {
            string forumPart = String.Empty;
            if (Topic.Forum.ForumCategory.Visible) {
                forumPart = Html.AsLink("/Community/Forum/" + Topic.Forum.CategoryId, Topic.Forum.ForumCategory.Title,
                new XAttribute("class", "nounderline")).ToString() + " &#187; ";
            }
            return "<b>" + forumPart +
             Html.AsLink("/Community/Forum/Topic/" + Topic.ForumId, Topic.Forum.Title,
                new XAttribute("class", "nounderline")).ToString() + " &#187; " +
             Html.AsLink("/Community/Forum/Posts/" + Topic.TopicId, Topic.Title,
                new XAttribute("class", "nounderline")).ToString() + "</b>";
        }
    }

    public Community_Forum_Posts()
        : base(MenuItem.Community, false) {

        VerifyQueryStringInt("id", id => db.Topics.Any(t => t.TopicId == id), "Topic not found!");
    }

    protected int FirstPostId {
        get {
            return db.Posts.Where(t => t.TopicId == Topic.TopicId).Min(p => p.PostId);
        }
    }

    protected string GetPostTitle(Post post) {
        return post.PostId == FirstPostId ? post.Title : "Re: " + post.Title;
    }

    public string GetUserPosts(Post post) {
        int count = post.User.Posts.Count(p => p.Topic.Visible);
        return count == 1 ? count + " Post" : count + " Posts";
    }

    protected void Page_Load(object sender, EventArgs e) {
        Posts.ItemInserted += new EventHandler<ListViewInsertedEventArgs>(Posts_ItemInserted);
    }

    void Posts_ItemInserted(object sender, ListViewInsertedEventArgs e) {
        if (!Topic.User.IsLoggedOn) {
            //only subscribe once
            if (FudgeUser.IsOptionSet(FudgeDatabase.User.UserOptions.AutomaticallySubscribeToTopicsIReplyTo) &&
                !FudgeUser.IsSubscribedTo(Topic.TopicId)) {

                //subscribe
                FudgeUser.SubscribeForReplies(Topic.TopicId);
            }

            var subscriptions = from s in db.TopicSubscriptions
                                where s.TopicId == Topic.TopicId && s.UserId != FudgeUser.UserId && s.UserId != Topic.UserId
                                select s;

            foreach (var s in subscriptions) {
                Email.NotifyNotMyTopicReply.Send(s.User, FudgeUser.FirstName + " replied to " + Topic.Title,
                    FudgeUser.FirstName, Topic.Title, Topic.TopicId);
            }
        }

        if (!Topic.User.IsLoggedOn && Topic.User.IsSubscribedTo(Topic.TopicId)) {
            //send notification to the topic owner
            Notification.Notify(Notification.TopicReply(Topic.TopicId));

            //send and email
            Email.NotifyTopicReply.Send(Topic.User, FudgeUser.FirstName + " replied to your post!",
                FudgeUser.FirstName, Topic.Title, Topic.TopicId);
        }
        //go to the last page
        int lastPage = (postsPager.TotalRowCount / postsPager.PageSize) + 1;
        postsPager.CurrentPage = lastPage;
    }

    protected void Posts_ItemInserting(object sender, ListViewInsertEventArgs e) {
        var textBox = e.Item.FindControl<TextBox>("postBody");
        e.Cancel = String.IsNullOrEmpty(textBox.Text);
        if (!e.Cancel) {            
            e.Values["RatingId"] = Rating.NewRating();
            e.Values["TopicId"] = Topic.TopicId;
            e.Values["Visible"] = true;
            e.Values["Title"] = Topic.Title;
            e.Values["UserId"] = FudgeUser.UserId;
            e.Values["Timestamp"] = DateTime.UtcNow;
        }
        else {
            var error = e.Item.FindControl<HtmlGenericControl>("insertError");
            error.Visible = true;
        }
    }

    protected void Posts_ItemDataBound(object sender, ListViewItemEventArgs e) {
        var rating = e.Item.FindControl<Controls_Rating>("postRating");
        if (rating != null) {
            rating.Bind();
        }
    }

    protected void Posts_ItemUpdating(object sender, ListViewUpdateEventArgs e) {
        var textBox = Posts.Items[e.ItemIndex].FindControl<TextBox>("postBody");
        e.Cancel = String.IsNullOrEmpty(textBox.Text.Trim());
        if (e.Cancel) {
            var error = Posts.Items[e.ItemIndex].FindControl<HtmlGenericControl>("editError");
            error.Visible = true;
        }
    }
}
