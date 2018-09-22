using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class TestRunViewer : UserControl {        

        public TestRunViewer() {
            InitializeComponent();
        }

        private string ShowWhitespace(string text) {
            return text.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t").Replace("\\r\\n", "\\r\\n\r\n");
        }

        public void Update(string input, string expected, string output) {
            inputTextBox.Text = ShowWhitespace(input);
            expectedTextBox.Text = ShowWhitespace(expected);
            outputTextBox.Text = ShowWhitespace(output);
        }       
    }
}
