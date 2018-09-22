using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Framework.Setup.Sheets {
    public class Sheet : UserControl {

        public virtual bool ValidateData() { return true; }
        public virtual void Execute() {}
    }
}
