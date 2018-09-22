using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace Fudge.Web.AdminConsole {
    public partial class ParseForm : Form {

        public struct TestCase {
            public string input;
            public string output;
        }

        public ParseForm() {
            InitializeComponent();
        }

        public TestCase[] GetTestCases() {
            if (ShowDialog() == DialogResult.OK) {

                string[] inputs = inputParser.GetResults();
                string[] outputs = outputParser.GetResults();

                if (inputs.Length != outputs.Length) {
                    if (MessageBox.Show("Then number of inputs and outputs do not match. Only properly paired testcases will be added", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) {
                        return GetTestCases();
                    }
                }

                TestCase[] testCases = new TestCase[Math.Min(inputs.Length, outputs.Length)];

                for (int i = 0; i < testCases.Length; ++i) {
                    testCases[i] = new TestCase() { input = inputs[i], output = outputs[i] };
                }

                return testCases;
            }

            return new TestCase[] { };
        }

        private void okButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }
    }
}
