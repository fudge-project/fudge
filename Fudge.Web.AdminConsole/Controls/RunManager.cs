using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using System.Data.Linq;
using Fudge.Web.AdminConsole.Utils;
using System.Data.Linq.SqlClient;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class RunManager : ManagerControl {

        private class RunsListView : PagedListView<Run> { }

        public RunManager() {
            InitializeComponent();

            runsListView.PageSize = 50;
            runsListView.RetrieveItem += new PagedListView<Run>.RetrieveItemEventHandler(runsListView_RetrieveItem);

            runsListView.Selector = (ListViewItem item) => { return DataContext.Runs.SingleOrDefault(r => r.RunId == Int32.Parse(item.SubItems[0].Text)); };
            runsListView.Sorter = (IQueryable<Run> items, int parameter, SortDirection direction) => {
                switch (parameter) {
                    case 0: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.RunId) : items.OrderByDescending(r => r.RunId));
                    case 1: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.Timestamp) : items.OrderByDescending(r => r.Timestamp));
                    case 2: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.User.FirstName + " " + r.User.LastName) : items.OrderByDescending(r => r.User.FirstName + " " + r.User.LastName));
                    case 3: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.Problem.Name) : items.OrderByDescending(r => r.Problem.Name));
                    case 4: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.Language.Name) : items.OrderByDescending(r => r.Language.Name));
                    case 5: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.Status) : items.OrderByDescending(r => r.Status));
                    case 6: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.ExecutionTime) : items.OrderByDescending(r => r.ExecutionTime));
                    case 7: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.Memory) : items.OrderByDescending(r => r.Memory));
                    case 8: return (direction == SortDirection.Ascending ? items.OrderBy(r => r.Contest.Name) : items.OrderByDescending(r => r.Contest.Name));
                }

                return items;
            };
        }

        void runsListView_RetrieveItem(object sender, PagedListView<Run>.RetrieveItemEventArgs e) {
            e.ListViewItem = new ListViewItem(new[] { e.Item.RunId.ToString(), e.Item.Timestamp.ToString(),
                        e.Item.User.FullName, e.Item.Problem.Name, e.Item.Language.Name,
                        e.Item.Status.ToString(), e.Item.ExecutionTime.ToString(), e.Item.Memory.ToString(),
                        e.Item.Contest != null ? e.Item.Contest.Name : String.Empty});

            if (e.Item.Solved) {
                e.ListViewItem.BackColor = Color.LightGreen;
            }
            else {
                e.ListViewItem.BackColor = Color.Pink;
            }
        }

        private void UpdateRuns() {
            var runs = DataContext.Runs.Where(r => (r.User.FirstName.Contains(filterControl["User"]) ||
                                                    r.User.LastName.Contains(filterControl["User"])) &&
                                                    r.Problem.Name.Contains(filterControl["Problem"]) &&
                                                    r.Language.Name.Contains(filterControl["Language"]));

            if (!String.IsNullOrEmpty(filterControl["Status"])) {
                runs = runs.Where(r => r.Status == (RunStatus)Enum.Parse(typeof(RunStatus), filterControl["Status"]));
            }

            if (!String.IsNullOrEmpty(filterControl["Contest"])) {
                runs = runs.Where(r => r.Contest.Name.Contains(filterControl["Contest"]));
            }

            runsListView.Update(runs);
        }

        public override void Initialize() {
            filterControl.AddFilter("Status", FilterControl.FilterType.Combo, new string[] { String.Empty, "Pending", "Compiling", "Running", "CompilationError", "Done", "InternalError" });
            filterControl.AddFilter("Contest", FilterControl.FilterType.ComboText, DataContext.Contests.Select(c => c.Name).ToArray());
            filterControl.AddFilter("Language", FilterControl.FilterType.ComboText, DataContext.Languages.Select(l => l.Name).ToArray());
            filterControl.AddFilter("Problem", FilterControl.FilterType.ComboText, DataContext.Problems.Select(p => p.Name).ToArray());
            filterControl.AddFilter("User", FilterControl.FilterType.Text);

            filterControl.Filter += new EventHandler(filterControl_Filter);

            runsListView.Update(DataContext.Runs);
        }

        void filterControl_Filter(object sender, EventArgs e) {
            UpdateRuns();
        }

        private void runsListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (runsListView.SelectedItem != null) {
                codeTextBox.Enabled = true;
                codeTextBox.Text = runsListView.SelectedItem.Code.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);

                if (runsListView.SelectedItem.Error != null) {
                    errorRichTextBox.Text = runsListView.SelectedItem.Error;
                    splitContainer.Panel2Collapsed = false;
                }
                else {
                    splitContainer.Panel2Collapsed = true;
                }

                testRunsListView.Enabled = true;
                testRunsListView.Items.Clear();

                var testRuns = DataContext.TestRuns.Where(tr => tr.RunId == runsListView.SelectedItem.RunId);                

                foreach (TestRun testRun in testRuns) {
                    var inputLength = DataContext.TestCases.Where(tc => tc.TestCaseId == testRun.TestCaseId).Select(tc => tc.Input.Length);
                    
                    string testCaseOuput = DataContext.TestCases.Where(tc => tc.TestCaseId == testRun.TestCaseId).Select(tc => tc.Output).First();
                    string testCaseInput = "[Unloaded]";                    

                    if (inputLength.First() < 65536) {
                        testCaseInput = testRun.TestCase.Input;
                    }                    

                    ListViewItem item = new ListViewItem(new[] {testRun.TestRunId.ToString(), testCaseInput, testCaseOuput, 
                                                                testRun.Output, testRun.Status.ToString()});

                    item.Lazify();
                    testRunsListView.Items.Add(item);
                }

                if (testRunsListView.Items.Count > 0) {
                    testRunsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                else {
                    testRunsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            else {
                codeTextBox.Clear();
                codeTextBox.Enabled = false;

                testRunsListView.Items.Clear();
                testRunsListView.Enabled = false;
            }
        }

        private void codeTextBox_MouseLeave(object sender, EventArgs e) {
            codeTextBox.Invalidate();
        }

        private void codeTextBox_MouseEnter(object sender, EventArgs e) {
            codeTextBox.Invalidate();
        }

        private void testRunsListView_DoubleClick(object sender, EventArgs e) {
            if (testRunsListView.SelectedItems.Count > 0) {
                new TestRunForm().ShowDialog(testRunsListView.SelectedItems[0].SubItems[1].Tag.ToString(),
                                            testRunsListView.SelectedItems[0].SubItems[2].Tag.ToString(),
                                            testRunsListView.SelectedItems[0].SubItems[3].Tag.ToString());
            }
        }

        private void previousButton_Click(object sender, EventArgs e) {
            runsListView.Previous();
        }

        private void nextButton_Click(object sender, EventArgs e) {
            runsListView.Next();
        }

        private void refreshButton_Click(object sender, EventArgs e) {
            UpdateRuns();
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (runsListView.SelectedItem != null && MessageBox.Show("Are you sure you want to delete this run?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                DataContext.Runs.DeleteOnSubmit(runsListView.SelectedItem);
                DataContext.SubmitChanges();

                UpdateRuns();
            }
        }

        private void rerunButton_Click(object sender, EventArgs e) {
            if (runsListView.SelectedItem != null && MessageBox.Show("Are you sure you want to re-run this run?", "Confirm Re-run", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

                DataContext.TestRuns.DeleteAllOnSubmit(DataContext.TestRuns.Where(tr => tr.RunId == runsListView.SelectedItem.RunId));
                DataContext.SubmitChanges();

                new Services.RunsSoapClient().Submit(runsListView.SelectedItem.RunId);
                UpdateRuns();
            }
        }

        private void testRunsListView_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
