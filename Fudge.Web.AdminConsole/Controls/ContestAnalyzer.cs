using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class ContestAnalyzer : ManagerControl {

        public Contest Contest { get; set; }

        private Problem SelectedProblem {
            get {
                if (problemsListView.SelectedItems.Count > 0) {
                    return DataContext.Problems.SingleOrDefault(p => p.ProblemId == Int32.Parse(problemsListView.SelectedItems[0].SubItems[0].Text));
                }

                return null;
            }
        }

        private TestCase SelectedTestCase {
            get {
                if (testCaseListView.SelectedItems.Count > 0) {
                    return DataContext.TestCases.SingleOrDefault(tc => tc.TestCaseId == Int32.Parse(testCaseListView.SelectedItems[0].SubItems[0].Text));
                }

                return null;
            }
        }

        private User SelectedUser {
            get {
                if (userTestCaseListView.SelectedItems.Count > 0) {
                    return DataContext.Users.SingleOrDefault(u => u.UserId == Int32.Parse(userTestCaseListView.SelectedItems[0].SubItems[0].Text));
                }

                return null;
            }
        }

        public ContestAnalyzer() {
            InitializeComponent();
        }

        public override void Initialize() {
            problemsGroupBox.Text = String.Format("Problem Breakdown for {0}", Contest.Name);
            problemsListView.Items.Clear();

            foreach (ContestProblem problem in Contest.ContestProblems) {
                problemsListView.Items.Add(new ListViewItem(new[] {
                    problem.ProblemId.ToString(),
                    problem.Problem.Name,
                    problem.Problem.Runs.Count(r => r.ContestId == Contest.ContestId).ToString(),
                    problem.Problem.Runs.Count(r => r.ContestId == Contest.ContestId && !r.TestRuns.Any() && r.Status == RunStatus.Done).ToString(),
                }));
            }

            problemsListView.SelectedIndices.Add(0);
        }

        private void problemsListView_SelectedIndexChanged(object sender, EventArgs e) {
            problemsListView.BeginUpdate();

            if (SelectedProblem != null) {
                testCaseGroupBox.Text = String.Format("Test Case Breakdown for {0}", SelectedProblem.Name);
                testCaseListView.Items.Clear();

                foreach (TestCase testCase in SelectedProblem.TestCases) {
                    var runs = DataContext.Runs.Where(r => r.ContestId == Contest.ContestId && r.ProblemId == testCase.ProblemId);

                    if (!Contest.IsScoringSet(ContestScoring.TestCaseScoring)) {
                        runs = runs.Where(r => r.TestRuns.Any(tr => tr.TestCaseId >= testCase.TestCaseId));
                    }

                    int attempted = runs.Count();
                    int accepted = runs.Count(r => !r.TestRuns.Any(tr => tr.TestCaseId == testCase.TestCaseId));                    

                    testCaseListView.Items.Add(new ListViewItem(new[] {
                        testCase.TestCaseId.ToString(),
                        attempted.ToString(),
                        accepted.ToString(),
                        (attempted == 0 ? 0 : 1 - (accepted / (double)attempted)).ToString(),
                        Math.Max(0.10, accepted == 0 ? 0 : 1 - accepted / (double)attempted).ToString()                        
                    }));
                }
            }

            problemsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            problemsListView.EndUpdate();
        }

        private void testCaseListView_SelectedIndexChanged(object sender, EventArgs e) {
            userTestCaseListView.BeginUpdate();

            if (SelectedTestCase != null) {
                userTestCaseGroupBox.Text = String.Format("User Breakdown for Test Case {0}", SelectedTestCase.TestCaseId);
                userTestCaseListView.Items.Clear();

                foreach (ContestUser user in Contest.ContestUsers) {
                    var runs = DataContext.Runs.Where(r => r.ContestId == Contest.ContestId && r.UserId == user.UserId && r.ProblemId == SelectedProblem.ProblemId && r.TestRuns.Any(tr => tr.TestCaseId >= SelectedTestCase.TestCaseId));
                    int attempted = runs.Count();
                    int accepted = runs.Count(r => !r.TestRuns.Any(tr => tr.TestCaseId == SelectedTestCase.TestCaseId));

                    userTestCaseListView.Items.Add(new ListViewItem(new[] {
                        user.User.UserId.ToString(),
                        user.User.FullName,
                        attempted.ToString(),
                        accepted.ToString(),
                    }));
                }
            }

            userTestCaseListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            userTestCaseListView.EndUpdate();
        }

        private void userTestCaseListView_SelectedIndexChanged(object sender, EventArgs e) {
            userListView.BeginUpdate();

            if (SelectedUser != null) {                
                userListView.Items.Clear();

                var userRuns = DataContext.Runs.Where(r => r.ContestId == Contest.ContestId && r.UserId == SelectedUser.UserId && r.ProblemId == SelectedProblem.ProblemId);
                var run = userRuns.SingleOrDefault(r => r.RunId == userRuns.Max(ur => ur.RunId));
                double avgPoints = run.AvgPoints / DataContext.TestCases.Count(tc => tc.ProblemId == SelectedProblem.ProblemId);
                double sum = 0;

                foreach (TestCase testCase in run.Problem.TestCases) {
                    var runs = DataContext.Runs.Where(r => r.ContestId == Contest.ContestId && r.UserId == SelectedUser.UserId && r.ProblemId == SelectedProblem.ProblemId && (!r.TestRuns.Any() || r.TestRuns.Any(tr => tr.TestCaseId >= testCase.TestCaseId)));                    
                    int failed = runs.Count() - runs.Count(r => !r.TestRuns.Any(tr => tr.TestCaseId == testCase.TestCaseId));

                    var testCaseRuns = DataContext.Runs.Where(r => r.ContestId == Contest.ContestId && r.ProblemId == testCase.ProblemId);

                    if (!Contest.IsScoringSet(ContestScoring.TestCaseScoring)) {
                        testCaseRuns = testCaseRuns.Where(r => r.TestRuns.Any(tr => tr.TestCaseId >= testCase.TestCaseId));
                    }

                    int attempted = testCaseRuns.Count();
                    int accepted = testCaseRuns.Count(r => !r.TestRuns.Any(tr => tr.TestCaseId == testCase.TestCaseId));
                    double ratio = Math.Max(0.10, accepted == 0 ? 0 : 1 - accepted / (double)attempted);
                    bool solved = !run.TestRuns.Any(tr => tr.TestCaseId == testCase.TestCaseId);
                    double points = (solved ? avgPoints * ratio * Math.Pow(0.99, failed) : 0);
                    sum += points;                   

                    userListView.Items.Add(new ListViewItem(new[] {
                        testCase.TestCaseId.ToString(),                        
                        failed.ToString(),
                        solved.ToString(),
                        points.ToString()
                    }));
                }

                userGroupBox.Text = String.Format("Test Case Breakdown for {0}'s Final Run ({1} points)", SelectedUser.FirstName + " " + SelectedUser.LastName, sum);
            }
            
            userListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            userListView.EndUpdate();
        }

        private void userListView_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
