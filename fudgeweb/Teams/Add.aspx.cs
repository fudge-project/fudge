using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;
using System.Data.Linq;
using System.IO;

public partial class Teams_Add : FudgePage {
    public Teams_Add()
        : base(MenuItem.Teams) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Teams.Add()";                
    }

    protected void newTeamForm_ItemInserting(object sender, FormViewInsertEventArgs e) {
        Page.Validate();
        e.Cancel = !Page.IsValid;
        if (!e.Cancel) {
            e.Values["TopicId"] = Topic.CreateStackTopic("Message Pump for " + e.Values["Name"]);
            e.Values["Timestamp"] = DateTime.Now.ToUniversalTime();
            e.Values["Scope"] = newTeamForm.FindControl<DropDownList>("scope").SelectedIndex;
            e.Values["Status"] = newTeamForm.FindControl<DropDownList>("privacy").SelectedIndex;
            e.Values["UserId"] = FudgeUser.UserId;

            var avatar = newTeamForm.FindControl<Controls_ImageUpload>("avatar");
            if (avatar.HasFile) {
                e.Values["PictureId"] = Picture.CreateFrom(e.Values["Name"] + "'s Avatar", avatar.ImageBytes);
            }
            else {
                e.Values["PictureId"] = Picture.TeamDefaultPicture;
            }
        }
    }

    protected void teamSource_Inserted(object sender, LinqDataSourceStatusEventArgs e) {
        var team = e.Result as Team;

        TeamUser creator = new TeamUser {
            Status = TeamUserStatus.Admin,
            TeamId = team.TeamId,
            UserId = FudgeUser.UserId,
            Title = null
        };

        FudgeDataContext db = new FudgeDataContext();
        db.TeamUsers.InsertOnSubmit(creator);
        db.SubmitChanges();

        addTeamPanel.Visible = false;
        message.Visible = true;
        message.InnerHtml = String.Format("Your team has been created, go to the {0} start inviting members.",
            Html.Link("/Teams/" + team.TeamId, "team profile"));
    }
}
