using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Fudge.Framework.Database {
    public enum TeamStatus : int {
        Open = 0,
        Invitation = 1,
        Private = 2,
        Closed = 3
    }

    public enum TeamScope : int {
        Global = 0,        
        Country = 2,
        School = 3,
        Friends = 4
    }
}
