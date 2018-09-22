using System;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Collections.Generic;
using Fudge.Framework.Database;

/// <summary>
/// Summary description for ProblemService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[ScriptService]
public class UserService : System.Web.Services.WebService {
    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count) {
        if (count == 0) {
            count = 10;
        }
        return UserExtensions.GetUsersByName(prefixText).ToArray();
    }
}

