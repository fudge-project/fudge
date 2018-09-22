using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace Fudge.Framework.Database {
    public enum TestRunStatus {
        PresentationError,
        RuntimeError,
        TimeLimitExceeded,
        MemoryLimitExceeded,
        OutputLimitExceeded,
        Accepted,
        WrongAnswer
    }
}
