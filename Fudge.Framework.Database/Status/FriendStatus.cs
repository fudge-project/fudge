using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Fudge.Framework.Database {
    public enum FriendStatus : int {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }
}
