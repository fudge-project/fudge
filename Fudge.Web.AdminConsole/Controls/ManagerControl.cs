using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Windows.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public class ManagerControl : UserControl {
        public virtual FudgeDataContext DataContext { get; set; } 
        public virtual void Initialize() {}
    }
}
