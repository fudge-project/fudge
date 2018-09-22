using System;
using System.Linq;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Teams_Default : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Teams_Default()
        : base(MenuItem.Teams, false) {

    }

    protected int MemberCount(object userList) {
        //used by the markup to get team member count
        var users = userList as IQueryable<User>;
        return users.Count();
    }

    protected string FormatTeamDate(DateTime dateTime) {
        var info = FudgeUser == null ? TimeZoneInfo.Utc : FudgeUser.TimeZoneInfo;
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, info).ToString("MMMM dd, yyyy");
    }

    protected string GetDescription(TeamStatus status) {
        if (status == TeamStatus.Invitation) {
            return "Invite only.";
        }
        return "Open to everyone.";
    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Teams";
    }

    protected void teamsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        //users may see teams that they are not banned from and are not private
        var teams = from t in db.Teams.AsEnumerable()
                    let bannedUsers = t.TeamUsers.Where(tu => tu.Status == TeamUserStatus.Banned)
                    where t.Status != TeamStatus.Private && t.Status != TeamStatus.Closed
                    && (FudgeUser != null ? !bannedUsers.Any(tu =>  tu.UserId == FudgeUser.UserId) : true)
                    select t;
        //add search expression if the user wishes to search
        if (!String.IsNullOrEmpty(teamSearch.Text.Trim())) {
            teams = teams.Where(t => t.Name.Contains(teamSearch.Text.Trim()));
        }
        //ranomize search results
        e.Result = teams.ToRandom();
    }

    protected void Search_Click(object sender, EventArgs e) {
        //rebind the data
        teams.DataBind();
    }
}
