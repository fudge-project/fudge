using Fudge.Web.AdminConsole.Controls;
namespace Fudge.Web.AdminConsole {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.manageProblemsTab = new System.Windows.Forms.TabPage();
            this.problemManager = new Fudge.Web.AdminConsole.Controls.ProblemManager();
            this.manageSchoolsTab = new System.Windows.Forms.TabPage();
            this.schoolManager = new Fudge.Web.AdminConsole.Controls.SchoolManager();
            this.manageRunsTab = new System.Windows.Forms.TabPage();
            this.runManager = new Fudge.Web.AdminConsole.Controls.RunManager();
            this.manageUsersTab = new System.Windows.Forms.TabPage();
            this.userManager = new Fudge.Web.AdminConsole.Controls.UserManager();
            this.manageContestsTab = new System.Windows.Forms.TabPage();
            this.contestManager = new Fudge.Web.AdminConsole.Controls.ContestManager();
            this.manageNewsTab = new System.Windows.Forms.TabPage();
            this.newsManager = new Fudge.Web.AdminConsole.Controls.NewsManager();
            this.manageForumsTab = new System.Windows.Forms.TabPage();
            this.forumManager = new Fudge.Web.AdminConsole.Controls.ForumManager();
            this.manageEmailsTab = new System.Windows.Forms.TabPage();
            this.emailManager = new Fudge.Web.AdminConsole.Controls.EmailManager();
            this.manageSiteTab = new System.Windows.Forms.TabPage();
            this.siteManager = new Fudge.Web.AdminConsole.Controls.SiteManager();
            this.viewLogTab = new System.Windows.Forms.TabPage();
            this.logViewer = new Fudge.Web.AdminConsole.Controls.LogViewer();
            this.tabImageList = new System.Windows.Forms.ImageList(this.components);
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectionTimer = new System.Windows.Forms.Timer(this.components);
            this.initializeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.mainTabControl.SuspendLayout();
            this.manageProblemsTab.SuspendLayout();
            this.manageSchoolsTab.SuspendLayout();
            this.manageRunsTab.SuspendLayout();
            this.manageUsersTab.SuspendLayout();
            this.manageContestsTab.SuspendLayout();
            this.manageNewsTab.SuspendLayout();
            this.manageForumsTab.SuspendLayout();
            this.manageEmailsTab.SuspendLayout();
            this.manageSiteTab.SuspendLayout();
            this.viewLogTab.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.manageProblemsTab);
            this.mainTabControl.Controls.Add(this.manageSchoolsTab);
            this.mainTabControl.Controls.Add(this.manageRunsTab);
            this.mainTabControl.Controls.Add(this.manageUsersTab);
            this.mainTabControl.Controls.Add(this.manageContestsTab);
            this.mainTabControl.Controls.Add(this.manageNewsTab);
            this.mainTabControl.Controls.Add(this.manageForumsTab);
            this.mainTabControl.Controls.Add(this.manageEmailsTab);
            this.mainTabControl.Controls.Add(this.manageSiteTab);
            this.mainTabControl.Controls.Add(this.viewLogTab);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Enabled = false;
            this.mainTabControl.ImageList = this.tabImageList;
            this.mainTabControl.Location = new System.Drawing.Point(0, 24);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(923, 445);
            this.mainTabControl.TabIndex = 0;
            // 
            // manageProblemsTab
            // 
            this.manageProblemsTab.Controls.Add(this.problemManager);
            this.manageProblemsTab.ImageIndex = 4;
            this.manageProblemsTab.Location = new System.Drawing.Point(4, 23);
            this.manageProblemsTab.Name = "manageProblemsTab";
            this.manageProblemsTab.Padding = new System.Windows.Forms.Padding(3);
            this.manageProblemsTab.Size = new System.Drawing.Size(915, 418);
            this.manageProblemsTab.TabIndex = 0;
            this.manageProblemsTab.Text = "Problems";
            this.manageProblemsTab.UseVisualStyleBackColor = true;
            // 
            // problemManager
            // 
            this.problemManager.DataContext = null;
            this.problemManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemManager.Location = new System.Drawing.Point(3, 3);
            this.problemManager.Name = "problemManager";
            this.problemManager.Size = new System.Drawing.Size(909, 412);
            this.problemManager.TabIndex = 0;
            // 
            // manageSchoolsTab
            // 
            this.manageSchoolsTab.Controls.Add(this.schoolManager);
            this.manageSchoolsTab.ImageIndex = 0;
            this.manageSchoolsTab.Location = new System.Drawing.Point(4, 23);
            this.manageSchoolsTab.Name = "manageSchoolsTab";
            this.manageSchoolsTab.Size = new System.Drawing.Size(915, 418);
            this.manageSchoolsTab.TabIndex = 1;
            this.manageSchoolsTab.Text = "Schools";
            this.manageSchoolsTab.UseVisualStyleBackColor = true;
            // 
            // schoolManager
            // 
            this.schoolManager.DataContext = null;
            this.schoolManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schoolManager.Location = new System.Drawing.Point(0, 0);
            this.schoolManager.Name = "schoolManager";
            this.schoolManager.Size = new System.Drawing.Size(915, 418);
            this.schoolManager.TabIndex = 0;
            // 
            // manageRunsTab
            // 
            this.manageRunsTab.Controls.Add(this.runManager);
            this.manageRunsTab.ImageIndex = 1;
            this.manageRunsTab.Location = new System.Drawing.Point(4, 23);
            this.manageRunsTab.Name = "manageRunsTab";
            this.manageRunsTab.Padding = new System.Windows.Forms.Padding(3);
            this.manageRunsTab.Size = new System.Drawing.Size(915, 418);
            this.manageRunsTab.TabIndex = 2;
            this.manageRunsTab.Text = "Runs";
            this.manageRunsTab.UseVisualStyleBackColor = true;
            // 
            // runManager
            // 
            this.runManager.DataContext = null;
            this.runManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runManager.Location = new System.Drawing.Point(3, 3);
            this.runManager.Name = "runManager";
            this.runManager.Size = new System.Drawing.Size(909, 412);
            this.runManager.TabIndex = 0;
            // 
            // manageUsersTab
            // 
            this.manageUsersTab.Controls.Add(this.userManager);
            this.manageUsersTab.ImageIndex = 2;
            this.manageUsersTab.Location = new System.Drawing.Point(4, 23);
            this.manageUsersTab.Name = "manageUsersTab";
            this.manageUsersTab.Padding = new System.Windows.Forms.Padding(3);
            this.manageUsersTab.Size = new System.Drawing.Size(915, 418);
            this.manageUsersTab.TabIndex = 3;
            this.manageUsersTab.Text = "Users";
            this.manageUsersTab.UseVisualStyleBackColor = true;
            // 
            // userManager
            // 
            this.userManager.DataContext = null;
            this.userManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userManager.Location = new System.Drawing.Point(3, 3);
            this.userManager.Name = "userManager";
            this.userManager.Size = new System.Drawing.Size(909, 412);
            this.userManager.TabIndex = 0;
            // 
            // manageContestsTab
            // 
            this.manageContestsTab.Controls.Add(this.contestManager);
            this.manageContestsTab.Location = new System.Drawing.Point(4, 23);
            this.manageContestsTab.Name = "manageContestsTab";
            this.manageContestsTab.Padding = new System.Windows.Forms.Padding(3);
            this.manageContestsTab.Size = new System.Drawing.Size(915, 418);
            this.manageContestsTab.TabIndex = 8;
            this.manageContestsTab.Text = "Contests";
            this.manageContestsTab.UseVisualStyleBackColor = true;
            // 
            // contestManager
            // 
            this.contestManager.DataContext = null;
            this.contestManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contestManager.Location = new System.Drawing.Point(3, 3);
            this.contestManager.Name = "contestManager";
            this.contestManager.Size = new System.Drawing.Size(909, 412);
            this.contestManager.TabIndex = 0;
            // 
            // manageNewsTab
            // 
            this.manageNewsTab.Controls.Add(this.newsManager);
            this.manageNewsTab.ImageIndex = 5;
            this.manageNewsTab.Location = new System.Drawing.Point(4, 23);
            this.manageNewsTab.Name = "manageNewsTab";
            this.manageNewsTab.Padding = new System.Windows.Forms.Padding(3);
            this.manageNewsTab.Size = new System.Drawing.Size(915, 418);
            this.manageNewsTab.TabIndex = 6;
            this.manageNewsTab.Text = "News";
            this.manageNewsTab.UseVisualStyleBackColor = true;
            // 
            // newsManager
            // 
            this.newsManager.DataContext = null;
            this.newsManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newsManager.Location = new System.Drawing.Point(3, 3);
            this.newsManager.Name = "newsManager";
            this.newsManager.Size = new System.Drawing.Size(909, 412);
            this.newsManager.TabIndex = 0;
            // 
            // manageForumsTab
            // 
            this.manageForumsTab.Controls.Add(this.forumManager);
            this.manageForumsTab.ImageIndex = 6;
            this.manageForumsTab.Location = new System.Drawing.Point(4, 23);
            this.manageForumsTab.Name = "manageForumsTab";
            this.manageForumsTab.Size = new System.Drawing.Size(915, 418);
            this.manageForumsTab.TabIndex = 7;
            this.manageForumsTab.Text = "Forums";
            this.manageForumsTab.UseVisualStyleBackColor = true;
            // 
            // forumManager
            // 
            this.forumManager.DataContext = null;
            this.forumManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.forumManager.Location = new System.Drawing.Point(0, 0);
            this.forumManager.Name = "forumManager";
            this.forumManager.Size = new System.Drawing.Size(915, 418);
            this.forumManager.TabIndex = 0;
            // 
            // manageEmailsTab
            // 
            this.manageEmailsTab.Controls.Add(this.emailManager);
            this.manageEmailsTab.Location = new System.Drawing.Point(4, 23);
            this.manageEmailsTab.Name = "manageEmailsTab";
            this.manageEmailsTab.Padding = new System.Windows.Forms.Padding(3);
            this.manageEmailsTab.Size = new System.Drawing.Size(915, 418);
            this.manageEmailsTab.TabIndex = 9;
            this.manageEmailsTab.Text = "Mass Email";
            this.manageEmailsTab.UseVisualStyleBackColor = true;
            // 
            // emailManager
            // 
            this.emailManager.DataContext = null;
            this.emailManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailManager.Location = new System.Drawing.Point(3, 3);
            this.emailManager.Name = "emailManager";
            this.emailManager.Size = new System.Drawing.Size(909, 412);
            this.emailManager.TabIndex = 0;
            // 
            // manageSiteTab
            // 
            this.manageSiteTab.Controls.Add(this.siteManager);
            this.manageSiteTab.ImageIndex = 7;
            this.manageSiteTab.Location = new System.Drawing.Point(4, 23);
            this.manageSiteTab.Name = "manageSiteTab";
            this.manageSiteTab.Padding = new System.Windows.Forms.Padding(3);
            this.manageSiteTab.Size = new System.Drawing.Size(915, 418);
            this.manageSiteTab.TabIndex = 5;
            this.manageSiteTab.Text = "Site Management";
            this.manageSiteTab.UseVisualStyleBackColor = true;
            // 
            // siteManager
            // 
            this.siteManager.DataContext = null;
            this.siteManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siteManager.Location = new System.Drawing.Point(3, 3);
            this.siteManager.Name = "siteManager";
            this.siteManager.Size = new System.Drawing.Size(909, 412);
            this.siteManager.TabIndex = 0;
            // 
            // viewLogTab
            // 
            this.viewLogTab.Controls.Add(this.logViewer);
            this.viewLogTab.ImageIndex = 3;
            this.viewLogTab.Location = new System.Drawing.Point(4, 23);
            this.viewLogTab.Name = "viewLogTab";
            this.viewLogTab.Padding = new System.Windows.Forms.Padding(3);
            this.viewLogTab.Size = new System.Drawing.Size(915, 418);
            this.viewLogTab.TabIndex = 4;
            this.viewLogTab.Text = "Log Viewer";
            this.viewLogTab.UseVisualStyleBackColor = true;
            // 
            // logViewer
            // 
            this.logViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logViewer.Location = new System.Drawing.Point(3, 3);
            this.logViewer.Name = "logViewer";
            this.logViewer.Size = new System.Drawing.Size(909, 412);
            this.logViewer.TabIndex = 0;
            // 
            // tabImageList
            // 
            this.tabImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("tabImageList.ImageStream")));
            this.tabImageList.TransparentColor = System.Drawing.Color.Black;
            this.tabImageList.Images.SetKeyName(0, "school.bmp");
            this.tabImageList.Images.SetKeyName(1, "runs.bmp");
            this.tabImageList.Images.SetKeyName(2, "users.bmp");
            this.tabImageList.Images.SetKeyName(3, "log.bmp");
            this.tabImageList.Images.SetKeyName(4, "problems.bmp");
            this.tabImageList.Images.SetKeyName(5, "newnews.bmp");
            this.tabImageList.Images.SetKeyName(6, "left_quote.bmp");
            this.tabImageList.Images.SetKeyName(7, "website.bmp");
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(923, 24);
            this.mainMenuStrip.TabIndex = 1;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectConsoleToolStripMenuItem,
            this.disconnectConsoleToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // connectConsoleToolStripMenuItem
            // 
            this.connectConsoleToolStripMenuItem.Image = global::Fudge.Web.AdminConsole.Properties.Resources.connect;
            this.connectConsoleToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.connectConsoleToolStripMenuItem.Name = "connectConsoleToolStripMenuItem";
            this.connectConsoleToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.connectConsoleToolStripMenuItem.Text = "&Connect Console...";
            this.connectConsoleToolStripMenuItem.Click += new System.EventHandler(this.connectConsoleToolStripMenuItem_Click);
            // 
            // disconnectConsoleToolStripMenuItem
            // 
            this.disconnectConsoleToolStripMenuItem.Image = global::Fudge.Web.AdminConsole.Properties.Resources.disconnect;
            this.disconnectConsoleToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.disconnectConsoleToolStripMenuItem.Name = "disconnectConsoleToolStripMenuItem";
            this.disconnectConsoleToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.disconnectConsoleToolStripMenuItem.Text = "&Disconnect Console...";
            this.disconnectConsoleToolStripMenuItem.Click += new System.EventHandler(this.disconnectConsoleToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(185, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emailSettingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // emailSettingsToolStripMenuItem
            // 
            this.emailSettingsToolStripMenuItem.Name = "emailSettingsToolStripMenuItem";
            this.emailSettingsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.emailSettingsToolStripMenuItem.Text = "&Email Settings...";
            this.emailSettingsToolStripMenuItem.Click += new System.EventHandler(this.emailSettingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripStatusLabel,
            this.timeToolStripStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 469);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(923, 22);
            this.mainStatusStrip.TabIndex = 2;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // statusToolStripStatusLabel
            // 
            this.statusToolStripStatusLabel.Name = "statusToolStripStatusLabel";
            this.statusToolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusToolStripStatusLabel.Text = "Ready";
            // 
            // timeToolStripStatusLabel
            // 
            this.timeToolStripStatusLabel.Name = "timeToolStripStatusLabel";
            this.timeToolStripStatusLabel.Size = new System.Drawing.Size(869, 17);
            this.timeToolStripStatusLabel.Spring = true;
            this.timeToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // connectionTimer
            // 
            this.connectionTimer.Interval = 1000;
            this.connectionTimer.Tick += new System.EventHandler(this.connectionTimer_Tick);
            // 
            // initializeBackgroundWorker
            // 
            this.initializeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.initializeBackgroundWorker_DoWork);
            this.initializeBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.initializeBackgroundWorker_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 491);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Fudge - Administration Console";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.mainTabControl.ResumeLayout(false);
            this.manageProblemsTab.ResumeLayout(false);
            this.manageSchoolsTab.ResumeLayout(false);
            this.manageRunsTab.ResumeLayout(false);
            this.manageUsersTab.ResumeLayout(false);
            this.manageContestsTab.ResumeLayout(false);
            this.manageNewsTab.ResumeLayout(false);
            this.manageForumsTab.ResumeLayout(false);
            this.manageEmailsTab.ResumeLayout(false);
            this.manageSiteTab.ResumeLayout(false);
            this.viewLogTab.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage manageProblemsTab;
        
        private System.Windows.Forms.TabPage manageSchoolsTab;
        
        private System.Windows.Forms.TabPage manageRunsTab;
        
        private System.Windows.Forms.TabPage manageUsersTab;
        
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel timeToolStripStatusLabel;
        private System.Windows.Forms.Timer connectionTimer;
        private System.Windows.Forms.TabPage viewLogTab;
        private Fudge.Web.AdminConsole.Controls.LogViewer logViewer;
        private System.Windows.Forms.ImageList tabImageList;
        private System.Windows.Forms.TabPage manageSiteTab;
        
        private System.Windows.Forms.TabPage manageNewsTab;

        private System.Windows.Forms.TabPage manageForumsTab;
        private ProblemManager problemManager;
        private SchoolManager schoolManager;
        private RunManager runManager;
        private UserManager userManager;
        private NewsManager newsManager;
        private ForumManager forumManager;
        private SiteManager siteManager;
        private System.Windows.Forms.TabPage manageContestsTab;
        private ContestManager contestManager;
        private System.Windows.Forms.TabPage manageEmailsTab;
        private EmailManager emailManager;
        private System.ComponentModel.BackgroundWorker initializeBackgroundWorker;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

