using System;
using System.Linq;
using Extensions;
using Fudge.Framework.Database;
using Resources;


public partial class Register_ForgotPassword : FudgePage {
    public Register_ForgotPassword()
        : base(MenuItem.MyProfile, false) {

    }    

    protected void Page_Load(object sender, EventArgs e) {        
        //set the default button for this page
        Form.DefaultButton = sendPassword.UniqueID;

        Title += ".ForgotPassword()";

        //setup validators
        emailValidator.ValidateData((db, value) => db.Users.Any(u => u.Email == value)
        , value => String.Format(Resource.UserDoesNotExist, Html.Link("/Users/Register", "here")));
    }

    protected void OnSendPasswordClick(object sender, EventArgs e) {
        FudgeDataContext db = new FudgeDataContext();
        var user = db.Users.SingleOrDefault(u => u.Email == email.Text);
        //if user exists
        if (user != null) {
            //generate a code for this reset password and save to the database
            user.ActivationCode = Util.GenerateActivationCode();
            db.SubmitChanges();

            resetPanel.Visible = false;
            if (user.Status != UserStatus.Activated) {
                message.Visible = true;
                if (Util.SendActivationEmail(user)) {                    
                    message.InnerText = String.Format(Resource.AccountNotActivatedReset, user.Email);
                }
                else {
                    message.InnerText = String.Format(Resource.SentEmailSuccessfully, user.Email);
                }
            }
            else {
                message.Visible = true;
                //send the user a forgot password email
                if (Util.SendForgotPassword(user)) {
                    message.InnerText = String.Format(Resource.SentEmailSuccessfully, user.Email);
                }
                else {                    
                    message.Attributes["class"] = "error";
                    message.InnerText = String.Format("Failed to send an email to {0}", user.Email);
                }
            }
        }
        
    }
}
