using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using System.Text.RegularExpressions;
using Fudge.Web.AdminConsole.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class ContestManager : ManagerControl {

        public Contest SelectedContest { get; private set; }

        public ContestManager() {
            InitializeComponent();

            string[] names = Enum.GetNames(typeof(ContestScoring));
            ContestScoring[] values = Enum.GetValues(typeof(ContestScoring)).Cast<ContestScoring>().ToArray();

            for (int i = 0; i < names.Length; ++i) {
                CheckBox scoringCheckBox = new CheckBox();
                scoringCheckBox.AutoSize = true;

                scoringCheckBox.Text = names[i];
                scoringCheckBox.Tag = values[i];

                scoringPanel.Controls.Add(scoringCheckBox);
            }
        }

        private void UpdateContests() {
            contestListView.Items.Clear();

            foreach (Contest contest in DataContext.Contests) {
                contestListView.Items.Add(new ListViewItem(new[] { contest.ContestId.ToString(), contest.Name, contest.UrlName, contest.Timestamp.ToString(), contest.StartTime.ToString(), contest.EndTime.ToString(), contest.Status.ToString(), contest.Scoring.ToString(), contest.TeamSize.ToString(), String.Empty }));
            }

            contestListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public override void Initialize() {
            statusComboBox.SelectedIndex = 0;
            string[] problemsNames = DataContext.Problems.Select(p => p.UrlName).ToArray();

            problemsComboBox.Items.AddRange(problemsNames);
            problemsComboBox.AutoCompleteCustomSource.AddRange(problemsNames);

            UpdateContests();

            contestListView.SelectedIndices.Add(0);
        }

        private void contestListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (contestListView.SelectedItems.Count > 0) {
                SelectedContest = DataContext.Contests.Single(c => c.ContestId == Int32.Parse(contestListView.SelectedItems[0].SubItems[0].Text));

                updateButton.Enabled = true;
                updateButton.Text = "Update";

                deleteButton.Enabled = true;
            }
            else {
                SelectedContest = null;

                updateButton.Enabled = false;
                deleteButton.Enabled = false;
            }

            UpdateSelectedContest();
        }

        private string GetShortName(string name) {
            Regex invalidRegex = new Regex(@"[^a-zA-Z0-9\s]");
            return invalidRegex.Replace(name, String.Empty).Replace(' ', '_');
        }

        private ContestScoring GetScoring() {
            ContestScoring scoring = 0;

            foreach (CheckBox checkBox in scoringPanel.Controls.Cast<CheckBox>()) {
                if (checkBox.Checked) {
                    scoring |= (ContestScoring)checkBox.Tag;
                }
            }

            return scoring;
        }

        private void SetScoring(Contest contest) {
            foreach (CheckBox checkBox in scoringPanel.Controls.Cast<CheckBox>()) {
                if (contest.IsScoringSet((ContestScoring)checkBox.Tag)) {
                    checkBox.Checked = true;
                }
                else {
                    checkBox.Checked = false;
                }
            }
        }

        private void UpdateSelectedContest() {
            contestProblemsListView.Items.Clear();

            if (SelectedContest == null) {
                nameTextBox.Text = String.Empty;
                urlNameLabel.Text = String.Empty;
                startDateTimePicker.Value = DateTime.UtcNow;
                endDateTimePicker.Value = DateTime.UtcNow;
                statusComboBox.SelectedIndex = 0;
                teamSizeTextBox.Text = "0";
            }
            else {          
                nameTextBox.Text = SelectedContest.Name;
                urlNameLabel.Text = SelectedContest.UrlName;
                startDateTimePicker.Value = SelectedContest.StartTime;
                endDateTimePicker.Value = SelectedContest.EndTime;
                SetScoring(SelectedContest);
                statusComboBox.SelectedIndex = (int)SelectedContest.Status;
                teamSizeTextBox.Text = SelectedContest.TeamSize.ToString();
                contestProblemsListView.Items.Clear();

                foreach (ContestProblem contestProblem in SelectedContest.ContestProblems) {
                    contestProblemsListView.Items.Add(new ListViewItem(new[] { contestProblem.Problem.ProblemId.ToString(), contestProblem.Problem.Name }));
                }
            }
        }

        private void contestProblemsListView_DoubleClick(object sender, EventArgs e) {
            if (contestProblemsListView.SelectedItems.Count > 0) {
                ContestProblem contestProblem = SelectedContest.ContestProblems.Single(cp => cp.ProblemId == Int32.Parse(contestProblemsListView.SelectedItems[0].SubItems[0].Text));
                SelectedContest.ContestProblems.Remove(contestProblem);
                DataContext.ContestProblems.DeleteOnSubmit(contestProblem);

                contestProblemsListView.SelectedItems[0].Remove();
            }
        }

        private void newButton_Click(object sender, EventArgs e) {
            contestListView.SelectedItems.Clear();

            updateButton.Enabled = true;
            updateButton.Text = "Add";

            SelectedContest = new Contest();
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to delete \"" + SelectedContest.Name + "\"?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                DataContext.ContestProblems.DeleteAllOnSubmit(DataContext.ContestProblems.Where(cp => cp.ContestId == SelectedContest.ContestId));
                DataContext.Contests.DeleteOnSubmit(SelectedContest);
                DataContext.SubmitChanges();

                UpdateContests();
            }
        }

        private void updateButton_Click(object sender, EventArgs e) {

            if (SelectedContest.ContestId == 0) {
                SelectedContest.Timestamp = DateTime.UtcNow;
                DataContext.Contests.InsertOnSubmit(SelectedContest);
            }
            else if (MessageBox.Show("Are you sure you want to update " + SelectedContest.Name + "?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No) {
                return;
            }

            SelectedContest.Name = nameTextBox.Text;
            SelectedContest.UrlName = urlNameLabel.Text;
            SelectedContest.StartTime = startDateTimePicker.Value;
            SelectedContest.EndTime = endDateTimePicker.Value;
            SelectedContest.Scoring = GetScoring();
            SelectedContest.Status = (ContestStatus)statusComboBox.SelectedIndex;
            SelectedContest.TeamSize = Int32.Parse(teamSizeTextBox.Text);

            DataContext.SubmitChanges();
            UpdateContests();
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e) {
            urlNameLabel.Text = GetShortName(nameTextBox.Text);
        }

        private void addProblemButton_Click(object sender, EventArgs e) {
            Problem problem = DataContext.Problems.SingleOrDefault(p => p.UrlName == problemsComboBox.Text);

            if (problem == null) {
                MessageBox.Show("Could not find a problem with that short name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                contestProblemsListView.Items.Add(new ListViewItem(new[] { problem.ProblemId.ToString(), problem.Name }));

                ContestProblem contestProblem = new ContestProblem();
                contestProblem.ProblemId = problem.ProblemId;

                SelectedContest.ContestProblems.Add(contestProblem);
            }
        }

        private void analyzeButton_Click(object sender, EventArgs e) {
            new ContestAnalyzeForm().ShowDialog(DataContext, SelectedContest);
        }

        private void pointsButton_Click(object sender, EventArgs e) {
            SelectedContest.UpdatePoints();
        }
    }
}
