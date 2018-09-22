<%@ Application Language="C#" %>

<script RunAt="server">
    
    void Application_BeginRequest(object sender, EventArgs e) {
                
    }

    void Application_Start(object sender, EventArgs e) {

    }

    void Application_End(object sender, EventArgs e) {
        //clear online users
        Fudge.Framework.Database.UserExtensions.OnlineUsers.Clear();
    }

    void Application_Error(object sender, EventArgs e) {

#if !DEBUG

        var ex = Server.GetLastError().GetBaseException();
        // Code that runs when an unhandled error occurs
        Util.SendEmail(Util.AdminEmail, Util.AdminEmail, "Exception Thrown", String.Format(
@"Path: {0}

Message :
{1}

User:
{2}

Source:
{3}

Stack Trace:
{4}
", Request.Url.PathAndQuery, ex.Message, Fudge.Framework.Database.User.LoggedInUser != null ? Fudge.Framework.Database.User.LoggedInUser.DisplayName : Request.UserHostName, ex.Source, ex.StackTrace), false);
#endif

    }

    void Session_Start(object sender, EventArgs e) {
        // Code that runs when a new session is started   
    }

    void Session_End(object sender, EventArgs e) {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        var user = Session["user"] as Fudge.Framework.Database.User;
        Fudge.Framework.Database.UserExtensions.OnlineUsers.Remove(user.UserId);
    }
       
</script>

