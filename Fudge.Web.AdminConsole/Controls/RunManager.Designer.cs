namespace Fudge.Web.AdminConsole.Controls {
    partial class RunManager {
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
            this.testRunsListView = new System.Windows.Forms.ListView();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.rerunButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.filterControl = new Fudge.Web.AdminConsole.Controls.FilterControl();
            this.runsListView = new Fudge.Web.AdminConsole.Controls.RunManager.RunsListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.codeTextBox = new System.Windows.Forms.RichTextBox();
            this.errorRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.testRunsListView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(758, 504);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // testRunsListView
            // 
            this.testRunsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13});
            this.testRunsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testRunsListView.FullRowSelect = true;
            this.testRunsListView.Location = new System.Drawing.Point(3, 255);
            this.testRunsListView.MultiSelect = false;
            this.testRunsListView.Name = "testRunsListView";
            this.testRunsListView.Size = new System.Drawing.Size(373, 246);
            this.testRunsListView.TabIndex = 2;
            this.testRunsListView.UseCompatibleStateImageBehavior = false;
            this.testRunsListView.View = System.Windows.Forms.View.Details;            
            this.testRunsListView.DoubleClick += new System.EventHandler(this.testRunsListView_DoubleClick);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "ID";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Input";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Expected";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Actual";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Status";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.filterControl, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.runsListView, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(752, 246);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 6;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel4.Controls.Add(this.nextButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.previousButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.deleteButton, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.rerunButton, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.refreshButton, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 214);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(752, 32);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(67, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(58, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(3, 3);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(58, 23);
            this.previousButton.TabIndex = 0;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(691, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(58, 23);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // rerunButton
            // 
            this.rerunButton.Location = new System.Drawing.Point(627, 3);
            this.rerunButton.Name = "rerunButton";
            this.rerunButton.Size = new System.Drawing.Size(58, 23);
            this.rerunButton.TabIndex = 1;
            this.rerunButton.Text = "Re-run";
            this.rerunButton.UseVisualStyleBackColor = true;
            this.rerunButton.Click += new System.EventHandler(this.rerunButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(563, 3);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(58, 23);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // filterControl
            // 
            this.filterControl.AutoSize = true;
            this.filterControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterControl.Location = new System.Drawing.Point(3, 3);
            this.filterControl.Name = "filterControl";
            this.filterControl.Size = new System.Drawing.Size(746, 26);
            this.filterControl.TabIndex = 5;
            // 
            // runsListView
            // 
            this.runsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader14});
            this.tableLayoutPanel2.SetColumnSpan(this.runsListView, 2);
            this.runsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runsListView.FullRowSelect = true;
            this.runsListView.Location = new System.Drawing.Point(3, 35);
            this.runsListView.MultiSelect = false;
            this.runsListView.Name = "runsListView";
            this.runsListView.Page = 0;
            this.runsListView.PageSize = 100;
            this.runsListView.Selector = null;
            this.runsListView.Size = new System.Drawing.Size(746, 176);
            this.runsListView.SortColumn = 0;
            this.runsListView.SortDirection = Fudge.Web.AdminConsole.Controls.SortDirection.Ascending;
            this.runsListView.Sorter = null;
            this.runsListView.TabIndex = 1;
            this.runsListView.UseCompatibleStateImageBehavior = false;
            this.runsListView.View = System.Windows.Forms.View.Details;
            this.runsListView.SelectedIndexChanged += new System.EventHandler(this.runsListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Timestamp";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "User";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Problem";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Language";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Status";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Time";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Memory";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Contest";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(382, 255);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.codeTextBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.errorRichTextBox);
            this.splitContainer.Panel2Collapsed = true;
            this.splitContainer.Size = new System.Drawing.Size(373, 246);
            this.splitContainer.SplitterDistance = 196;
            this.splitContainer.TabIndex = 4;
            // 
            // codeTextBox
            // 
            this.codeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextBox.Location = new System.Drawing.Point(0, 0);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.ReadOnly = true;
            this.codeTextBox.Size = new System.Drawing.Size(373, 246);
            this.codeTextBox.TabIndex = 5;
            this.codeTextBox.Text = "";
            this.codeTextBox.WordWrap = false;
            // 
            // errorRichTextBox
            // 
            this.errorRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorRichTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorRichTextBox.ForeColor = System.Drawing.Color.DarkRed;
            this.errorRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.errorRichTextBox.Name = "errorRichTextBox";
            this.errorRichTextBox.ReadOnly = true;
            this.errorRichTextBox.Size = new System.Drawing.Size(150, 46);
            this.errorRichTextBox.TabIndex = 0;
            this.errorRichTextBox.Text = "";
            this.errorRichTextBox.WordWrap = false;
            // 
            // RunManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RunManager";
            this.Size = new System.Drawing.Size(758, 504);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView testRunsListView;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private RunsListView runsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.RichTextBox codeTextBox;
        private System.Windows.Forms.RichTextBox errorRichTextBox;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button rerunButton;
        private System.Windows.Forms.Button refreshButton;
        private FilterControl filterControl;
    }
}
