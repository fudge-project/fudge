using System;
using System.Linq;
using System.Web.UI.WebControls;
using Extensions;
using Fudge.Framework.Database;
using System.Collections.Generic;


public partial class Users_Search : FudgePage {
    public Users_Search()
        : base(MenuItem.Community) {

    }

    FudgeDataContext db = new FudgeDataContext();

    protected void Page_Load(object sender, EventArgs e) {
        Form.DefaultButton = findAll.UniqueID;
        Title += ".Search()";
        searchResults.ItemDataBound += (s, args) => {
            args.Item.FindControl<Controls_MiniProfile>("friendProfile").Bind();
        };
    }

    protected void findAll_Click(object sender, EventArgs e) {
        string query = Util.CreateQueryString(new Dictionary<string, object> {
            {"name", String.IsNullOrEmpty(name.Text) ? null : name.Text.Trim() },
            {"school", String.IsNullOrEmpty(school.Text) ? null : school.Text.Trim() },
            {"lid", !language.SelectedLanguageId.HasValue ? null : language.SelectedLanguageId.Value.ToString() },
            {"cid", !country.SelectedCountryId.HasValue ? null : country.SelectedCountryId.Value.ToString() },            
        });

        Response.Redirect("Search" + query);
    }

    protected void userSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        IQueryable<User> users = db.Users.Where(u => u.Status == UserStatus.Activated); 

        if (!Request.IsQueryStringNull("name")) {
            users = from u in users
                    let hideLastName = (u.OptionFlag & (int)Fudge.Framework.Database.User.UserOptions.HideLastName) ==
                    (int)Fudge.Framework.Database.User.UserOptions.HideLastName
                    let fullName = u.FirstName + " " + (hideLastName ? u.LastName[0] + "." : u.LastName)
                    where u.ShortName.Contains(Request.QueryString["name"])
                            || fullName.Contains(Request.QueryString["name"])                  
                    select u;
        }

        if (!Request.IsQueryStringNull("school")) {
            var chosonSchool = db.Schools.SingleOrDefault(s => s.Name == Request.QueryString["school"]);
            if (chosonSchool != null) {
                users = users.Where(u => u.SchoolId == chosonSchool.SchoolId);
            }
        }

        if (!Request.IsQueryStringNull("lid")) {
            users = users.Where(u => u.LanguageId == Int32.Parse(Request.QueryString["lid"]));
        }
        
        if (!Request.IsQueryStringNull("cid")) {
            users = users.Where(u => u.CountryId == Int32.Parse(Request.QueryString["cid"]));
        }

        e.Result = users;
    }
}
