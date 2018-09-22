using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Community_Forum_Topics_Create : FudgePage {
    FudgeDataContext db = new FudgeDataContext();

    public Community_Forum_Topics_Create()
        : base(MenuItem.Community) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        
    }

    protected void newPost_ItemInserting(object sender, FormViewInsertEventArgs e) {
        Page.Validate();
        e.Cancel = !Page.IsValid;
        if (!e.Cancel) {
            //create a new topic
            var newTopic = new Topic {
                ForumId = Int32.Parse(Request.QueryString["id"]),
                Visible = true,
                UserId = Fudge.Framework.Database.User.LoggedInUser.UserId,
                Timestamp = DateTime.Now.ToUniversalTime(),
                Status = 0,
                Title = e.Values["Title"].ToString()
            };

            var newRating = new Rating {
                Sum = 0,
                Count = 0
            };

            //insert rating and topic for this post
            db.Ratings.InsertOnSubmit(newRating);
            db.Topics.InsertOnSubmit(newTopic);
            db.SubmitChanges();

            //assign values to new post
            e.Values["RatingId"] = newRating.RatingId;
            e.Values["TopicId"] = newTopic.TopicId;
            e.Values["Timestamp"] = DateTime.Now.ToUniversalTime();
            e.Values["Visible"] = true;
            e.Values["UserId"] = newTopic.UserId;
        }
    }

    protected void postsSource_Inserted(object sender, LinqDataSourceStatusEventArgs e) {        
        //insert topic subscription if user preference is set
        if (FudgeUser.IsOptionSet(Fudge.Framework.Database.User.UserOptions.AutomaticallySubscribeToMyTopics)) {
            FudgeUser.SubscribeForReplies((e.Result as Post).TopicId);            
        }        
        //redirect
        Response.Redirect("/Community/Forum/Topic/" + Request.QueryString["id"]);
    }
}
