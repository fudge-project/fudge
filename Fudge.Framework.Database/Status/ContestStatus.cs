using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Fudge.Framework.Database {
    public enum ContestStatus : int {
        Open = 0,
        Invitation = 1,
        Private = 2,
        Closed = 3
    }

    public enum ContestScope : int {
        Global = 0,
        Region = 1,
        Country = 2,
        School = 3,
        Friends = 4
    }
   
    public enum ContestScoring : int {
        DeferredJudging = 1,
        TestCaseScoring = 2,        
    }
}
