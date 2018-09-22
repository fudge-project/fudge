using System;
using System.Linq;
using System.Web.UI;
using Fudge.Framework.Database;
using System.Web.Services;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public partial class Problems_TestRuns : FudgePage {
    FudgeDataContext db = new FudgeDataContext();

    const int MaxTextCase = 50;

    public Problems_TestRuns()
        : base(MenuItem.Problems) {

        VerifyQueryStringInt("id", id => db.Runs.Any(r => r.RunId == id), "Test runs not found for this submission.");
    }

    protected Run Run {
        get {
            return db.Runs.SingleOrDefault(r => r.RunId == Int32.Parse(Request.QueryString["id"]));
        }
    }

    protected void Page_Load(object sender, EventArgs e) {        
        Title += ".TestRuns[" + Run.RunId + "]";
        if (Run.Problem.IsArchived) {
            if (Run.Solved) {
                //just show testcases as the test runs if it all passed
                testRuns.DataSource = from tc in Run.Problem.TestCases
                                      select new {
                                          Input = tc.Input,
                                          Output = tc.Output.Truncate(MaxTextCase),
                                          ExpectedOutput = tc.Output.Truncate(MaxTextCase),
                                          tc.TestCaseId
                                      };
                testRuns.DataBind();
            }
            else {                
                var testRun = Run.TestRuns.First();
                //stop at first null
                int nullPos = testRun.Output.IndexOf('\0');
                var failedRun = new {
                    Input = testRun.TestCase.Input,
                    Output = nullPos >= 0 ? testRun.Output.Substring(0, nullPos).Truncate(MaxTextCase) :
                    testRun.Output.Truncate(MaxTextCase),
                    ExpectedOutput = testRun.TestCase.Output.Truncate(MaxTextCase),
                    TestCaseId = testRun.TestCaseId
                };
                //otherwise show all test cases up the failed testrun
                testRuns.DataSource = Run.Problem.TestCases
                                        .TakeWhile(tc => tc.TestCaseId != testRun.TestCase.TestCaseId)
                                        .Select(tc => new {
                                            Input = tc.Input,
                                            Output = tc.Output.Truncate(MaxTextCase),
                                            ExpectedOutput = tc.Output.Truncate(MaxTextCase),
                                            tc.TestCaseId
                                        })
                                        .Concat(new[] { failedRun });
                testRuns.DataBind();
            }            
        }
        else {
            testRunPanel.Visible = false;
            tip.Show();
        }
    }

    protected string GetSolved(int testCaseId) {        
        if (Run.TestRuns.Any()) {
            if (Run.TestRuns.First().TestCaseId == testCaseId) {
                return Html.Color("Failed", "red");
            }
        }
        return Html.Color("Passed", "green");
    }
}
