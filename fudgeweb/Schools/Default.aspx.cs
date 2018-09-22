using System;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Schools_Schools : FudgePage {
    public Schools_Schools()
        : base(MenuItem.Community, false) {

    }

    protected string AlphabetLinks {
        get {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 26; i++) {
                char c = (char)('A' + i);
                sb.Append(Html.Link("/Schools/" + c, c.ToString()));
            }
            sb.Append(Html.Link("/Schools/", "All"));
            return sb.ToString();
        }
    }

    FudgeDataContext db = new FudgeDataContext();

    protected void Page_Load(object sender, EventArgs e) {
        if (Request.IsQueryStringNull("name")) {
            schools.DataSourceID = "schoolsSource";
            schoolsByLetter.DataSourceID = String.Empty;
        }
        else {
            schoolsByLetter.DataSourceID = "schoolsSource";
            schools.DataSourceID = String.Empty;
        }
    }

    protected void schoolsSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        if (Request.IsQueryStringNull("name")) {
            e.Result = from s in db.Schools
                       group s by s.Name[0] into g
                       select new { Letter = g.Key, Schools = g };
        }
        else {
            e.Result = from s in db.Schools
                       where s.Name[0] == Request.QueryString["name"][0]
                       orderby s.Name
                       select s;
        }
    }
}
