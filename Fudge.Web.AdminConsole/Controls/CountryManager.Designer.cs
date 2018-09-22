namespace Fudge.Web.AdminConsole.Controls {
    partial class CountryManager {
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
            this.components = new System.ComponentModel.Container();
            this.countryListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.countryListViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countryImageList = new System.Windows.Forms.ImageList(this.components);
            this.countryListViewContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // countryListView
            // 
            this.countryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.countryListView.ContextMenuStrip = this.countryListViewContextMenu;
            this.countryListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.countryListView.FullRowSelect = true;
            this.countryListView.LargeImageList = this.countryImageList;
            this.countryListView.Location = new System.Drawing.Point(0, 0);
            this.countryListView.MultiSelect = false;
            this.countryListView.Name = "countryListView";
            this.countryListView.Size = new System.Drawing.Size(557, 330);
            this.countryListView.SmallImageList = this.countryImageList;
            this.countryListView.TabIndex = 0;
            this.countryListView.UseCompatibleStateImageBehavior = false;
            this.countryListView.View = System.Windows.Forms.View.Details;
            this.countryListView.SelectedIndexChanged += new System.EventHandler(this.countryListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Flag";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ID";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Name";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Short Name";
            // 
            // countryListViewContextMenu
            // 
            this.countryListViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editFlagToolStripMenuItem});
            this.countryListViewContextMenu.Name = "countryListViewContextMenu";
            this.countryListViewContextMenu.Size = new System.Drawing.Size(129, 26);
            // 
            // editFlagToolStripMenuItem
            // 
            this.editFlagToolStripMenuItem.Name = "editFlagToolStripMenuItem";
            this.editFlagToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.editFlagToolStripMenuItem.Text = "Edit &Flag...";
            this.editFlagToolStripMenuItem.Click += new System.EventHandler(this.editFlagToolStripMenuItem_Click);
            // 
            // countryImageList
            // 
            this.countryImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.countryImageList.ImageSize = new System.Drawing.Size(18, 12);
            this.countryImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // CountryManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.countryListView);
            this.Name = "CountryManager";
            this.Size = new System.Drawing.Size(557, 330);
            this.countryListViewContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView countryListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ImageList countryImageList;
        private System.Windows.Forms.ContextMenuStrip countryListViewContextMenu;
        private System.Windows.Forms.ToolStripMenuItem editFlagToolStripMenuItem;
    }
}
