using System;
using System.Web.UI;
using System.Linq;
using Fudge.Framework.Database;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

public partial class Problems_View : FudgePage {
    public Problem CurrentProblem {
        get {
            return Problem.GetProblemByUrlName(Request.QueryString["name"]);
        }
    }

    public List<int> TagIds {
        get {
            var tags = ViewState["tags"] as List<int>;
            if (tags == null) {
                tags = new List<int>();
                ViewState["tags"] = tags;
            }
            return tags;
        }
    }

    FudgeDataContext db = new FudgeDataContext();

    public Problems_View()
        : base(MenuItem.Problems, false) {
        //Verify that the id is an integer and a valid problem
        VerifyQueryString("name", name => Problem.GetProblemByUrlName(name) != null &&
            Problem.GetProblemByUrlName(name).Visible, "The problem requested is not valid");
    }

    protected void Page_Load(object sender, EventArgs e) {
        var scriptManager = ScriptManager.GetCurrent(this);
        UpdateTags();
        tagCloud.DataBind();
        if (!scriptManager.IsInAsyncPostBack) {
            Title += ".Problems[\"" + CurrentProblem.Name + "\"]";
            ProblemXml.ProblemId = CurrentProblem.ProblemId;

            if (FudgeUser != null) {
                problemRating.RatingId = CurrentProblem.RatingId;
            }
            else {
                shareProblem.Visible = false;
                tagProblem.Visible = false;
            }

            //show 5 most recent topics
            RecentTopics.DataSource = (from t in CurrentProblem.Forum.Topics
                                       from p in t.Posts
                                       orderby p.Timestamp descending
                                       group t by p.Topic into g
                                       select new { g.Key.TopicId, g.Key.Timestamp, g.Key.UserId }).Distinct().Take(5);
            RecentTopics.DataBind();
        }
    }

    protected void ShareIt(object sender, EventArgs e) {
        var badNames = new List<string>();
        var users = new List<User>();

        foreach (ListItem item in friendList.Items) {
            if (item.Selected) {
                users.Add(Fudge.Framework.Database.User.GetUserById(Int32.Parse(item.Value)));
            }
        }

        if (!users.Any()) {
            problemSharedTip.RenderAsError = true;
            problemSharedTip.Text = "No users selected.";
        }
        else {
            //resolve names        
            foreach (var user in users) {
                Notification.Notify(Notification.ShareProblem(user.UserId, CurrentProblem.ProblemId));

                Email.NotifyProblemShared.Send(user, "Check this problem out!",
                    FudgeUser.FirstName, CurrentProblem.UrlName);
            }
            problemSharedTip.RenderAsError = false;
            problemSharedTip.Text = "Problem shared successfully";
            shareProblemPanel.Visible = false;
        }
        problemSharedTip.Show();

        foreach (ListItem item in friendList.Items) {
            item.Selected = false;
        }
    }

    protected void OntheTopsSourceSelecting(object sender, LinqDataSourceSelectEventArgs e) {
        if (CurrentProblem.SolvedRuns.Any()) {
            var shortestRun = (from r in CurrentProblem.SolvedRuns
                               orderby r.Size
                               select new {
                                   Title = "Shortest Submission",
                                   Language = r.Language.Name,
                                   r.RunId,
                                   r.UserId,
                                   Value = String.Format("{0:0.00} kB", r.Size / 1024.0)
                               }).FirstOrDefault();

            if (CurrentProblem.IsArchived) {
                var mostLikedRun = (from r in CurrentProblem.SolvedRuns
                                    let popularity = r.Rating.Count == 0 ? 1 : 1 + (r.Rating.Sum / r.Rating.Count) * Math.Log10(r.Rating.Count + 1)
                                    orderby popularity descending
                                    select new {
                                        Title = "Most Popular Submission",
                                        Language = r.Language.Name,
                                        r.RunId,
                                        r.UserId,
                                        Value = String.Format("{0:0.00}", popularity)
                                    }).FirstOrDefault();
                e.Result = new[] { shortestRun, mostLikedRun };
            }
            else {
                e.Result = new[] { shortestRun };
            }
        }
        else {
            e.Result = new List<Run>();
        }
    }
    protected void userFriendsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = FudgeUser.ApprovedFriends;
    }

    protected void ShowSharePanel(object sender, EventArgs e) {
        shareProblemPanel.Visible = true;
        tagProblemPanel.Visible = false;
        TagIds.Clear();
    }

    protected void closeSharing_Click(object sender, ImageClickEventArgs e) {
        shareProblemPanel.Visible = false;
    }

    protected void ShowTagProblemPanel(object sender, EventArgs e) {
        shareProblemPanel.Visible = false;
        tagProblemPanel.Visible = true;
    }

    protected void CloseTagging(object sender, ImageClickEventArgs e) {
        tagProblemPanel.Visible = false;
        TagIds.Clear();
    }

    protected void problemCategories_SelectedIndexChanged(object sender, EventArgs e) {
        errorTagging.Hide();
        int tagId = Int32.Parse(problemCategories.SelectedValue);
        if (!TagIds.Contains(tagId)) {
            TagIds.Add(tagId);
            UpdateTags();
        }
    }

    protected void UpdateTags() {
        if (tagProblemPanel.Visible) {
            categories.Controls.Clear();
            foreach (var tagId in TagIds) {
                var tag = db.Tags.Single(t => t.TagId == tagId);
                LinkButton button = new LinkButton {
                    Text = tag.Keyword,
                    ID = tagId.ToString()
                };
                HtmlGenericControl span = new HtmlGenericControl("span");
                HtmlGenericControl img = new HtmlGenericControl("img");
                img.Attributes["src"] = @"/site/images/tag_blue.png";
                img.Attributes["align"] = "middle";
                span.Controls.Add(button);
                span.Controls.Add(new LiteralControl(" "));
                span.Controls.Add(img);
                categories.Controls.Add(span);
                button.Click += (s, e) => {
                    TagIds.Remove(Int32.Parse(((LinkButton)s).ID));
                    UpdateTags();
                };
            }
        }
    }


    protected void TagProblem(object sender, EventArgs e) {
        if (TagIds.Count == 0) {
            errorTagging.Show();
        }
        else {
            foreach (var tagId in TagIds) {
                if (!CurrentProblem.ProblemTags.Any(p => p.UserId == FudgeUser.UserId && p.TagId == tagId)) {
                    db.ProblemTags.InsertOnSubmit(new ProblemTag {
                        ProblemId = CurrentProblem.ProblemId,
                        TagId = tagId,
                        UserId = FudgeUser.UserId
                    });
                }
            }

            db.SubmitChanges();
            TagIds.Clear();
            tagProblemPanel.Visible = false;
            errorTagging.Hide();
            tagTip.Show();
        }
    }

    protected void tagSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        e.Result = from t in CurrentProblem.ProblemTags
                   group t by t.Tag into g
                   select new {
                       Url = "/Problems/Archive/Tags/" + g.Key.UrlName,
                       Count = g.Count(),
                       g.Key.Keyword
                   };
    }
}
