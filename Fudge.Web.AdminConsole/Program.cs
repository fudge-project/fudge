using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Fudge.Tasks.NewsFeedTask;
using Fudge.Tasks.AdminTasks;

namespace Fudge.Web.AdminConsole {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            //Rankings.Update(DateTime.Now, false);
            //return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
