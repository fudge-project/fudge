using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Setup.Sheets;

namespace Fudge.Framework.Setup {
    public partial class MainForm : Form {

        List<Sheet> _sheets = new List<Sheet>();
        int _currentSheet = 0;

        public MainForm() {
            InitializeComponent();

            _sheets.Add(new DirectorySheet());
            _sheets.Add(new UsersSheet());
            _sheets.Add(new CompilersSheet());

            containerPanel.Controls.Add(_sheets[0]);
            UpdateNavigation();
        }

        private void UpdateNavigation() {
            if (_sheets.Count == _currentSheet + 1) {
                nextButton.Text = "&Finished";
            }
            else {
                nextButton.Text = "&Next";
            }

            previousButton.Enabled = _currentSheet != 0;   
        }

        private void nextButton_Click(object sender, EventArgs e) {
            if (_sheets.Count == _currentSheet + 1) {
                foreach (Sheet sheet in _sheets) {
                    sheet.Execute();
                }

                previousButton.Enabled = false;
                nextButton.Enabled = false;

                _currentSheet = -1;
            }
            else if (_sheets[_currentSheet].ValidateData()) {
                containerPanel.Controls.Clear();
                containerPanel.Controls.Add(_sheets[++_currentSheet]);

                UpdateNavigation();
            }                 
        }

        private void previousButton_Click(object sender, EventArgs e) {
            containerPanel.Controls.Clear();
            containerPanel.Controls.Add(_sheets[--_currentSheet]);

            UpdateNavigation();
        }

        private void exitButton_Click(object sender, EventArgs e) {
            if (_currentSheet == -1 || MessageBox.Show("Are you sure you want to cancel setup?", "Confirm Cancel Setup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                Close();
            }
        }
    }
}
