namespace Fudge.Web.AdminConsole.Controls {
    partial class ProblemVerifier {
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
            this.testCasesListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.codeTextBox = new System.Windows.Forms.RichTextBox();
            this.errorRichTexBox = new System.Windows.Forms.RichTextBox();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.testCasesListView, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.languageComboBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(579, 454);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // testCasesListView
            // 
            this.testCasesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.testCasesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCasesListView.FullRowSelect = true;
            this.testCasesListView.Location = new System.Drawing.Point(3, 313);
            this.testCasesListView.Name = "testCasesListView";
            this.testCasesListView.Size = new System.Drawing.Size(573, 138);
            this.testCasesListView.TabIndex = 2;
            this.testCasesListView.UseCompatibleStateImageBehavior = false;
            this.testCasesListView.View = System.Windows.Forms.View.Details;
            this.testCasesListView.DoubleClick += new System.EventHandler(this.testCasesListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Input";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Expected Output";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Output";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Status";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 27);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.codeTextBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.errorRichTexBox);
            this.splitContainer.Panel2Collapsed = true;
            this.splitContainer.Size = new System.Drawing.Size(573, 280);
            this.splitContainer.SplitterDistance = 25;
            this.splitContainer.TabIndex = 3;
            // 
            // codeTextBox
            // 
            this.codeTextBox.AcceptsTab = true;
            this.codeTextBox.DetectUrls = false;
            this.codeTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeTextBox.Location = new System.Drawing.Point(0, 0);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(573, 280);
            this.codeTextBox.TabIndex = 5;
            this.codeTextBox.Text = "";
            this.codeTextBox.WordWrap = false;
            // 
            // errorRichTexBox
            // 
            this.errorRichTexBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorRichTexBox.Location = new System.Drawing.Point(0, 0);
            this.errorRichTexBox.Name = "errorRichTexBox";
            this.errorRichTexBox.Size = new System.Drawing.Size(150, 46);
            this.errorRichTexBox.TabIndex = 0;
            this.errorRichTexBox.Text = "";
            // 
            // languageComboBox
            // 
            this.languageComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FormattingEnabled = true;
            this.languageComboBox.Location = new System.Drawing.Point(3, 3);
            this.languageComboBox.Name = "languageComboBox";
            this.languageComboBox.Size = new System.Drawing.Size(573, 21);
            this.languageComboBox.Sorted = true;
            this.languageComboBox.TabIndex = 4;
            // 
            // ProblemVerifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProblemVerifier";
            this.Size = new System.Drawing.Size(579, 454);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView testCasesListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.RichTextBox errorRichTexBox;
        private System.Windows.Forms.RichTextBox codeTextBox;
        private System.Windows.Forms.ComboBox languageComboBox;
    }
}
