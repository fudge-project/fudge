using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Framework.Database {
    public partial class Post : IRateable {
        public int AvgPoints {
            get {
                return 1;
            }
        }
    }
}
