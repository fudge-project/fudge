using System;
using System.Linq;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Community_Snippets_View : FudgePage {
    FudgeDataContext db = new FudgeDataContext();
    public Community_Snippets_View()
        : base(MenuItem.Community, false) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        Form.DefaultButton = Search.UniqueID;
        Title += ".Snippets";
    }

    protected void Search_Click(object sender, EventArgs e) {
        codeSnippetView.DataBind();
    }

    protected void snippetSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        IQueryable<CodeSnippet> query = db.CodeSnippets;

        if (!String.IsNullOrEmpty(snippetName.Text.Trim())) {
            query = query.Where(s => s.Snippet.Contains(snippetName.Text.Trim()) || 
                s.Name.Contains(snippetName.Text.Trim()));
        }

        if (language.SelectedLanguageId.HasValue) {
            query = query.Where(s => s.LanguageId == language.SelectedLanguageId.Value);
        }

        e.Result = query.OrderByDescending(s => s.Timestamp);

    }

    protected void codeSnippetView_ItemUpdating(object sender, ListViewUpdateEventArgs e) {
        //never update the source
        e.Cancel = true;
    }   
}
