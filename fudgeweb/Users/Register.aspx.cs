using System;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;
using Resources;

public partial class Register_Default : FudgePage {
    public Register_Default()
        : base(MenuItem.MyProfile, false) {

    }

    protected User Referrer {
        get {
            if (Request.IsQueryStringNull("ref")) {
                return null;
            }
            return db.Users.SingleOrDefault(u => u.ReferralCode == Request.QueryString["ref"]);
        }
    }

    FudgeDataContext db = new FudgeDataContext();

    protected void Page_Init(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (Request.Cookies["activation_cookie"] != null) {
                //user has not activated as yet
                notActivated.Visible = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        //enable page methods for this page
        //ScriptManager.GetCurrent(this).EnablePageMethods = true;
        Form.DefaultButton = userFormView.FindControl("registerUser").UniqueID;

        Title += ".Register()";

        //setup the validators
        CustomValidator shortNameValidator = userFormView.FindControl<CustomValidator>("shortNameValidator");
        shortNameValidator.ValidateData((fdb, arg) => !fdb.Users.Any(u => u.ShortName == arg),
            arg => Resource.UserExists);

        CustomValidator emailValidator = userFormView.FindControl<CustomValidator>("emailValidator");
        emailValidator.ValidateData((fdb, arg) => !fdb.Users.Any(u => u.Email == arg),
                                arg => Resource.UserExists);

        CustomValidator schoolValidator = userFormView.FindControl<CustomValidator>("schoolValidator");
        schoolValidator.Validate(sid => {
            int res;
            return Int32.TryParse(userFormView.FindControl<HiddenField>("locatedSchool").Value, out res);
        });

        if (!Page.IsPostBack) {
            var countries = userFormView.FindControl<DropDownList>("countries");
            countries.DataSource = db.Countries.OrderBy(c => c.Name);
            countries.DataBind();

            //check the school domain
            TextBox email = userFormView.FindControl<TextBox>("email");
            //check for school on both events
            email.Attributes["onblur"] = "checkifEnough()";
            email.Attributes["onkeyup"] = "checkifEnough()";
        }
    }

    [WebMethod]
    public static object FindSchool(string email) {
        FudgeDataContext db = new FudgeDataContext();

        //break the email into parts separated by '.'
        int at = email.IndexOf("@");
        string domain = email.Substring(at + 1);
        string[] parts = domain.Split('.');
        if (parts.Length < 2) {
            return null;
        }


        //return the first school that matches the email domain
        var school = (from s in db.Schools
                      let wc = "%" + s.Domain
                      where SqlMethods.Like(email, wc)
                      select new { s.Name, s.CountryId, s.SchoolId }).SingleOrDefault();
        //if the list is empty return an empty list
        return school;
    }

    protected void OnUserFormViewItemInserting(object sender, FormViewInsertEventArgs e) {
        //validate the page
        Page.Validate();

        e.Cancel = !Page.IsValid;

        if (!e.Cancel) {
            e.Values["Password"] = e.Values["Password"].ToString().ToMD5();
            e.Values["SchoolId"] = Int32.Parse(userFormView.FindControl<HiddenField>("locatedSchool").Value);
            e.Values["Timestamp"] = DateTime.Now.ToUniversalTime();
            
            string code = Util.GenerateActivationCode();
            do {
                code = Util.GenerateActivationCode();
                e.Values["ActivationCode"] = code;                
            }
            while (db.Users.Any(u => u.ActivationCode == code));

            e.Values["CountryId"] = Int32.Parse(userFormView.FindControl<DropDownList>("countries").SelectedValue);
            e.Values["ReferralCode"] = Util.GenerateRandomString(20);
            e.Values["GlobalRank"] = db.Users.Max(u => u.GlobalRank);
            e.Values["SecondaryEmail"] = e.Values["Email"];            

            string firstName = (string)e.Values["FirstName"];
            string lastName = (string)e.Values["LastName"];
            //get the school
            var school = db.Schools.SingleOrDefault(s => s.SchoolId == Convert.ToInt32(e.Values["SchoolId"]));

            var schoolUsers = (from u in db.Users
                               where u.SchoolId == school.SchoolId
                               select u);
            e.Values["SchoolRank"] = schoolUsers.Any() ? schoolUsers.Max(u => u.SchoolRank) : 1;

            var countryUsers = (from u in db.Users
                                where u.CountryId == Convert.ToInt32(e.Values["CountryId"])
                                select u);

            e.Values["CountryRank"] = countryUsers.Any() ? countryUsers.Max(u => u.CountryRank) : 1;

            var avatar = userFormView.FindControl<Controls_ImageUpload>("avatar");
            if (avatar.HasFile) {                                
                e.Values["PictureId"] = Picture.CreateFrom(firstName + "'s Avatar", avatar.ImageBytes);
            }
            else {
                e.Values["PictureId"] = Picture.ProfileDefaultPicture;
            }
        }
    }

    protected void OnLinqDataSource1Inserted(object sender, LinqDataSourceStatusEventArgs e) {
        userFormView.Visible = false;
        notActivated.Visible = false;
        var registeredUser = e.Result as User;

        //update the topic id for this user
        var user = db.Users.SingleOrDefault(u => u.UserId == registeredUser.UserId);

        //set default flags
        user.OptionFlag |= (int)Fudge.Framework.Database.User.UserOptions.AutomaticallySubscribeToMyTopics;
        user.OptionFlag |= (int)Fudge.Framework.Database.User.UserOptions.AutomaticallySubscribeToTopicsIReplyTo;
                 
        user.TopicId = Topic.CreateStackTopic(registeredUser.UserId, "Stack for UserId " + registeredUser.UserId);
        db.SubmitChanges();

        if (Util.SendActivationEmail(registeredUser)) {
            //add activation cookie
            var cookie = new HttpCookie("activation_cookie");
            cookie.Expires = DateTime.Now.AddDays(3);
            Response.Cookies.Add(cookie);

            if (Referrer != null) {
                Referrer.ReferralCount++;
                Referrer.AddFriend(registeredUser.UserId);
                db.SubmitChanges();
            }
            regSuccess.Visible = true;
        }
        else {
            regFailed.Visible = true;
        }
    }
}