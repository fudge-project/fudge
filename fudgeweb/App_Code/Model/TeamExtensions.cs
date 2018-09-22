using System;
using System.Data.Linq;
using System.Linq;
using Fudge.Framework.Database;

/// <summary>
/// Summary description for TeamExtensions
/// </summary>
public static class TeamExtensions {

    public static Func<FudgeDataContext, int, int, TeamUser> FindTeamUser = CompiledQuery.Compile(
        (FudgeDataContext db, int userId, int teamId) =>
            (from u in db.TeamUsers
             where u.UserId == userId
             && u.TeamId == teamId
             select u).FirstOrDefault()
        );

    public static void InviteMember(this Team team, int userId) {
        FudgeDataContext db = new FudgeDataContext();

        db.TeamUsers.InsertOnSubmit(new TeamUser {
            Status = TeamUserStatus.Invited,
            TeamId = team.TeamId,
            UserId = userId
        });

        Notification.Notify(Notification.InviteToTeam(userId, team.TeamId));
        db.SubmitChanges();

        Email.NotifyTeamInvite.Send(User.GetUserById(userId), "Join " + team.Name + "!",
            User.LoggedInUser.FirstName, team.Name, team.TeamId);
        
    }

    public static void ChangeUserStatus(this Team team, int userId, TeamUserStatus status) {
        FudgeDataContext db = new FudgeDataContext();

        var teamUser = FindTeamUser(db, userId, team.TeamId);
        teamUser.Status = status;

        db.SubmitChanges();
    }

    public static TeamUserStatus GetUserStatus(this Team team, int userId) {
        var teamUser = team.TeamUsers.FirstOrDefault(u => u.UserId == userId);
        return teamUser.Status;
    }
}
