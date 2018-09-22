using System;
using System.IO;
using System.Web.UI;
using Fudge.Framework.Database;

public partial class Problems_Submit : FudgePage {

    public Problems_Submit()
        : base(MenuItem.Problems) {
        //Verify that problem is valid
        VerifyQueryString("name", name => Problem.GetProblemByUrlName(name) != null, "The problem requested is not valid");
    }

    public Problem Problem {
        get {
            return Problem.GetProblemByUrlName(Request.QueryString["name"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        ScriptManager.GetCurrent(this).EnablePageMethods = true;
        Title += ".Problems[\"" + Problem.Name + "\"].Submit()";
        if (!Page.IsPostBack) {
            javaTip.TipType = Fudge.Framework.Database.User.Tooltips.JavaGotcha;

            probref.InnerHtml = Html.LinkToProblem(Problem.ProblemId);
            language.SelectedLanguageId = FudgeUser.LanguageId;
        }
    }

    protected void OnProblemSubmitted(object sender, EventArgs e) {
        Page.Validate();

        if (!Page.IsValid) {
            return;
        }

        var db = new FudgeDataContext();

        //create a new run
        Run run = new Run {
            UserId = FudgeUser.UserId,
            Timestamp = DateTime.Now.ToUniversalTime(),
            LanguageId = language.SelectedLanguageId.Value,
            Status = RunStatus.Pending,
            ProblemId = Problem.ProblemId
        };

        //give precedence to the file upload
        if (submittedFile.HasFile) {
            //validate file
            Path.GetFullPath(submittedFile.FileName);
            StreamReader sr = new StreamReader(submittedFile.FileContent);
            run.Code = sr.ReadToEnd();
        }
        else {
            run.Code = codeBox.Text;
        }
        run.Size = run.Code.Length;
        db.Runs.InsertOnSubmit(run);
        db.SubmitChanges();
        //Services.Runs submissionService = new Services.Runs();
        fudge.fit.edu.Runs submissionService = new fudge.fit.edu.Runs();
        //submit the run id
        submissionService.BeginSubmit(run.RunId, null, null);

        //show the runs for current user and current problem
        Response.Redirect(String.Format("~/Problems/Runs.aspx?pid={0}&uid={1}", run.ProblemId, run.UserId));
    }
}
