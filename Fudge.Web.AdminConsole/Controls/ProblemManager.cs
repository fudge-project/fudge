using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml.Linq;
using Fudge.Framework.Database;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.Threading;
using Fudge.Web.AdminConsole.Forms;
using Fudge.Web.AdminConsole.Utils;
using System.Data.Linq;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class ProblemManager : ManagerControl {

        private static string[] testCaseDefaultItems = new[] { String.Empty, "Input", "Expected Ouput" };
        private int? currentProblemId = null;

        public override FudgeDataContext DataContext { get; set; }

        public ProblemManager() {
            InitializeComponent();
        }

        private void ClearForNew() {
            submitChangesButton.Text = "Add";
            updateTestCasesCheckBox.Visible = false;
            updateTestCasesCheckBox.Checked = true;
            visibleCheckBox.Checked = true;
            deleteButton.Enabled = false;
            currentProblemId = null;
            testCasesGroupBox.Text = "Test Cases (0)";
            UpdateUniqueKeysLabel();
            Clear();
            UpdateHtml();
        }

        private void Clear() {
            nameTextBox.Clear();
            timeLimitTextBox.Clear();
            memoryLimitTextBox.Clear();
            testCasesListView.Items.Clear();
            outputTextBox.Clear();
            inputTextBox.Clear();
            descriptionTextBox.Clear();
            xmlTextBox.Clear();
        }

        private void RetrieveProblem(int problemId) {
            Problem problem = DataContext.Problems.SingleOrDefault(p => p.ProblemId == problemId);

            if (problem == null) {
                MessageBox.Show("A problem with that ID does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                submitChangesButton.Text = "Update";
                updateTestCasesCheckBox.Visible = true;
                updateTestCasesCheckBox.Checked = false;
                currentProblemId = problemId;
                deleteButton.Enabled = true;
                visibleCheckBox.Checked = problem.Visible;
                Clear();

                UpdateFields(problem.Statement);
            }
        }

        private void UpdateFields(XElement problemXml) {

            descriptionTextBox.Text = problemXml.Element("description").Value;
            inputTextBox.Text = problemXml.Element("input").Value;
            outputTextBox.Text = problemXml.Element("output").Value;

            nameTextBox.Text = problemXml.Attribute("name").Value;
            sourceComboBox.Text = problemXml.Attribute("source").Value;
            timeLimitTextBox.Text = problemXml.Attribute("time").Value;
            memoryLimitTextBox.Text = problemXml.Attribute("memory").Value;

            testCasesListView.Items.Clear();

            foreach (XElement testCase in problemXml.Element("cases").Elements("case")) {
                ListViewItem testCaseItem = new ListViewItem(new[] { String.Empty, testCase.Element("input").Value, testCase.Element("output").Value });

                if (testCase.Element("sample") != null) {
                    testCaseItem.Checked = true;
                }

                testCaseItem.Lazify();
                testCasesListView.Items.Add(testCaseItem);
            }

            testCasesGroupBox.Text = String.Format("Test Cases ({0})", testCasesListView.Items.Count);

            UpdateProblemXml();
            UpdateHtml();
        }

        private void UpdateProblemAutoComplete() {
            byNameComboBox.AutoCompleteCustomSource.Clear();
            byNameComboBox.Items.Clear();

            string[] names = DataContext.Problems.Select(p => p.UrlName).ToArray();

            byNameComboBox.AutoCompleteCustomSource.AddRange(names);
            byNameComboBox.Items.AddRange(names);            
        }

        private void UpdateSourceAutoComplete() {
            sourceComboBox.AutoCompleteCustomSource.Clear();
            sourceComboBox.Items.Clear();

            string[] names = DataContext.Sources.Select(s => s.Name).ToArray();
            
            sourceComboBox.AutoCompleteCustomSource.AddRange(names);
            sourceComboBox.Items.AddRange(names);
        }

        private XElement UpdateProblemXml() {
            FixAllNewLines();

            string[] inputs = new string[testCasesListView.Items.Count];
            string[] outputs = new string[testCasesListView.Items.Count];
            bool[] samples = new bool[testCasesListView.Items.Count];

            for (int i = 0; i < testCasesListView.Items.Count; ++i) {
                inputs[i] = testCasesListView.Items[i].SubItems[1].Tag.ToString();
                outputs[i] = testCasesListView.Items[i].SubItems[2].Tag.ToString();
                samples[i] = testCasesListView.Items[i].Checked;
            }

            //preserve new lines
            XElement problemXml = CreateProblemXml(nameTextBox.Text,
                                                   sourceComboBox.Text,
                                                   timeLimitTextBox.Value,
                                                   memoryLimitTextBox.Value,
                                                   inputs,
                                                   outputs,
                                                   samples,
                                                   descriptionTextBox.Text,
                                                   inputTextBox.Text,
                                                   outputTextBox.Text);

            xmlTextBox.Text = problemXml.ToString();

            return problemXml;
        }

        private string FormatNewLines(string html) {
            return html.Replace("\r\n", "\n").Replace("\n", "<br/>");
        }

        private int GetSourceId(string name) {
            return DataContext.Sources.Single(s => s.Name == name).SourceId;
        }

        private void CreateProblem(Problem problem, XElement problemXml, bool updateTestCases) {

            problem.Statement = problemXml;
            problem.Name = problem.Statement.Attribute("name").Value;
            problem.SourceId = GetSourceId(problem.Statement.Attribute("source").Value);                        

            if (updateTestCases) {
                foreach (XElement testCase in problem.Statement.Element("cases").Elements("case")) {
                    TestCase tc = new TestCase();

                    tc.Input = FixNewLines(testCase.Element("input").Value);
                    tc.Output = FixNewLines(testCase.Element("output").Value);

                    problem.TestCases.Add(tc);
                }
            }
        }

        private void FixAllNewLines() {
            descriptionTextBox.Text = FixNewLines(descriptionTextBox.Text);
            inputTextBox.Text = FixNewLines(inputTextBox.Text);
            outputTextBox.Text = FixNewLines(outputTextBox.Text);

            foreach (ListViewItem item in testCasesListView.Items) {
                item.SubItems[1].Text = FixNewLines(item.SubItems[1].Tag.ToString().TrimEnd(null));
                item.SubItems[2].Text = FixNewLines(item.SubItems[2].Tag.ToString().TrimEnd(null));
            }

            testCasesListView.Lazify(new int [] {1, 2} );
        }

        private string FixNewLines(string text) {
            return EncodeTestCase(text);
        }

        private XElement CreateProblemXml(string name, string source, int timeLimit, int memoryLimit, string[] inputs, string[] outputs, bool[] samples, string description, string input, string output) {
            if (inputs.Length != outputs.Length || inputs.Length != samples.Length) {
                throw new Exception("Cases input/output/sample mismatch!");
            }

            var cases = new List<XElement>();

            for (int i = 0; i < inputs.Length; ++i) {

                if (!samples[i]) {
                    cases.Add(new XElement("case",
                                new XElement("input", inputs[i]),
                                new XElement("output", outputs[i])));
                }
                else {
                    cases.Add(new XElement("case",
                                new XElement("input", inputs[i]),
                                new XElement("output", outputs[i]),
                                new XElement("sample",
                                    new XElement("input", EncodeTestCase(inputs[i])),
                                    new XElement("output", EncodeTestCase(outputs[i]))
                                    )
                                )
                              );
                }
            }

            return new XElement("problem",
                        new XAttribute("name", name),
                        new XAttribute("source", source),
                        new XAttribute("time", timeLimit),
                        new XAttribute("memory", memoryLimit),
                        new XElement("description", description),
                        new XElement("input", input),
                        new XElement("output", output),
                        new XElement("cases", cases));
        }

        private string EncodeTestCase(string testCase) {
            return testCase.Replace("\r\n", "\n").Replace("\n", "\r\n").TrimEnd(null);
        }

        private void submitChangesButton_Click(object sender, EventArgs e) {

            if (DataContext.Sources.Where(s => s.Name == sourceComboBox.Text).Count() == 0) {
                if (MessageBox.Show("The source " + sourceComboBox.Text + " does not exist and must be added before performing this operation", "Confirm Insert Source", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel) {
                    return;
                }

                Source source = new Source();
                source.Name = sourceComboBox.Text;

                DataContext.Sources.InsertOnSubmit(source);
                DataContext.SubmitChanges();

                UpdateSourceAutoComplete();
            }

            Problem problem;
            XElement problemXml = UpdateProblemXml();

            if (currentProblemId == null) {
                problem = new Problem();

                problem.Rating = new Rating();
                problem.Timestamp = DateTime.UtcNow;

                problem.Forum = new Forum();
                problem.Forum.CategoryId = 1;
                problem.Forum.Title = nameTextBox.Text;
                problem.Forum.Visible = true;
                problem.Forum.Timestamp = DateTime.UtcNow;
            }
            else {
                problem = DataContext.Problems.SingleOrDefault(p => p.ProblemId == currentProblemId);

                if (updateTestCasesCheckBox.Checked) {
                    DataContext.TestCases.DeleteAllOnSubmit(DataContext.TestCases.Where(tc => tc.ProblemId == currentProblemId));
                    DataContext.TestRuns.DeleteAllOnSubmit(DataContext.TestRuns.Where(tr => tr.TestCase.ProblemId == currentProblemId));
                    
                    foreach (Run run in problem.Runs) {
                        run.Status = RunStatus.Pending;
                    }

                    problem.TestCases.Clear();
                }
            }

            CreateProblem(problem, problemXml, updateTestCasesCheckBox.Checked);

            problem.UrlName = GetShortName(problem.Name);
            problem.Visible = visibleCheckBox.Checked;

            if (currentProblemId == null) {
                DataContext.Problems.InsertOnSubmit(problem);
            }
            else {
                if (MessageBox.Show("Are you sure you want to update problem with ID " + currentProblemId + "?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No) {
                    return;
                }
            }

            try {

                ChangeSet cs = DataContext.GetChangeSet();
                DataContext.SubmitChanges();
            }
            catch (ChangeConflictException ex) {
                MessageBox.Show(ex.Message, "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (updateTestCasesCheckBox.Checked) {
                Services.RunsSoapClient runsService = new Services.RunsSoapClient();

                foreach (Run run in problem.Runs) {
                    runsService.Submit(run.RunId);
                }
            }

            UpdateUniqueKeysLabel();
            RetrieveProblem(problem.ProblemId);
        }

        private void newButton_Click(object sender, EventArgs e) {
            ClearForNew();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in testCasesListView.SelectedItems) {
                testCasesListView.Items.Remove(item);
            }
            
            testCasesGroupBox.Text = String.Format("Test Cases ({0})", testCasesListView.Items.Count);
        }

        private void retrieveButton_Click(object sender, EventArgs e) {
            if (idRadioButton.Checked) {
                RetrieveProblem(Int32.Parse(byIdTextBox.Text));
            }
            else {
                Problem problem = DataContext.Problems.SingleOrDefault(p => p.UrlName == byNameComboBox.Text);

                if (problem == null) {
                    MessageBox.Show("A problem with that short name does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else {
                    RetrieveProblem(problem.ProblemId);
                }
            }
        }

        private void testCaseOutputTextBox_TextChanged(object sender, EventArgs e) {
            if (testCasesListView.SelectedItems.Count > 0) {
                testCasesListView.SelectedItems[0].SubItems[2].Text = testCaseOutputTextBox.Text;
                testCasesListView.SelectedItems[0].SubItems[2].Lazify();
            }
        }

        private void testCaseInputTextBox_TextChanged(object sender, EventArgs e) {
            if (testCasesListView.SelectedItems.Count > 0) {
                testCasesListView.SelectedItems[0].SubItems[1].Text = testCaseInputTextBox.Text;
                testCasesListView.SelectedItems[0].SubItems[1].Lazify();
            }
        }

        private void testCasesListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (testCasesListView.SelectedItems.Count == 0) {
                testCaseInputTextBox.Text = String.Empty;
                testCaseOutputTextBox.Text = String.Empty;

                testCaseInputTextBox.Enabled = false;
                testCaseOutputTextBox.Enabled = false;
            }
            else {
                testCaseInputTextBox.Text = testCasesListView.SelectedItems[0].SubItems[1].Tag.ToString();
                testCaseOutputTextBox.Text = testCasesListView.SelectedItems[0].SubItems[2].Tag.ToString();

                testCaseInputTextBox.Enabled = true;
                testCaseOutputTextBox.Enabled = true;
            }
        }

        private void insertnewToolStripMenuItem_Click(object sender, EventArgs e) {
            ListViewItem item = new ListViewItem(testCaseDefaultItems);
            item.Lazify();

            testCasesListView.Items.Add(item);
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to delete this problem?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {

                DataContext.Problems.DeleteOnSubmit(DataContext.Problems.SingleOrDefault(p => p.ProblemId == currentProblemId));
                DataContext.SubmitChanges();

                ClearForNew();
            }
        }

        private void insertparsedToolStripMenuItem_Click(object sender, EventArgs e) {
            
            ParseForm.TestCase[] testCases = new ParseForm().GetTestCases();

            foreach (ParseForm.TestCase testCase in testCases) {
                ListViewItem item = new ListViewItem(new[] { String.Empty, testCase.input, testCase.output });
                item.Lazify();

                testCasesListView.Items.Add(item);
            }   
            
            testCasesGroupBox.Text = String.Format("Test Cases ({0})", testCasesListView.Items.Count);         
        }

        private void manageSourcesButton_Click(object sender, EventArgs e) {
            new SourceForm(DataContext).ShowDialog();

            UpdateSourceAutoComplete();
        }

        public override void Initialize() {
            descriptionTextBox.DataContext = DataContext;
            inputTextBox.DataContext = DataContext;
            outputTextBox.DataContext = DataContext;

            ClearForNew();
            UpdateSourceAutoComplete();
            UpdateProblemAutoComplete();
        }

        private void saveXmlButton_Click(object sender, EventArgs e) {
            UpdateProblemXml();

            Regex invalidRegex = new Regex(@"[\\\/:\*\?""'<>|]");
            xmlSaveDialog.FileName = invalidRegex.Replace(nameTextBox.Text, String.Empty);

            if (xmlSaveDialog.ShowDialog() == DialogResult.OK) {
                StreamWriter file = new StreamWriter(new FileStream(xmlSaveDialog.FileName, FileMode.Create));

                file.Write(xmlTextBox.Text);
                file.Close();
            }
        }

        private void loadXmlButton_Click(object sender, EventArgs e) {
            if (xmlOpenDialog.ShowDialog() == DialogResult.OK) {
                //ClearForNew();
                StreamReader file = new StreamReader(new FileStream(xmlOpenDialog.FileName, FileMode.Open));

                UpdateFields(XElement.Parse(file.ReadToEnd(), LoadOptions.PreserveWhitespace));
                file.Close();
            }
        }

        private void previewXmlButton_Click(object sender, EventArgs e) {
            UpdateProblemXml();
            UpdateHtml();
            statementTabControl.SelectedIndex = 4;
        }

        private void UpdateHtml() {
            string header = @"<?xml version=""1.0"" encoding=""utf-8"" ?>";
            string resourceFolder = @"Resources\";

            string tempXml = Path.Combine(Path.GetTempPath(), "temp.xml");
            string tempHtml = Path.Combine(Path.GetTempPath(), "temp.htm");
            string tempXsl = Path.Combine(Path.GetTempPath(), "Problem.xsl");
            string tempDefaultCss = Path.Combine(Path.GetTempPath(), "default.css");
            string tempControlsCss = Path.Combine(Path.GetTempPath(), "fudgecontrols.css");

            string xmlContent = xmlTextBox.Text;
            string imageRegex = @"/Images/(?<id>\d+)";

            foreach (Match m in Regex.Matches(xmlContent, imageRegex)) {
                //create the image in the temp folder
                int id = Int32.Parse(m.Groups["id"].Value);
                var pic = DataContext.Pictures.SingleOrDefault(p => p.PictureId == id);
                if (pic != null) {
                    Image image = Image.FromStream(new MemoryStream(pic.Data.ToArray()));
                    image.Save(Path.Combine(Path.GetTempPath(), id + ".jpg"), ImageFormat.Jpeg);
                }
            }

            //replace the references for the html
            xmlContent = Regex.Replace(xmlContent, @"/Images/(\d+)", "$1.jpg");

            File.WriteAllText(tempXsl, File.ReadAllText(Path.Combine(resourceFolder, "Problem.xsl")));
            File.WriteAllText(tempDefaultCss, File.ReadAllText(Path.Combine(resourceFolder, "default.css")));
            File.WriteAllText(tempControlsCss, File.ReadAllText(Path.Combine(resourceFolder, "fudgecontrols.css")));
            File.WriteAllText(tempHtml, File.ReadAllText(Path.Combine(resourceFolder, "temp.htm")));
            File.WriteAllText(tempXml, header + xmlContent);

            browserPreview.Navigate(tempHtml);
        }

        private void testButton_Click(object sender, EventArgs e) {

            string[] inputs = new string[testCasesListView.Items.Count];
            string[] outputs = new string[testCasesListView.Items.Count];

            for (int i = 0; i < testCasesListView.Items.Count; ++i) {
                inputs[i] = testCasesListView.Items[i].SubItems[1].Tag.ToString();
                outputs[i] = testCasesListView.Items[i].SubItems[2].Tag.ToString();
            }

            new VerifyForm(DataContext).ShowDialog(timeLimitTextBox.Value, memoryLimitTextBox.Value, inputs, outputs);
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e) {
            UpdateUniqueKeysLabel();
        }

        private void UpdateUniqueKeysLabel() {
            string shortName = GetShortName(nameTextBox.Text);
            uniqueKeysLabel.Text = String.Format("ID: {0}, Short Name: {1}", currentProblemId.HasValue ? currentProblemId.Value.ToString() : "[not assigned]", shortName.Length != 0 ? shortName : "[empty]");
        }

        private string GetShortName(string name) {
            Regex invalidRegex = new Regex(@"[^a-zA-Z0-9\s]");
            return invalidRegex.Replace(nameTextBox.Text, String.Empty).Replace(' ', '_');
        }

        private void byRadioButtons_CheckedChanged(object sender, EventArgs e) {
            if (idRadioButton.Checked) {
                byIdTextBox.Enabled = true;
                byNameComboBox.Enabled = false;

                nameRadioButton.Checked = false;
            }
            else if (nameRadioButton.Checked) {
                byNameComboBox.Enabled = true;
                byIdTextBox.Enabled = false;

                idRadioButton.Checked = false;
            }
        }

        private void insertFromfilesToolStripMenuItem_Click(object sender, EventArgs e) {
            ParseForm.TestCase[] testCases = new ImportForm().GetTestCases();

            foreach (ParseForm.TestCase testCase in testCases) {
                ListViewItem item = new ListViewItem(new[] { String.Empty, testCase.input, testCase.output });
                item.Lazify();

                testCasesListView.Items.Add(item);
            }

            testCasesGroupBox.Text = String.Format("Test Cases ({0})", testCasesListView.Items.Count); 
        }
    }
}
