using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;
using System.Net.Mail;
using System.Net.Sockets;

namespace Fudge.Tasks.AdminTasks {
    public static class Emails {
        private const string MailServer = "mailhost.fit.edu";
        private static string AdminEmail = "fudge@fit.edu";
        private static MailAddress FudgeAdmin = new MailAddress(AdminEmail, "Fudge");

        public static string ActivationEmail = @"Hello {0},
Thank you for signing up with Fudge, the Florida Tech Online Judge.

To activate your account, follow the link below:
http://fudge.fit.edu/Users/Activate/{1}

Thanks,
The Fudge Team";

        public static string ContestNotificationEmail = @"Hello {0},
{1} is scheduled for {2} {3}

To view more details about the contest, follow the link below:
http://fudge.fit.edu/Contests/{4}

Thanks,
The Fudge Team";



        public static bool SendEmail(User user, string subject, string body) {
            try {
                SmtpClient client = new SmtpClient(MailServer, 25);
                MailMessage message = new MailMessage();
                message.From = FudgeAdmin;
                message.Subject = subject;
                //add recepient
                message.To.Add(user.Email);

                message.Body = body;

                client.Send(message);
            }
            catch (SocketException) {
                return false;
            }

            return true;
        }
    }
}
