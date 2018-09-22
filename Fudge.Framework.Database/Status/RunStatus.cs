using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Fudge.Framework.Database {
    public enum RunStatus : int {
        Pending = 0,
        Compiling = 1,
        Running = 2,
        CompilationError = 3,
        Done = 4,
        InternalError = 5
    }
}
