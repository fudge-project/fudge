using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using System.IO;
using System.Data.Linq;

namespace Fudge.Web.AdminConsole.Forms {
    public partial class PictureEditForm : Form {

        private Picture Picture;        

        public PictureEditForm() {
            InitializeComponent();
        }

        public DialogResult ShowDialog(Picture picture) {
            Picture = picture;

            titleTextBox.Text = picture.Title;            

            if (picture.Data != null) {
                fileTextBox.Text = "[Image in database]";
                fileTextBox.Tag = picture.Data;
                fileTextBox.Font = new System.Drawing.Font(fileTextBox.Font, System.Drawing.FontStyle.Italic);
                fileTextBox.ForeColor = System.Drawing.Color.LightGray;
            }

            return ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e) {         
            Picture.Title = titleTextBox.Text;            

            if (fileTextBox.Tag == null) {
                if (!File.Exists(fileTextBox.Text.Trim())) {
                    MessageBox.Show("The file path cannot be found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Picture.Data = new Binary(File.ReadAllBytes(fileTextBox.Text.Trim()));
            }

            DialogResult = DialogResult.OK;
        }

        private void browseButton_Click(object sender, EventArgs e) {
            if (openImageDialog.ShowDialog() == DialogResult.OK) {
                fileTextBox.Text = openImageDialog.FileName;
            }
        }

        private void fileTextBox_TextChanged(object sender, EventArgs e) {
            if(fileTextBox.Tag != null) {
                fileTextBox.Tag = null;
                fileTextBox.Font = new System.Drawing.Font(fileTextBox.Font, System.Drawing.FontStyle.Regular);
                fileTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void fileTextBox_Enter(object sender, EventArgs e) {
            if (fileTextBox.Tag != null) {
                fileTextBox.Text = String.Empty;
            }
        }
    }
}
