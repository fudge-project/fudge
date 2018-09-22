namespace Fudge.Web.AdminConsole.Controls {
    partial class ProblemParser {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.snippetComboBox = new System.Windows.Forms.ComboBox();
            this.languageTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.originalTextBox = new System.Windows.Forms.RichTextBox();
            this.inputUriTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.browseButton = new System.Windows.Forms.Button();
            this.fetchButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.resultsTextBox = new System.Windows.Forms.TextBox();
            this.resultsCountLabel = new System.Windows.Forms.Label();
            this.resultsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.parseButton = new System.Windows.Forms.Button();
            this.inputOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(581, 529);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ruby Parser";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.snippetComboBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.languageTextBox, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(569, 140);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // snippetComboBox
            // 
            this.snippetComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.snippetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.snippetComboBox.FormattingEnabled = true;
            this.snippetComboBox.Location = new System.Drawing.Point(3, 3);
            this.snippetComboBox.Name = "snippetComboBox";
            this.snippetComboBox.Size = new System.Drawing.Size(563, 21);
            this.snippetComboBox.TabIndex = 0;
            this.snippetComboBox.SelectedIndexChanged += new System.EventHandler(this.snippetComboBox_SelectedIndexChanged);
            // 
            // languageTextBox
            // 
            this.languageTextBox.AcceptsTab = true;
            this.languageTextBox.DetectUrls = false;
            this.languageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.languageTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.languageTextBox.Location = new System.Drawing.Point(3, 35);
            this.languageTextBox.Name = "languageTextBox";
            this.languageTextBox.Size = new System.Drawing.Size(563, 102);
            this.languageTextBox.TabIndex = 1;
            this.languageTextBox.Text = "";
            this.languageTextBox.WordWrap = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(575, 159);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Original";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 151F));
            this.tableLayoutPanel3.Controls.Add(this.originalTextBox, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.inputUriTextBox, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.flowLayoutPanel2, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(569, 140);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // originalTextBox
            // 
            this.originalTextBox.AcceptsTab = true;
            this.tableLayoutPanel3.SetColumnSpan(this.originalTextBox, 2);
            this.originalTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.originalTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.originalTextBox.Location = new System.Drawing.Point(3, 35);
            this.originalTextBox.Name = "originalTextBox";
            this.originalTextBox.Size = new System.Drawing.Size(563, 102);
            this.originalTextBox.TabIndex = 3;
            this.originalTextBox.Text = "";
            this.originalTextBox.WordWrap = false;
            // 
            // inputUriTextBox
            // 
            this.inputUriTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputUriTextBox.Location = new System.Drawing.Point(3, 3);
            this.inputUriTextBox.Name = "inputUriTextBox";
            this.inputUriTextBox.Size = new System.Drawing.Size(412, 20);
            this.inputUriTextBox.TabIndex = 4;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.browseButton);
            this.flowLayoutPanel2.Controls.Add(this.fetchButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(418, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(151, 32);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(3, 3);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 7;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // fetchButton
            // 
            this.fetchButton.Location = new System.Drawing.Point(84, 3);
            this.fetchButton.Name = "fetchButton";
            this.fetchButton.Size = new System.Drawing.Size(64, 23);
            this.fetchButton.TabIndex = 6;
            this.fetchButton.Text = "Fetch";
            this.fetchButton.UseVisualStyleBackColor = true;
            this.fetchButton.Click += new System.EventHandler(this.fetchButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 333);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(575, 159);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parsed Results";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel4.Controls.Add(this.resultsTextBox, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.resultsCountLabel, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.resultsListView, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(569, 140);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // resultsTextBox
            // 
            this.resultsTextBox.AcceptsTab = true;
            this.resultsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTextBox.Location = new System.Drawing.Point(401, 3);
            this.resultsTextBox.Multiline = true;
            this.resultsTextBox.Name = "resultsTextBox";
            this.tableLayoutPanel4.SetRowSpan(this.resultsTextBox, 2);
            this.resultsTextBox.Size = new System.Drawing.Size(165, 134);
            this.resultsTextBox.TabIndex = 1;
            this.resultsTextBox.TextChanged += new System.EventHandler(this.resultsTextBox_TextChanged);
            // 
            // resultsCountLabel
            // 
            this.resultsCountLabel.AutoSize = true;
            this.resultsCountLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsCountLabel.Location = new System.Drawing.Point(3, 120);
            this.resultsCountLabel.Name = "resultsCountLabel";
            this.resultsCountLabel.Size = new System.Drawing.Size(392, 20);
            this.resultsCountLabel.TabIndex = 2;
            this.resultsCountLabel.Text = "0 result blocks";
            // 
            // resultsListView
            // 
            this.resultsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.resultsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsListView.Location = new System.Drawing.Point(3, 3);
            this.resultsListView.MultiSelect = false;
            this.resultsListView.Name = "resultsListView";
            this.resultsListView.Size = new System.Drawing.Size(392, 114);
            this.resultsListView.TabIndex = 3;
            this.resultsListView.UseCompatibleStateImageBehavior = false;
            this.resultsListView.View = System.Windows.Forms.View.Details;
            this.resultsListView.VirtualMode = true;
            this.resultsListView.SelectedIndexChanged += new System.EventHandler(this.resultsListView_SelectedIndexChanged);
            this.resultsListView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.resultsListView_RetrieveVirtualItem);
            this.resultsListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resultsListView_KeyDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Test Case";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.parseButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 498);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(575, 28);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // parseButton
            // 
            this.parseButton.Location = new System.Drawing.Point(3, 3);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(75, 23);
            this.parseButton.TabIndex = 0;
            this.parseButton.Text = "Parse";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.parseButton_Click);
            // 
            // inputOpenFileDialog
            // 
            this.inputOpenFileDialog.Filter = "All files|*.*";
            this.inputOpenFileDialog.Title = "Open Input File";
            // 
            // ProblemParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProblemParser";
            this.Size = new System.Drawing.Size(581, 529);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox snippetComboBox;
        private System.Windows.Forms.RichTextBox languageTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RichTextBox originalTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox resultsTextBox;
        private System.Windows.Forms.TextBox inputUriTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button parseButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button fetchButton;
        private System.Windows.Forms.OpenFileDialog inputOpenFileDialog;
        private System.Windows.Forms.Label resultsCountLabel;
        private System.Windows.Forms.ListView resultsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
