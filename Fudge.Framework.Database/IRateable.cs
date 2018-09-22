using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Framework.Database {
    public interface IRateable {
        DateTime Timestamp { get; }
        Rating Rating { get; set; }
        int AvgPoints { get; }
    }
}
