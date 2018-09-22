using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Web.AdminConsole.Forms;
using Fudge.Framework.Database;
using System.Xml;
using System.IO;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class ProblemFormatter : UserControl {
        public override string Text { get { return textBox.Text; } set { textBox.Text = value; } }

        public FudgeDataContext DataContext { get; set; }

        public ProblemFormatter() {
            InitializeComponent();
        }

        public void Clear() {
            textBox.Clear();
        }

        private void AddSymbol(string symbol) {
            int selection = textBox.SelectionStart;
            int length = textBox.SelectionLength;

            textBox.Text = textBox.Text.Remove(selection, length);
            textBox.Text = textBox.Text.Insert(selection, symbol);

            textBox.SelectionStart = selection + symbol.Length;
            textBox.Focus();
        }

        private void AddTag(string tag, bool close) {
            int selection = textBox.SelectionStart;
            int length = textBox.SelectionLength;

            textBox.Text = textBox.Text.Insert(selection, "<" + tag + (close ? ">" : "/>"));

            if (close) {
                textBox.Text = textBox.Text.Insert(selection + tag.Length + 2 + length, "</" + tag + ">");
            }

            textBox.Select(selection + tag.Length + 2, length);
            textBox.Focus();
        }

        private void ftmPreButton_Click(object sender, EventArgs e) {
            AddTag("tt", true);
        }

        private void fmtIButton_Click(object sender, EventArgs e) {
            AddTag("i", true);
        }

        private void ftmSupButton_Click(object sender, EventArgs e) {
            AddTag("sup", true);
        }

        private void fmtSubButton_Click(object sender, EventArgs e) {
            AddTag("sub", true);
        }

        private void symbol_Click(object sender, EventArgs e) {
            AddSymbol(((ToolStripMenuItem)sender).Tag.ToString());
        }

        private void symbolsButton_Click(object sender, EventArgs e) {
            symbolContextMenuStrip.Show(((Control)sender), new Point(0, 0), ToolStripDropDownDirection.AboveRight);
        }

        private void imageButton_Click(object sender, EventArgs e) {
            PictureForm pictureForm = new PictureForm(DataContext);

            if (pictureForm.ShowDialog() == DialogResult.OK) {
                AddTag("img src=\"/Images/" + pictureForm.SelectedPicture.PictureId + "\"", false);
            }
        }
    }
}
