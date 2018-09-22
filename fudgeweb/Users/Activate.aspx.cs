using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Fudge;
using Resources;
using Fudge.Framework.Database;

public partial class Register_Activation : FudgePage {
    FudgeDataContext db = new FudgeDataContext();

    public Register_Activation()
        : base(MenuItem.MyProfile, false) {
        VerifyQueryString("code", code => db.Users.Any(u => u.ActivationCode == code), "Invalid activation code!");
    }

    protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack) {
            var user = db.Users.SingleOrDefault(u => u.ActivationCode == Request.QueryString["code"]);
            if (Request.Cookies["activation_cookie"] != null) {
                var cookie = Response.Cookies.Get("activation_cookie");
                //make the cookie expire
                cookie.Expires = DateTime.Now.AddDays(-1);
            }

            Title += ".ActivateUser(" + user.FullName + ")";

            //set this account as activated
            user.Status = UserStatus.Activated;
            //remove the activation code
            user.ActivationCode = null;
            //update the database
            db.SubmitChanges();

            //first user to from a school
            if (user.School.Users.Count == 1) {                
                db.NewsFeeds.InsertOnSubmit(new NewsFeed {
                    Text = String.Format("<strong>{0} is the first user to join from {1}!</strong>", Html.LinkToProfile(user.UserId), Html.LinkToSchoolProfile(user.School.SchoolId)),
                    Link = "http://fudge.fit.edu/Users/Profile/" + user.UserId,
                    Timestamp = DateTime.UtcNow,
                    Type = NewsFeedType.FirstUserFromSchool
                });
                db.SubmitChanges();
            }

            Email.NewUserEmail.SendToAdmin("New user joined Fudge!", user.FullName, user.School.Name);
        }
    }
}
