namespace Fudge.Web.AdminConsole.Controls {
    partial class ContestAnalyzer {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.problemsGroupBox = new System.Windows.Forms.GroupBox();
            this.problemsListView = new System.Windows.Forms.ListView();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.testCaseGroupBox = new System.Windows.Forms.GroupBox();
            this.testCaseListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.userGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.userListView = new System.Windows.Forms.ListView();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.userComboBox = new System.Windows.Forms.ComboBox();
            this.userTestCaseGroupBox = new System.Windows.Forms.GroupBox();
            this.userTestCaseListView = new System.Windows.Forms.ListView();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel1.SuspendLayout();
            this.problemsGroupBox.SuspendLayout();
            this.testCaseGroupBox.SuspendLayout();
            this.userGroupBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.userTestCaseGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.problemsGroupBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.testCaseGroupBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.userGroupBox, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.userTestCaseGroupBox, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(515, 503);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // problemsGroupBox
            // 
            this.problemsGroupBox.Controls.Add(this.problemsListView);
            this.problemsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemsGroupBox.Location = new System.Drawing.Point(3, 3);
            this.problemsGroupBox.Name = "problemsGroupBox";
            this.problemsGroupBox.Size = new System.Drawing.Size(509, 119);
            this.problemsGroupBox.TabIndex = 0;
            this.problemsGroupBox.TabStop = false;
            this.problemsGroupBox.Text = "Problem Breakdown for {0}";
            // 
            // problemsListView
            // 
            this.problemsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
            this.problemsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.problemsListView.FullRowSelect = true;
            this.problemsListView.Location = new System.Drawing.Point(3, 16);
            this.problemsListView.MultiSelect = false;
            this.problemsListView.Name = "problemsListView";
            this.problemsListView.Size = new System.Drawing.Size(503, 100);
            this.problemsListView.TabIndex = 0;
            this.problemsListView.UseCompatibleStateImageBehavior = false;
            this.problemsListView.View = System.Windows.Forms.View.Details;
            this.problemsListView.SelectedIndexChanged += new System.EventHandler(this.problemsListView_SelectedIndexChanged);
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "ID";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Name";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Submissions";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Accepted";
            // 
            // testCaseGroupBox
            // 
            this.testCaseGroupBox.Controls.Add(this.testCaseListView);
            this.testCaseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCaseGroupBox.Location = new System.Drawing.Point(3, 128);
            this.testCaseGroupBox.Name = "testCaseGroupBox";
            this.testCaseGroupBox.Size = new System.Drawing.Size(509, 119);
            this.testCaseGroupBox.TabIndex = 1;
            this.testCaseGroupBox.TabStop = false;
            this.testCaseGroupBox.Text = "Test Case Breakdown for {0}";
            // 
            // testCaseListView
            // 
            this.testCaseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6});
            this.testCaseListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCaseListView.FullRowSelect = true;
            this.testCaseListView.Location = new System.Drawing.Point(3, 16);
            this.testCaseListView.MultiSelect = false;
            this.testCaseListView.Name = "testCaseListView";
            this.testCaseListView.Size = new System.Drawing.Size(503, 100);
            this.testCaseListView.TabIndex = 0;
            this.testCaseListView.UseCompatibleStateImageBehavior = false;
            this.testCaseListView.View = System.Windows.Forms.View.Details;
            this.testCaseListView.SelectedIndexChanged += new System.EventHandler(this.testCaseListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Attempted";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Accepted";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Ratio";
            // 
            // userGroupBox
            // 
            this.userGroupBox.Controls.Add(this.tableLayoutPanel2);
            this.userGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userGroupBox.Location = new System.Drawing.Point(3, 378);
            this.userGroupBox.Name = "userGroupBox";
            this.userGroupBox.Size = new System.Drawing.Size(509, 122);
            this.userGroupBox.TabIndex = 2;
            this.userGroupBox.TabStop = false;
            this.userGroupBox.Text = "Test Case Breakdown for {0}\'s Final Run";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.userListView, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.userComboBox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(503, 103);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // userListView
            // 
            this.userListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader17,
            this.columnHeader8});
            this.userListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userListView.FullRowSelect = true;
            this.userListView.Location = new System.Drawing.Point(3, 30);
            this.userListView.MultiSelect = false;
            this.userListView.Name = "userListView";
            this.userListView.Size = new System.Drawing.Size(497, 70);
            this.userListView.TabIndex = 1;
            this.userListView.UseCompatibleStateImageBehavior = false;
            this.userListView.View = System.Windows.Forms.View.Details;
            this.userListView.SelectedIndexChanged += new System.EventHandler(this.userListView_SelectedIndexChanged);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ID";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Failed";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Points";
            // 
            // userComboBox
            // 
            this.userComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userComboBox.FormattingEnabled = true;
            this.userComboBox.Location = new System.Drawing.Point(3, 3);
            this.userComboBox.Name = "userComboBox";
            this.userComboBox.Size = new System.Drawing.Size(497, 21);
            this.userComboBox.TabIndex = 0;
            this.userComboBox.Visible = false;
            // 
            // userTestCaseGroupBox
            // 
            this.userTestCaseGroupBox.Controls.Add(this.userTestCaseListView);
            this.userTestCaseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userTestCaseGroupBox.Location = new System.Drawing.Point(3, 253);
            this.userTestCaseGroupBox.Name = "userTestCaseGroupBox";
            this.userTestCaseGroupBox.Size = new System.Drawing.Size(509, 119);
            this.userTestCaseGroupBox.TabIndex = 3;
            this.userTestCaseGroupBox.TabStop = false;
            this.userTestCaseGroupBox.Text = "User Breakdown for Test Case {0}";
            // 
            // userTestCaseListView
            // 
            this.userTestCaseListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.userTestCaseListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userTestCaseListView.FullRowSelect = true;
            this.userTestCaseListView.Location = new System.Drawing.Point(3, 16);
            this.userTestCaseListView.MultiSelect = false;
            this.userTestCaseListView.Name = "userTestCaseListView";
            this.userTestCaseListView.Size = new System.Drawing.Size(503, 100);
            this.userTestCaseListView.TabIndex = 0;
            this.userTestCaseListView.UseCompatibleStateImageBehavior = false;
            this.userTestCaseListView.View = System.Windows.Forms.View.Details;
            this.userTestCaseListView.SelectedIndexChanged += new System.EventHandler(this.userTestCaseListView_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "ID";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Name";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Attempted";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Accepted";
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Accepted";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Adjusted Ratio";
            // 
            // ContestAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ContestAnalyzer";
            this.Size = new System.Drawing.Size(515, 503);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.problemsGroupBox.ResumeLayout(false);
            this.testCaseGroupBox.ResumeLayout(false);
            this.userGroupBox.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.userTestCaseGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox problemsGroupBox;
        private System.Windows.Forms.GroupBox testCaseGroupBox;
        private System.Windows.Forms.ListView testCaseListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox userGroupBox;
        private System.Windows.Forms.GroupBox userTestCaseGroupBox;
        private System.Windows.Forms.ListView problemsListView;
        private System.Windows.Forms.ListView userTestCaseListView;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListView userListView;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ComboBox userComboBox;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}
