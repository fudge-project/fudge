using Resources;
using Fudge.Framework.Database;
using System;

/// <summary>
/// Summary description for Emails
/// </summary>
public class Email {
    //website emails
    public static Email WelcomeEmail = new Email(Resource.WelcomeEmail);
    public static Email InviteEmail = new Email(Resource.InviteEmail);
    public static Email AddFriendEmail = new Email(Resource.AddFriendEmail);
    public static Email ForgotPasswordEmail = new Email(Resource.ForgotPasswordEmail);

    //notification emails
    public static Email NotifySourcePost = new Email(Resource.NotifySourcePost);
    public static Email NotifyProblemShared = new Email(Resource.NotifyProblemShared);
    public static Email NotifyStackPost = new Email(Resource.NotifyStackPost);
    public static Email NotifyTeamInvite = new Email(Resource.NotifyTeamInvite);
    public static Email NotifyTopicReply = new Email(Resource.NotifyTopicReply);
    public static Email NotifyNotMyTopicReply = new Email(Resource.NotifyNotMyTopicReply);
    public static Email NotifyBlogPost = new Email(Resource.NotifyBlogPost);
    public static Email NotifyTeamPost = new Email(Resource.NotifyTeamPost);

    //admin emails
    public static Email NewSchoolEmail = new Email(Resource.NewSchoolEmail);
    public static Email NewUserEmail = new Email(Resource.NewUserEmail);

    public string Content { get; private set; }

    public Email(string content) {
        Content = content;
    }

    public bool Send(User user, string subject, params object[] args) {
        return Util.SendEmail(user, this, subject, args);
    }

    public bool SendToAdmin(string subject, params object[] args) {
        return Send(Util.AdminEmail, Util.AdminEmail, subject, args);
    }

    public bool Send(string to, string from, string subject, params object[] args) {
        return Util.SendEmail(from, to, subject, String.Format(Content, args), false);
    }
}
