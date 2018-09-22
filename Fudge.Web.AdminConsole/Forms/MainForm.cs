using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using System.Xml;
using System.Xml.Linq;
using System.Web;
using Fudge.Web.AdminConsole.Forms;
using Fudge.Web.AdminConsole.Controls;
using System.Data.SqlClient;
using System.Threading;

namespace Fudge.Web.AdminConsole {
    public partial class MainForm : Form {

        public FudgeDataContext DataContext { get; private set; }
        public DateTime LoginTime { get; private set; }
        public ProgressDialog ProgressDialog { get; private set; }

        public MainForm() {
            InitializeComponent();
            connectionTimer.Start();
        }

        private void Connect() {

            ConnectForm connectForm = new ConnectForm();

            if (connectForm.ShowDialog() == DialogResult.OK) {
                DataContext = connectForm.DataContext;
                DataContext.Log = logViewer.TextBoxWriter;

                ProgressDialog = new ProgressDialog(false);
                initializeBackgroundWorker.RunWorkerAsync();                
                ProgressDialog.ShowDialog(this);
            }
        }

        private void Disconnect() {
            mainTabControl.Enabled = false;

            if (DataContext != null) {
                DataContext.Dispose();
                DataContext = null;
            }
            disconnectConsoleToolStripMenuItem.Enabled = false;
            connectConsoleToolStripMenuItem.Enabled = true;
            statusToolStripStatusLabel.Text = "Ready";
        }

        private void connectConsoleToolStripMenuItem_Click(object sender, EventArgs e) {
            Connect();
        }

        private void MainForm_Shown(object sender, EventArgs e) {
            Connect();
        }

        private void disconnectConsoleToolStripMenuItem_Click(object sender, EventArgs e) {
            Disconnect();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Disconnect();
            Close();
        }

        private void connectionTimer_Tick(object sender, EventArgs e) {
            if (DataContext != null) {
                TimeSpan connectionTime = DateTime.Now - LoginTime;
                timeToolStripStatusLabel.Text = String.Format("{0:D2}:{1:D2}:{2:D2}", connectionTime.Hours, connectionTime.Minutes, connectionTime.Seconds);
            }
        }

        private void initializeBackgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            while (!ProgressDialog.Created) ;
            ManagerControl[] managers = new ManagerControl[] { problemManager, schoolManager, runManager, userManager, contestManager, newsManager, forumManager, emailManager, /* siteManager */ };

            foreach (ManagerControl manager in managers) {             
                ProgressDialog.progressBar.Invoke(new Action(() => {
                    ProgressDialog.progressBar.Increment(100 / managers.Length);
                }));

                manager.Invoke(new Action(() => {
                    try {
                        manager.DataContext = DataContext;
                        manager.Initialize();
                        manager.Enabled = true;
                    }
                    catch (Exception ex) {
                        manager.Enabled = false;

                        logViewer.Invoke(new Action(() => {
                            logViewer.TextBoxWriter.WriteLine("Will not load {0}: {1}", manager.ToString(), ex.Message);
                        }));
                    }
                }));
            }
        }

        private void initializeBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

            LoginTime = DateTime.Now;

            statusToolStripStatusLabel.Text = "Connected";
            connectConsoleToolStripMenuItem.Enabled = false;
            disconnectConsoleToolStripMenuItem.Enabled = true;

            mainTabControl.Enabled = true;

            ProgressDialog.Hide();
        }

        private void emailSettingsToolStripMenuItem_Click(object sender, EventArgs e) {
            new EmailSettingsForm().ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            new AboutBox().ShowDialog();
        }
    }
}
