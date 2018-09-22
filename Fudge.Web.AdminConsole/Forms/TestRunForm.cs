using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Web.AdminConsole {
    public partial class TestRunForm : Form {
        public TestRunForm() {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string input, string expected, string output) {
            testRunViewer.Update(input, expected, output);

            return ShowDialog();
        }
    }
}
