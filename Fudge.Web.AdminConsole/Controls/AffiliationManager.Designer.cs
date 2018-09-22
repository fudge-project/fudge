namespace Fudge.Web.AdminConsole.Controls {
    partial class AffiliationManager {
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
            this.affiliationsListView = new Fudge.Web.AdminConsole.Controls.AffiliationManager.AffiliationsListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // affiliationsListView
            // 
            this.affiliationsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.affiliationsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.affiliationsListView.FullRowSelect = true;
            this.affiliationsListView.Location = new System.Drawing.Point(0, 0);
            this.affiliationsListView.MultiSelect = false;
            this.affiliationsListView.Name = "affiliationsListView";
            this.affiliationsListView.Page = 0;
            this.affiliationsListView.PageSize = 100;
            this.affiliationsListView.Selector = null;
            this.affiliationsListView.Size = new System.Drawing.Size(488, 222);
            this.affiliationsListView.SortColumn = 0;
            this.affiliationsListView.SortDirection = Fudge.Web.AdminConsole.Controls.SortDirection.Ascending;
            this.affiliationsListView.Sorter = null;
            this.affiliationsListView.TabIndex = 0;
            this.affiliationsListView.UseCompatibleStateImageBehavior = false;
            this.affiliationsListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Entity";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Entity Type";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Email";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Join Time";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Leave Time";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Type";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Status";
            // 
            // AffiliationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.affiliationsListView);
            this.Name = "AffiliationManager";
            this.Size = new System.Drawing.Size(488, 222);
            this.ResumeLayout(false);

        }

        #endregion

        private AffiliationsListView affiliationsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
    }
}
