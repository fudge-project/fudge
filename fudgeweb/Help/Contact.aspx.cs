using System;

public partial class Help_Contact : FudgePage {
    public Help_Contact()
        : base(MenuItem.Help, false) {

    }
    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Help.ContactUs()";
    }

    protected void sendEmail_Click(object sender, EventArgs e) {
        if (Util.SendEmail(email.Text, Util.AdminEmail, subject.SelectedItem.Text, FormatHelper.FormatPost(messageBody.Text), true)) {
            tip.RenderAsError = false;
            tip.Text = "Thanks for the feedback!";
            email.Text = messageBody.Text = String.Empty;
        }
        else {
            tip.RenderAsError = true;
            tip.Text = "There was an error sending feedback! Please check your email and try again.";
        }
        tip.Show();
    }
}
