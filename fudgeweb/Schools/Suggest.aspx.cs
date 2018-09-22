using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;
using Resources;

public partial class Register_AddSchool : FudgePage {
    public Register_AddSchool()
        : base(MenuItem.Teams, false) {

    }

    FudgeDataContext db = new FudgeDataContext();

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void suggestSchoolSourceInserted(object sender, LinqDataSourceStatusEventArgs e) {
        addSchoolPanel.Visible = false;
        message.Visible = true;
        message.InnerText = Resource.SchoolSuggested;
        SuggestedSchool suggestedSchool = e.Result as SuggestedSchool;
        //this should never happen
        if (suggestedSchool != null) {
            var messageBody = schoolRequestForm.FindControl<TextBox>("messageBody");

            Email.NewSchoolEmail.SendToAdmin("New School Request",
                suggestedSchool.Name, suggestedSchool.Domain, suggestedSchool.NotifyEmail, messageBody.Text);
        }
    }

    protected void checkSchool_Click(object sender, EventArgs e) {
        string website = schoolRequestForm.FindControl<TextBox>("schoolWebsite").Text;
        string name = schoolRequestForm.FindControl<TextBox>("schoolName").Text;

        //check to see if requested school is already in the database
        var possible = from s in db.Schools
                       let wc = "%" + s.Domain
                       where SqlMethods.Like(website, wc) ||
                       s.Name.StartsWith(name)
                       select new { s.Name, s.Domain };

        //check to see if school has been suggested
        var possibleSuggested = from s in db.SuggestedSchools
                                let wc = "%" + s.Domain
                                where SqlMethods.Like(website, wc) ||
                                s.Name.StartsWith(name)
                                select new { s.Name, s.Domain };

        if (possibleSuggested.Any()) {
            schoolRequestForm.FindControl<HtmlGenericControl>("schoolRequested").Visible = true;
            var schools = schoolRequestForm.FindControl<DataList>("schools");
            schools.DataSource = possibleSuggested;
            schools.DataBind();
        }
        else if (possible.Any()) {
            schoolRequestForm.FindControl<HtmlGenericControl>("possibleSchools").Visible = true;
            var schools = schoolRequestForm.FindControl<DataList>("schools");
            schools.DataSource = possible;
            schools.DataBind();
        }        
        else {
            schoolRequestForm.InsertItem(true);
        }
    }
}
