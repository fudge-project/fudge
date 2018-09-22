using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using Fudge.Web.AdminConsole.Properties;
using System.Configuration;

namespace Fudge.Web.AdminConsole.Forms {
    public partial class ConnectForm : Form {

        public FudgeDataContext DataContext { get; set; }

        public ConnectForm() {
            InitializeComponent();
            
            userTextBox.Text = Settings.Default.SavedUser;
            passwordTextBox.Text = Settings.Default.SavedPassword;
            rememberCheckBox.Checked = Settings.Default.SavedCheckBox;

            userTextBox.Focus();
        }

        private void okButton_Click(object sender, EventArgs e) {

            DataContext = new FudgeDataContext(String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                                                            serverComboBox.Text, databaseComboBox.Text, userTextBox.Text, passwordTextBox.Text));

            if (!DataContext.DatabaseExists()) {                
                MessageBox.Show("An error occured while establishing the connection", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (rememberCheckBox.Checked) {                
                Settings.Default.SavedUser = userTextBox.Text;
                Settings.Default.SavedPassword = passwordTextBox.Text;
                Settings.Default.SavedCheckBox = true;                
            }
            else {
                Settings.Default.SavedUser = String.Empty;
                Settings.Default.SavedPassword = String.Empty;
                Settings.Default.SavedCheckBox = false;
            }

            Settings.Default.Save();

            DialogResult = DialogResult.OK;                                        
        }
    }
}
