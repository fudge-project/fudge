using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Fudge.Framework.Setup.Sheets {
    public partial class DirectorySheet : Sheet {
        public DirectorySheet() {
            InitializeComponent();

            directoryTextBox.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Fudge Framework");
        }

        public override void Execute() {

        }

        private void browseButton_Click(object sender, EventArgs e) {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                directoryTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
