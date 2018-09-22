using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;

namespace Fudge.Web.AdminConsole {
    public partial class VerifyForm : Form {        

        public VerifyForm(FudgeDataContext dataContext) {
            InitializeComponent();

            problemVerifier.DataContext = dataContext;
            problemVerifier.Initialize();
        }

        private void runButton_Click(object sender, EventArgs e) {
            problemVerifier.Verify();
        }

        public DialogResult ShowDialog(int timeLimit, int memoryLimit, string[] inputs, string[] outputs) {
            problemVerifier.AddTestCases(inputs, outputs);
            problemVerifier.TimeLimit = timeLimit;
            problemVerifier.MemoryLimit = memoryLimit;

            return ShowDialog();
        }
    }
}
