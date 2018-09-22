using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Fudge.Framework.Database {
    public enum TeamUserStatus : int {
        Requested = 0,
        Invited = 1,
        RejectedRequest = 2,
        RejectedInvite = 3,
        Banned = 4,
        Member = 5,
        Admin = 6
    }
}
