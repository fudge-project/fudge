using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Framework.Database {
    public partial class Team {
        FudgeDataContext db = new FudgeDataContext();
        public IQueryable<User> Members {
            get {                
                var users = from tu in TeamUsers
                            where tu.Status == TeamUserStatus.Member ||
                            tu.Status == TeamUserStatus.Admin
                            select tu.User;
                return users.AsQueryable();
            }
        }

        public static Team GetTeamById(int id) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Teams.SingleOrDefault(t => t.TeamId == id);
        }
    }
}
