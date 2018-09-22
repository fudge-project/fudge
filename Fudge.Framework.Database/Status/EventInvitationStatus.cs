using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Fudge.Framework.Database {
    public enum EventInvitationStatus : int {
        Pending = 0,
        Attending = 1,
        Undecided = 2,
        Rejected = 3
    }
}
