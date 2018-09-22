using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using Fudge.Web.AdminConsole.Properties;
using System.Net;

namespace Fudge.Web.AdminConsole.Forms {
    public partial class EmailSettingsForm : Form {
        public EmailSettingsForm() {
            InitializeComponent();

            serverTextBox.Text = Settings.Default.SmtpServer;
            portTextBox.Value = Settings.Default.SmtpPort;
            userTextBox.Text = Settings.Default.SmtpUser;
            sslCheckBox.Checked = Settings.Default.SmtpUseSsl;
            rememberCheckBox.Checked = Settings.Default.SmtpRemember;

            if (Settings.Default.SmtpRemember) {
                passwordTextBox.Text = Settings.Default.SmtpPassword;
            }
        }

        public SmtpClient GetClient() {
            if (!Settings.Default.SmtpRemember) {
                if (ShowDialog() != DialogResult.OK) {
                    return null;
                }
            }

            SmtpClient client = new SmtpClient();
            client.Host = serverTextBox.Text;
            client.Port = portTextBox.Value;
            client.Credentials = new NetworkCredential(userTextBox.Text, passwordTextBox.Text);
            client.EnableSsl = sslCheckBox.Checked;

            return client;
        }

        private void okButton_Click(object sender, EventArgs e) {
            Settings.Default.SmtpServer = serverTextBox.Text;
            Settings.Default.SmtpPort = portTextBox.Value;
            Settings.Default.SmtpUser = userTextBox.Text;
            Settings.Default.SmtpUseSsl = sslCheckBox.Checked;
            Settings.Default.SmtpRemember = rememberCheckBox.Checked;

            if (rememberCheckBox.Checked) {
                Settings.Default.SmtpPassword = passwordTextBox.Text;
            }
            else {
                Settings.Default.SmtpPassword = String.Empty;
            }

            Settings.Default.Save();

            DialogResult = DialogResult.OK;
        }
    }
}
