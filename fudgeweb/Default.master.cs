using System;
using System.Linq;
using Fudge.Framework.Database;
using System.Net;
using System.Web;
using System.Web.Services;

public partial class _default : System.Web.UI.MasterPage {
    FudgeDataContext db = new FudgeDataContext();
    public bool LoginStatus {
        get {
            return Session["user"] != null;
        }
        set {
            if (value) {
                loginPanel.Visible = false;
                logout.Visible = true;
                loginName.Visible = true;
                loginName.Text = String.Format("Welcome {0} |", ((User)Session["user"]).FirstName);
                reglink.HRef = "/Users/Profile";
                reglink.InnerText = "My Profile";
            }
            else {
                loginPanel.Visible = true;
                loginName.Visible = false;
                loginName.Text = String.Empty;
                reglink.HRef = "/Users/Register";
                reglink.InnerText = "Registration";
            }
        }
    }

    public void HideLoginPanel() {
        loginPanel.Visible = false;
        loginName.Visible = false;
        loginName.Text = String.Empty;
    }

    //setup cookies
    protected void Page_Init(object sender, EventArgs e) {
        if (!IsPostBack) {
            if (Request.Cookies["remember_me"] != null) {
                var cookie = Request.Cookies.Get("remember_me");
                if (Session["user"] == null) {
                    User cookieUser = User.GetUserById(Int32.Parse(cookie.Values["userid"]));
                    if (cookieUser.Status != UserStatus.Banned) {
                        Session.Add("user", cookieUser);
                        LoginStatus = true;
                    }
                }

            }
            else if (Request.Cookies["user"] != null) {
                var cookie = Request.Cookies.Get("user");
                email.Text = cookie.Values["name"];
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        if (LoginStatus) {
            //add to list of logged in users
            var user = Session["user"] as User;
            UserExtensions.OnlineUsers.Add(user.UserId);

            LoginStatus = true;
        }
        else {
            LoginStatus = false;
        }
    }

    protected void OnLoginButtonClick(object sender, EventArgs e) {
        //find the user account in the database
        var user = db.Users.SingleOrDefault(u => u.Email == email.Text
                                   && u.Password == password.Text.ToMD5()
                                   && u.Status == UserStatus.Activated);

        if (Request.Cookies["user"] != null) {
            //expire this cookie if it exists
            var currentUser = Request.Cookies.Get("user");
            currentUser.Expires = DateTime.Now.AddDays(-1);
        }

        //create new cookie
        var cookie = new HttpCookie("user");
        cookie.Values.Add("name", email.Text);
        cookie.Expires = DateTime.Now.AddDays(15);
        Response.Cookies.Add(cookie);


        //if a user exists
        if (user != null) {
            //store it in the session            
            Session.Add("user", user);
            LoginStatus = true;


            var rememberMeCookie = new HttpCookie("remember_me");

            if (rememberMe.Checked) {
                Int32 persistDays = 15;
                rememberMeCookie.Values.Add("userid", user.UserId.ToString());
                rememberMeCookie.Expires = DateTime.Now.AddDays(persistDays); //you can add years and months too here
            }
            else {
                rememberMeCookie.Values.Add("username", String.Empty);
                rememberMeCookie.Values.Add("userid", String.Empty);
                rememberMeCookie.Expires = DateTime.Now.AddDays(-1); //you can add years and months too here
            }

            Response.Cookies.Add(rememberMeCookie);

            string url = (string)Session["requested_url"];
            Session["requested_url"] = null;
            Response.Redirect(url ?? "~/Default.aspx");
        }
        else {
            LoginStatus = false;
            Response.Redirect("~/LoginFailed");
        }
    }

    protected void OnLogoutClick(object sender, EventArgs e) {
        //remove from loggen in users        
        //remove the session variable
        Session.Remove("user");
        Session.Remove("requested_url");

        //abandon the session
        Session.Abandon();

        //if you logout delete the cookie
        if (Request.Cookies["remember_me"] != null) {
            var cookie = Response.Cookies.Get("remember_me");
            //make the cookie expire
            cookie.Expires = DateTime.Now.AddDays(-1);
            LoginStatus = false;
        }

        Response.Redirect("~/Default.aspx");
    }
}
