using System.Web;

/// <summary>
/// 
/// </summary>
public static class ErrorHelper {
    /// <summary>
    /// Verifies that a user is logged in otherwise redirects to specified url
    /// </summary>
    /// <param name="redirectUrl">url to redirect to if user is not logged in</param>
    public static void VerifyUserSession(string redirectUrl) {
        if (HttpContext.Current.Session["user"] == null) {
            HttpContext.Current.Response.Redirect(redirectUrl);
        }
    }

    public static void RequireLogin() {
        VerifyUserSession("~/LoginRequired");
    }
}
