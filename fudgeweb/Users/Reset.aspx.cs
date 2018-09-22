using System;
using System.Linq;
using System.Web.UI;
using Fudge;
using Resources;
using Fudge.Framework.Database;

public partial class Register_ResetPassword : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Register_ResetPassword()
        : base(MenuItem.MyProfile, false) {

        VerifyQueryString("code", code => db.Users.SingleOrDefault(u => u.ActivationCode == code) != null, "Invalid activation code");
    }

    protected void Page_Load(object sender, EventArgs e) {
        //the default button for this page is change password
        Form.DefaultButton = changePassword.UniqueID;
        Title += ".ResetPassword()";

        if (!Page.IsPostBack) {

            var user = db.Users.SingleOrDefault(u => u.ActivationCode == Request.QueryString["code"]);

            //if this activation code does not exists
            if (user == null) {
                resetPanel.Visible = false;
                resetText.Visible = true;
                resetText.Attributes["class"] = "error";
                resetText.InnerText = Resource.PageExpired;
            }
            else if (user.Status != UserStatus.Activated) {
                resetPanel.Visible = false;
                //if the user exists but the account has not been activated                     
                resetText.Visible = true;
                resetText.Attributes["class"] = "error";
                resetText.InnerText = Resource.AccountNotActivated;
            }
        }
    }

    protected void OnPasswordChangedClick(object sender, EventArgs e) {
        var user = db.Users.SingleOrDefault(u => u.ActivationCode == Request.QueryString["code"] && u.Status == UserStatus.Activated);

        if (user != null) {            
            //change the password for this user
            user.Password = password.Text.ToMD5();
            //reset the activation code
            user.ActivationCode = null;
            db.SubmitChanges();

            resetPanel.Visible = false;
            resetText.Visible = true;
            resetText.Attributes["class"] = "fudge_message";
            resetText.InnerText = Resource.PasswordChanged;
        }
        else {
            resetText.Attributes["class"] = "error";
            resetText.InnerText = "The password you entered is invalid";
        }
    }
}
