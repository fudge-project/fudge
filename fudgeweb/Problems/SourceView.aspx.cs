using System;
using System.Linq;
using Fudge.Framework.Database;

public partial class Problems_SourceView : FudgePage {
    public Problems_SourceView()
        : base(MenuItem.Problems, false) {
        VerifyQueryStringInt("id", id => db.Runs.Any(r => r.RunId == id), "No source code for this submission");
    }

    protected Run Run {
        get {
            return Run.GetRunById(Int32.Parse(Request.QueryString["id"]));
        }
    }

    FudgeDataContext db = new FudgeDataContext();

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Source[" + Run.RunId + "]";

        //setup rating and posts controls
        problemRating.RatingId = Run.RatingId;
        sourceComments.TopicId = Run.TopicId;

        if (Run.Problem.IsArchived || Run.User.IsLoggedOn) {
            //users can't rate their own code or rate unsolve submissions
            ratingPanel.Visible = Run.Solved && !Run.User.IsLoggedOn;
            //users can't comment on unsolved submission
            commentPanel.Visible = Run.Solved;

            //show popularity if the submission is solved
            if (Run.Solved) {
                popularity.Text = String.Format("Popularity {0:0.0}", Run.Rating.Popularity);
            }

            //set the source code and language name
            codeView.RunId = Run.RunId;
            string user = Run.User.FirstName + "'s";
            if (Run.User.IsLoggedOn) {
                user = "Your";
            }
            codeheader.Text = user + " solution to " + Html.LinkToProblem(Run.ProblemId) + " in " + Run.Language.SourceId;
        }
        else {
            tip.Show();
            codeView.Visible = ratingPanel.Visible = false;
        }
    }

    protected void CommentPosted(object sender, CommentPostedArgs e) {
        if (!Run.User.IsLoggedOn) {
            //notify the user
            Notification.Notify(Notification.SourcePost(Run.RunId));
        }

        foreach (var s in Run.Topic.TopicSubscriptions) {
            if (Run.UserId != s.UserId) {
                string subject = Run.User.FirstName + " replied to your post!";
                //Email.NotifyTopicReply.Send(s.User, subject, s.User.FirstName, "his source");
            }
        }
    }
}
