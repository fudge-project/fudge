using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Fudge.Framework;
using Fudge.Framework.Database;
using Fudge.Modules.Execute;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Fudge.Web.AdminConsole.Forms;
using Fudge.Web.AdminConsole.Utils;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class ProblemVerifier : ManagerControl {
        private List<string> _inputs;

        public int TimeLimit { get; set; }
        public int MemoryLimit { get; set; }

        public ProblemVerifier() {
            InitializeComponent();

            _inputs = new List<string>();
        }

        public override void Initialize() {
            languageComboBox.Items.AddRange(DataContext.Languages.ToArray());
            languageComboBox.SelectedIndex = 0;
        }

        public void AddTestCases(string[] inputs, string[] outputs) {
            if (inputs.Length != outputs.Length) {
                throw new Exception("Cases input/output/sample mismatch!");
            }

            _inputs.Clear();

            for (int i = 0; i < inputs.Length; ++i) {
                _inputs.Add(inputs[i].TrimEnd(null));
                testCasesListView.Items.Add(new ListViewItem(new[] { inputs[i].TrimEnd(null), outputs[i].TrimEnd(null), String.Empty, String.Empty }));
            }

            testCasesListView.Lazify();
        }

        public void Verify() {
            BackgroundWorker bw = new BackgroundWorker();
            ProgressDialog progressDialog = new ProgressDialog(false, true);
            ExecuteResponse response = null;

            int languageId = ((Framework.Database.Language)languageComboBox.SelectedItem).LanguageId;
            string source = codeTextBox.Text;

            bw.DoWork += (sender, args) => { response = Execute(languageId, source); };
            
            bw.RunWorkerCompleted += (sender, args) => {
                progressDialog.Hide();

                if (response == null) {
                    MessageBox.Show("The connection could not be established, or timed out", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else {
                    ProcessResponse(response);
                }
            };

            bw.RunWorkerAsync();
            progressDialog.ShowDialog();
        }

        private void ProcessResponse(ExecuteResponse response) {
            foreach (ListViewItem item in testCasesListView.Items) {
                item.BackColor = Color.Empty;
                item.SubItems[2].Text = String.Empty;
            }

            splitContainer.Panel2Collapsed = true;

            if (response.Response != 0) {
                splitContainer.Panel2Collapsed = false;
            }

            if (response.Response == 1) {
                errorRichTexBox.Text = response.Outputs[0];
            }
            else {
                if (response.Response == 2) {
                    errorRichTexBox.Text = TimeLimit + " ms time limit exceeded";
                }

                if (response.Response == 3) {
                    errorRichTexBox.Text = MemoryLimit + " bytes memory limit exceeded";
                }

                if (response.Response == 4) {
                    errorRichTexBox.Text = response.Outputs.Last();
                    response.Outputs.RemoveAt(response.Outputs.Count - 1);
                }

                for (int i = 0; i < response.Outputs.Count; ++i) {
                    testCasesListView.Items[i].SubItems[2].Text = response.Outputs[i].TrimEnd(null);
                    testCasesListView.Items[i].SubItems[2].Lazify();

                    if (testCasesListView.Items[i].SubItems[1].Tag.ToString() != testCasesListView.Items[i].SubItems[2].Tag.ToString()) {
                        testCasesListView.Items[i].BackColor = Color.Pink;
                    }
                    else {
                        testCasesListView.Items[i].BackColor = Color.LightGreen;
                    }
                }
            }
        }

        private ExecuteResponse Execute(int languageId, string source) {
            try {
                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(IPAddress.Parse("163.118.202.146"), 5557);
                sender.SendTimeout = sender.ReceiveTimeout = TimeLimit + 12000;

                ExecuteResponse response = ExecuteModule.SendRequest(new ExecuteRequest(languageId, source, TimeLimit, MemoryLimit, _inputs), sender);

                sender.Close();
                return response;
            }
            catch (Exception ) {
                return null;
            }
        }

        private void testCasesListView_DoubleClick(object sender, EventArgs e) {
            if (testCasesListView.SelectedItems.Count > 0) {
                new TestRunForm().ShowDialog(testCasesListView.SelectedItems[0].SubItems[0].Tag.ToString(),
                                            testCasesListView.SelectedItems[0].SubItems[1].Tag.ToString(),
                                            testCasesListView.SelectedItems[0].SubItems[2].Tag.ToString());
            }
        }
    }
}
