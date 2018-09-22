namespace Fudge.Web.AdminConsole.Controls {
    partial class SchoolManager {
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.schoolBrowser = new System.Windows.Forms.WebBrowser();
            this.suggestedSchoolListView = new Fudge.Web.AdminConsole.Controls.SchoolManager.SuggestedSchoolListView();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.schoolListView = new Fudge.Web.AdminConsole.Controls.SchoolManager.SchoolListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.previousButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.filterControl = new Fudge.Web.AdminConsole.Controls.FilterControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.refreshButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(944, 441);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.schoolBrowser, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.suggestedSchoolListView, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 214F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(938, 214);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // schoolBrowser
            // 
            this.schoolBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schoolBrowser.Location = new System.Drawing.Point(472, 3);
            this.schoolBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.schoolBrowser.Name = "schoolBrowser";
            this.schoolBrowser.Size = new System.Drawing.Size(463, 208);
            this.schoolBrowser.TabIndex = 0;
            // 
            // suggestedSchoolListView
            // 
            this.suggestedSchoolListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader8});
            this.suggestedSchoolListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.suggestedSchoolListView.FullRowSelect = true;
            this.suggestedSchoolListView.Location = new System.Drawing.Point(3, 3);
            this.suggestedSchoolListView.MultiSelect = false;
            this.suggestedSchoolListView.Name = "suggestedSchoolListView";
            this.suggestedSchoolListView.Page = 0;
            this.suggestedSchoolListView.PageSize = 100;
            this.suggestedSchoolListView.Selector = null;
            this.suggestedSchoolListView.Size = new System.Drawing.Size(463, 208);
            this.suggestedSchoolListView.SortColumn = 0;
            this.suggestedSchoolListView.SortDirection = Fudge.Web.AdminConsole.Controls.SortDirection.Ascending;
            this.suggestedSchoolListView.Sorter = null;
            this.suggestedSchoolListView.TabIndex = 1;
            this.suggestedSchoolListView.UseCompatibleStateImageBehavior = false;
            this.suggestedSchoolListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "ID";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 158;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Domain";
            this.columnHeader2.Width = 170;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Email";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 340F));
            this.tableLayoutPanel3.Controls.Add(this.schoolListView, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.filterControl, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 2, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 223);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(938, 215);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // schoolListView
            // 
            this.schoolListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.tableLayoutPanel3.SetColumnSpan(this.schoolListView, 3);
            this.schoolListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schoolListView.FullRowSelect = true;
            this.schoolListView.Location = new System.Drawing.Point(3, 3);
            this.schoolListView.MultiSelect = false;
            this.schoolListView.Name = "schoolListView";
            this.schoolListView.Page = 0;
            this.schoolListView.PageSize = 100;
            this.schoolListView.Selector = null;
            this.schoolListView.Size = new System.Drawing.Size(932, 177);
            this.schoolListView.SortColumn = 0;
            this.schoolListView.SortDirection = Fudge.Web.AdminConsole.Controls.SortDirection.Ascending;
            this.schoolListView.Sorter = null;
            this.schoolListView.TabIndex = 0;
            this.schoolListView.UseCompatibleStateImageBehavior = false;
            this.schoolListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ID";
            this.columnHeader3.Width = 39;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 159;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Domain";
            this.columnHeader5.Width = 153;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Country";
            this.columnHeader6.Width = 194;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.previousButton);
            this.flowLayoutPanel1.Controls.Add(this.nextButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 183);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(180, 32);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(3, 3);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(75, 23);
            this.previousButton.TabIndex = 6;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(84, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 7;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // filterControl
            // 
            this.filterControl.AutoSize = true;
            this.filterControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterControl.Location = new System.Drawing.Point(180, 183);
            this.filterControl.Margin = new System.Windows.Forms.Padding(0);
            this.filterControl.Name = "filterControl";
            this.filterControl.Size = new System.Drawing.Size(418, 32);
            this.filterControl.TabIndex = 8;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.refreshButton);
            this.flowLayoutPanel2.Controls.Add(this.deleteButton);
            this.flowLayoutPanel2.Controls.Add(this.editButton);
            this.flowLayoutPanel2.Controls.Add(this.addButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(598, 183);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(340, 32);
            this.flowLayoutPanel2.TabIndex = 9;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(262, 3);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(181, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // editButton
            // 
            this.editButton.Enabled = false;
            this.editButton.Location = new System.Drawing.Point(100, 3);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(75, 23);
            this.editButton.TabIndex = 1;
            this.editButton.Text = "Edit...";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(19, 3);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Add...";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Type";
            // 
            // SchoolManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SchoolManager";
            this.Size = new System.Drawing.Size(944, 441);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.WebBrowser schoolBrowser;
        private SuggestedSchoolListView suggestedSchoolListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private SchoolListView schoolListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private FilterControl filterControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ColumnHeader columnHeader7;
    }
}
