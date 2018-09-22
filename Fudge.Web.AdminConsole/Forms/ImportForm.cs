using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fudge.Web.AdminConsole {
    public partial class ImportForm : Form {
        public ImportForm() {
            InitializeComponent();
        }

        public Fudge.Web.AdminConsole.ParseForm.TestCase[] GetTestCases() {
            List<Fudge.Web.AdminConsole.ParseForm.TestCase> testCases = new List<ParseForm.TestCase>();

            if (ShowDialog() == DialogResult.OK) {              

                if (inputListBox.Items.Count != outputListBox.Items.Count && MessageBox.Show("Inputs and outputs are unmatched. Do you want to add only matched cases?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) {
                    return GetTestCases();
                }

                for (int i = 0; i < Math.Min(inputListBox.Items.Count, outputListBox.Items.Count); ++i) {
                    testCases.Add(new Fudge.Web.AdminConsole.ParseForm.TestCase { input = inputListBox.Items[i].ToString(), output = outputListBox.Items[i].ToString() });
                }
            }

            return testCases.ToArray();          
        }

        private void importInputButton_Click(object sender, EventArgs e) {
            if (importFileDialog.ShowDialog() == DialogResult.OK) {
                foreach (string fileName in importFileDialog.FileNames) {
                    StreamReader file = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
                    inputListBox.Items.Add(file.ReadToEnd());
                    file.Close();
                }
            }
        }

        private void importOutputButton_Click(object sender, EventArgs e) {
            if (importFileDialog.ShowDialog() == DialogResult.OK) {
                foreach (string fileName in importFileDialog.FileNames) {
                    StreamReader file = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read));
                    outputListBox.Items.Add(file.ReadToEnd());
                    file.Close();
                }
            }
        }
    }
}
