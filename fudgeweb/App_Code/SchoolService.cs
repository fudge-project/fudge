using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using Fudge.Framework.Database;

/// <summary>
/// Summary description for SchoolService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[ScriptService]
public class SchoolService : System.Web.Services.WebService {

    FudgeDataContext db = new FudgeDataContext();

    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count) {
        if (count == 0) {
            count = 10;
        }
        return db.Schools.Where(s => s.Name.StartsWith(prefixText)).Select(s => s.Name).ToArray();
    }

}

