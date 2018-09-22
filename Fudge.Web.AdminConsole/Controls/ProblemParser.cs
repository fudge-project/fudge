using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;
using Fudge.Web.AdminConsole.Forms;
using Fudge.Web.AdminConsole.Utils;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class ProblemParser : UserControl {

        List<string> results = new List<string>();

        static string separator = "~~~~";
        static string[][] snippets = { new[] { "1 per line", @"print while gets << ""~~~~""" }, 
                                         new[]  { "separated by delimiter" , "delim = \"\"\r\nwhile s = gets\r\n\tif s.chomp == delim\r\n\t\tprint \"~~~~\"\r\n\telse\r\n\t\tprint s\r\n\tend\r\nend"}, 
                                         new[] { "n lines per case", "while s = gets\r\n\tprint s\r\n\ts.to_i.times do\r\n\t\tprint gets\r\n\tend\r\n\tprint \"~~~~\"\r\nend" } };

        public ProblemParser() {
            InitializeComponent();

            foreach (string[] snippet in snippets) {
                snippetComboBox.Items.Add(snippet[0]);
            }
        }

        public string[] GetResults() {

            return results.ToArray();
        }

        private static string[] Parse(string input, string language, string separator) {
            try {
                string fileName = Path.Combine(Path.GetTempPath(), "parser.rb");
                string inputFile = Path.Combine(Path.GetTempPath(), "parser.in");
                string outFile = Path.Combine(Path.GetTempPath(), "parser.out");
                string batchFile = Path.Combine(Path.GetTempPath(), "parser.bat");

                string batchText = String.Format(@"{0} < {1} > {2}", fileName, inputFile, outFile);

                File.WriteAllText(inputFile, input);
                File.WriteAllText(fileName, language);
                File.WriteAllText(batchFile, batchText);

                Process parser = new Process();
                parser.StartInfo = new ProcessStartInfo {
                    FileName = batchFile,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                parser.Start();
                parser.WaitForExit();
                string output = File.ReadAllText(outFile);

                File.Delete(fileName);
                File.Delete(inputFile);
                File.Delete(outFile);
                File.Delete(batchFile);

                return output.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            return new string[] { };
        }

        private void resultsListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (resultsListView.SelectedIndices.Count > 0) {
                resultsTextBox.Text = results[resultsListView.SelectedIndices[0]];
            }
        }

        private void snippetComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            languageTextBox.Text = snippets[snippetComboBox.SelectedIndex][1];
        }

        private void parseButton_Click(object sender, EventArgs e) {
            resultsListView.Items.Clear();
          
            BackgroundWorker bw = new BackgroundWorker();
            ProgressDialog progressDialog = new ProgressDialog(false, true);

            results.Clear();

            string originalText = originalTextBox.Text.Replace("\r\n", "\n").Replace("\n", Environment.NewLine);
            string languageText = languageTextBox.Text;

            bw.DoWork += (bwSender, bwArgs) => { results.AddRange(Parse(originalText, languageText, separator)); };            

            bw.RunWorkerCompleted += (bwSender, bwArgs) => {
                resultsListView.VirtualListSize = results.Count;
                progressDialog.Hide();
            };

            bw.RunWorkerAsync();
            progressDialog.ShowDialog();

            resultsCountLabel.Text = String.Format("{0} result blocks", results.Count);
        }

        private void ReadFileContents() {

            originalTextBox.Text = String.Empty;

            BackgroundWorker bw = new BackgroundWorker();
            ProgressDialog progressDialog = new ProgressDialog(false, true);
            
            string inputUri = inputUriTextBox.Text;

            bw.DoWork += (sender, e) => {
                if (inputUri.StartsWith("file://")) {
                    string path = inputUri.Remove(0, 7);

                    originalTextBox.Invoke(new Action(() => { originalTextBox.Text = File.ReadAllText(path); }));
                }
                else {
                    try {
                        originalTextBox.Invoke(new Action(() => { originalTextBox.Text = ASCIIEncoding.Default.GetString(new WebClient().DownloadData(inputUri)); }));
                    }
                    catch (Exception) {
                        MessageBox.Show("Could not connect to the server and download data", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            bw.RunWorkerCompleted += (sender, e) => {                                   
                progressDialog.Hide();
            };

            bw.RunWorkerAsync();
            progressDialog.ShowDialog();
        }

        private void fetchButton_Click(object sender, EventArgs e) {
            ReadFileContents();
        }

        private void browseButton_Click(object sender, EventArgs e) {
            if (inputOpenFileDialog.ShowDialog() == DialogResult.OK) {
                inputUriTextBox.Text = "file://" + inputOpenFileDialog.FileName;
                ReadFileContents();
            }
        }

        private void resultsListView_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete && resultsListView.SelectedIndices.Count > 0) {
                results.RemoveAt(resultsListView.SelectedIndices[0]);
                resultsListView.VirtualListSize = results.Count;

                resultsCountLabel.Text = String.Format("{0} result blocks", resultsListView.Items.Count);
            }
        }

        private void resultsTextBox_TextChanged(object sender, EventArgs e) {
            if (resultsListView.SelectedIndices.Count > 0) {
                results[resultsListView.SelectedIndices[0]] = resultsTextBox.Text;
                Update();
            }
        }

        private void resultsListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            e.Item = new ListViewItem(results[e.ItemIndex]);
            e.Item.Lazify();
        }
    }
}
