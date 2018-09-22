using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Fudge.Modules.TaskManager.Gui {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            new TaskManagerForm().Initialize();
        }
    }
}
