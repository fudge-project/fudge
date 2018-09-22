using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Security;

namespace Fudge.Framework.Helper {
    public partial class Helper : ServiceBase {
        public Helper() {
            InitializeComponent();

            ServiceName = "Fudge Framework Helper Service";
            EventLog.Log = "Fudge";
        }

        protected override void OnStart(string[] args) {
            Process fx = new Process();

            fx.StartInfo.FileName = Path.Combine(Framework.GetDirectory(), "fudgefx.exe");

            //fx.StartInfo.UserName = Framework.GetCompilerUserName();
            //fx.StartInfo.Password = Framework.GetCompilerUserPassword();
            
            fx.Start();
        }

        protected override void OnStop() {
        }
    }
}
