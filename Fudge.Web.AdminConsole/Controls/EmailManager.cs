using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using System.Net.Mail;
using Fudge.Web.AdminConsole.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class EmailManager : ManagerControl {
        public EmailManager() {
            InitializeComponent();
        }

        private void sendButton_Click(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(subjectTextBox.Text.Trim())) {
                MessageBox.Show("You must enter a subject line", "Send Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try {
                SmtpClient client = new EmailSettingsForm().GetClient();

                foreach (User user in DataContext.Users) {
                    if (user.IsOptionSet(User.UserOptions.NoEmailNotifications) || user.Status != UserStatus.Activated) {
                        continue;
                    }

                    string content = emailRichTextBox.Text;
                    content = content.Replace("{Name}", user.FirstName);

                    MailMessage message = new MailMessage(new MailAddress("fudge@fit.edu", "Fudge"), new MailAddress(user.Email, user.FullName));
                    message.Subject = subjectTextBox.Text;
                    message.Body = content;

                    client.Send(message);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Smtp Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
