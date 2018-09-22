namespace Fudge.Web.AdminConsole.Controls {
    partial class ProblemManager {
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
            this.testCasesListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertnewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertparsedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xmlSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.xmlOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.deleteButton = new System.Windows.Forms.Button();
            this.submitChangesButton = new System.Windows.Forms.Button();
            this.updateTestCasesCheckBox = new System.Windows.Forms.CheckBox();
            this.testButton = new System.Windows.Forms.Button();
            this.testCasesGroupBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.testCasesListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.testCaseOutputTextBox = new System.Windows.Forms.RichTextBox();
            this.testCaseInputTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.sourceComboBox = new System.Windows.Forms.ComboBox();
            this.manageSourcesButton = new System.Windows.Forms.Button();
            this.visibleCheckBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.memoryLimitTextBox = new NumericOnlyTextBox(8388608);
            this.label4 = new System.Windows.Forms.Label();
            this.timeLimitTextBox = new NumericOnlyTextBox(5000);
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.idRadioButton = new System.Windows.Forms.RadioButton();
            this.nameRadioButton = new System.Windows.Forms.RadioButton();
            this.byIdTextBox = new NumericOnlyTextBox();
            this.byNameComboBox = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.retrieveButton = new System.Windows.Forms.Button();
            this.newButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.uniqueKeysLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.previewXmlButton = new System.Windows.Forms.Button();
            this.saveXmlButton = new System.Windows.Forms.Button();
            this.loadXmlButton = new System.Windows.Forms.Button();
            this.statementTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.descriptionTextBox = new Fudge.Web.AdminConsole.Controls.ProblemFormatter();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.inputTextBox = new Fudge.Web.AdminConsole.Controls.ProblemFormatter();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.outputTextBox = new Fudge.Web.AdminConsole.Controls.ProblemFormatter();
            this.xmlTabPage = new System.Windows.Forms.TabPage();
            this.xmlTextBox = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.browserPreview = new System.Windows.Forms.WebBrowser();
            this.insertFromfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testCasesListViewContextMenuStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.testCasesGroupBox.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.statementTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.xmlTabPage.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // testCasesListViewContextMenuStrip
            // 
            this.testCasesListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertnewToolStripMenuItem,
            this.insertFromfilesToolStripMenuItem,
            this.insertparsedToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.testCasesListViewContextMenuStrip.Name = "contextMenuStrip1";
            this.testCasesListViewContextMenuStrip.Size = new System.Drawing.Size(166, 114);
            // 
            // insertnewToolStripMenuItem
            // 
            this.insertnewToolStripMenuItem.Name = "insertnewToolStripMenuItem";
            this.insertnewToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.insertnewToolStripMenuItem.Text = "Insert &new";
            this.insertnewToolStripMenuItem.Click += new System.EventHandler(this.insertnewToolStripMenuItem_Click);
            // 
            // insertparsedToolStripMenuItem
            // 
            this.insertparsedToolStripMenuItem.Name = "insertparsedToolStripMenuItem";
            this.insertparsedToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.insertparsedToolStripMenuItem.Text = "Insert &parsed...";
            this.insertparsedToolStripMenuItem.Click += new System.EventHandler(this.insertparsedToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // xmlSaveDialog
            // 
            this.xmlSaveDialog.DefaultExt = "xml";
            this.xmlSaveDialog.Filter = "XML files|*.xml|All files|*.*";
            this.xmlSaveDialog.Title = "Save Problem as Xml";
            // 
            // xmlOpenDialog
            // 
            this.xmlOpenDialog.Filter = "XML files|*.xml|All files|*.*";
            this.xmlOpenDialog.Title = "Open Xml Problem";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel9);
            this.splitContainer1.Panel1MinSize = 256;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel10);
            this.splitContainer1.Size = new System.Drawing.Size(814, 477);
            this.splitContainer1.SplitterDistance = 406;
            this.splitContainer1.TabIndex = 4;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel9.Controls.Add(this.testCasesGroupBox, 0, 1);
            this.tableLayoutPanel9.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 3;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 304F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(406, 477);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.deleteButton);
            this.flowLayoutPanel1.Controls.Add(this.submitChangesButton);
            this.flowLayoutPanel1.Controls.Add(this.updateTestCasesCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.testButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 448);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(400, 26);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // deleteButton
            // 
            this.deleteButton.Enabled = false;
            this.deleteButton.Location = new System.Drawing.Point(3, 3);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // submitChangesButton
            // 
            this.submitChangesButton.Location = new System.Drawing.Point(84, 3);
            this.submitChangesButton.Name = "submitChangesButton";
            this.submitChangesButton.Size = new System.Drawing.Size(75, 23);
            this.submitChangesButton.TabIndex = 0;
            this.submitChangesButton.Text = "Add";
            this.submitChangesButton.UseVisualStyleBackColor = true;
            this.submitChangesButton.Click += new System.EventHandler(this.submitChangesButton_Click);
            // 
            // updateTestCasesCheckBox
            // 
            this.updateTestCasesCheckBox.AutoSize = true;
            this.updateTestCasesCheckBox.Location = new System.Drawing.Point(165, 7);
            this.updateTestCasesCheckBox.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.updateTestCasesCheckBox.Name = "updateTestCasesCheckBox";
            this.updateTestCasesCheckBox.Size = new System.Drawing.Size(183, 17);
            this.updateTestCasesCheckBox.TabIndex = 2;
            this.updateTestCasesCheckBox.Text = "Update Test Cases (Rurun Runs)";
            this.updateTestCasesCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.updateTestCasesCheckBox.UseVisualStyleBackColor = true;
            this.updateTestCasesCheckBox.Visible = false;
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(3, 32);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(94, 23);
            this.testButton.TabIndex = 3;
            this.testButton.Text = "Test...";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // groupBox2
            // 
            this.testCasesGroupBox.Controls.Add(this.tableLayoutPanel5);
            this.testCasesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCasesGroupBox.Location = new System.Drawing.Point(3, 307);
            this.testCasesGroupBox.Name = "groupBox2";
            this.testCasesGroupBox.Size = new System.Drawing.Size(400, 135);
            this.testCasesGroupBox.TabIndex = 3;
            this.testCasesGroupBox.TabStop = false;
            this.testCasesGroupBox.Text = "Test Cases (0)";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.testCasesListView, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(394, 116);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // testCasesListView
            // 
            this.testCasesListView.CheckBoxes = true;
            this.testCasesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader1,
            this.columnHeader2});
            this.testCasesListView.ContextMenuStrip = this.testCasesListViewContextMenuStrip;
            this.testCasesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCasesListView.FullRowSelect = true;
            this.testCasesListView.GridLines = true;
            this.testCasesListView.Location = new System.Drawing.Point(3, 3);
            this.testCasesListView.Name = "testCasesListView";
            this.testCasesListView.ShowGroups = false;
            this.testCasesListView.Size = new System.Drawing.Size(388, 14);
            this.testCasesListView.TabIndex = 1;
            this.testCasesListView.UseCompatibleStateImageBehavior = false;
            this.testCasesListView.View = System.Windows.Forms.View.Details;
            this.testCasesListView.SelectedIndexChanged += new System.EventHandler(this.testCasesListView_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Sample";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Input";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Expected Output";
            this.columnHeader2.Width = 96;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.testCaseOutputTextBox, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.testCaseInputTextBox, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(388, 90);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // testCaseOutputTextBox
            // 
            this.testCaseOutputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCaseOutputTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testCaseOutputTextBox.Location = new System.Drawing.Point(197, 3);
            this.testCaseOutputTextBox.Name = "testCaseOutputTextBox";
            this.testCaseOutputTextBox.Size = new System.Drawing.Size(188, 84);
            this.testCaseOutputTextBox.TabIndex = 1;
            this.testCaseOutputTextBox.Text = "";
            this.testCaseOutputTextBox.TextChanged += new System.EventHandler(this.testCaseOutputTextBox_TextChanged);
            // 
            // testCaseInputTextBox
            // 
            this.testCaseInputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testCaseInputTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testCaseInputTextBox.Location = new System.Drawing.Point(3, 3);
            this.testCaseInputTextBox.Name = "testCaseInputTextBox";
            this.testCaseInputTextBox.Size = new System.Drawing.Size(188, 84);
            this.testCaseInputTextBox.TabIndex = 2;
            this.testCaseInputTextBox.Text = "";
            this.testCaseInputTextBox.TextChanged += new System.EventHandler(this.testCaseInputTextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 298);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Problem Information";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel4, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.visibleCheckBox, 0, 6);
            this.tableLayoutPanel4.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.memoryLimitTextBox, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.timeLimitTextBox, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.nameTextBox, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.uniqueKeysLabel, 1, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(394, 286);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.sourceComboBox);
            this.flowLayoutPanel4.Controls.Add(this.manageSourcesButton);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(84, 160);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(310, 32);
            this.flowLayoutPanel4.TabIndex = 15;
            // 
            // sourceComboBox
            // 
            this.sourceComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.sourceComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.sourceComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.sourceComboBox.FormattingEnabled = true;
            this.sourceComboBox.Location = new System.Drawing.Point(3, 3);
            this.sourceComboBox.MaxLength = 64;
            this.sourceComboBox.Name = "sourceComboBox";
            this.sourceComboBox.Size = new System.Drawing.Size(262, 21);
            this.sourceComboBox.Sorted = true;
            this.sourceComboBox.TabIndex = 8;
            // 
            // manageSourcesButton
            // 
            this.manageSourcesButton.Location = new System.Drawing.Point(3, 30);
            this.manageSourcesButton.Name = "manageSourcesButton";
            this.manageSourcesButton.Size = new System.Drawing.Size(75, 23);
            this.manageSourcesButton.TabIndex = 9;
            this.manageSourcesButton.Text = "Manage...";
            this.manageSourcesButton.UseVisualStyleBackColor = true;
            this.manageSourcesButton.Click += new System.EventHandler(this.manageSourcesButton_Click);
            // 
            // visibleCheckBox
            // 
            this.visibleCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.visibleCheckBox.AutoSize = true;
            this.tableLayoutPanel4.SetColumnSpan(this.visibleCheckBox, 2);
            this.visibleCheckBox.Location = new System.Drawing.Point(3, 262);
            this.visibleCheckBox.Name = "visibleCheckBox";
            this.visibleCheckBox.Size = new System.Drawing.Size(56, 17);
            this.visibleCheckBox.TabIndex = 11;
            this.visibleCheckBox.Text = "Visible";
            this.visibleCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Memory Limit";
            // 
            // memoryLimitTextBox
            // 
            this.memoryLimitTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.memoryLimitTextBox.Location = new System.Drawing.Point(87, 230);
            this.memoryLimitTextBox.Name = "memoryLimitTextBox";
            this.memoryLimitTextBox.Size = new System.Drawing.Size(100, 20);
            this.memoryLimitTextBox.TabIndex = 9;
            this.memoryLimitTextBox.Text = "0";            
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Time Limit";
            // 
            // timeLimitTextBox
            // 
            this.timeLimitTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timeLimitTextBox.Location = new System.Drawing.Point(87, 198);
            this.timeLimitTextBox.Name = "timeLimitTextBox";
            this.timeLimitTextBox.Size = new System.Drawing.Size(100, 20);
            this.timeLimitTextBox.TabIndex = 8;
            this.timeLimitTextBox.Text = "0";            
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Source";
            // 
            // groupBox3
            // 
            this.tableLayoutPanel4.SetColumnSpan(this.groupBox3, 2);
            this.groupBox3.Controls.Add(this.tableLayoutPanel8);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(1);
            this.groupBox3.Size = new System.Drawing.Size(388, 90);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Find";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.idRadioButton, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.nameRadioButton, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.byIdTextBox, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.byNameComboBox, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.flowLayoutPanel2, 1, 2);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(1, 14);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(386, 75);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // idRadioButton
            // 
            this.idRadioButton.AutoSize = true;
            this.idRadioButton.Location = new System.Drawing.Point(3, 3);
            this.idRadioButton.Name = "idRadioButton";
            this.idRadioButton.Size = new System.Drawing.Size(50, 17);
            this.idRadioButton.TabIndex = 0;
            this.idRadioButton.Text = "by ID";
            this.idRadioButton.UseVisualStyleBackColor = true;
            this.idRadioButton.CheckedChanged += new System.EventHandler(this.byRadioButtons_CheckedChanged);
            // 
            // nameRadioButton
            // 
            this.nameRadioButton.AutoSize = true;
            this.nameRadioButton.Checked = true;
            this.nameRadioButton.Location = new System.Drawing.Point(3, 27);
            this.nameRadioButton.Name = "nameRadioButton";
            this.nameRadioButton.Size = new System.Drawing.Size(67, 17);
            this.nameRadioButton.TabIndex = 1;
            this.nameRadioButton.TabStop = true;
            this.nameRadioButton.Text = "by Name";
            this.nameRadioButton.UseVisualStyleBackColor = true;
            this.nameRadioButton.CheckedChanged += new System.EventHandler(this.byRadioButtons_CheckedChanged);
            // 
            // byIdTextBox
            // 
            this.byIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.byIdTextBox.Enabled = false;
            this.byIdTextBox.Location = new System.Drawing.Point(99, 3);
            this.byIdTextBox.Name = "byIdTextBox";
            this.byIdTextBox.Size = new System.Drawing.Size(284, 20);
            this.byIdTextBox.TabIndex = 0;
            this.byIdTextBox.Text = "0";
            // 
            // byNameComboBox
            // 
            this.byNameComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.byNameComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.byNameComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.byNameComboBox.FormattingEnabled = true;
            this.byNameComboBox.Location = new System.Drawing.Point(99, 27);
            this.byNameComboBox.Name = "byNameComboBox";
            this.byNameComboBox.Size = new System.Drawing.Size(284, 21);
            this.byNameComboBox.Sorted = true;
            this.byNameComboBox.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.retrieveButton);
            this.flowLayoutPanel2.Controls.Add(this.newButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(96, 48);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(290, 27);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // retrieveButton
            // 
            this.retrieveButton.Location = new System.Drawing.Point(212, 3);
            this.retrieveButton.Name = "retrieveButton";
            this.retrieveButton.Size = new System.Drawing.Size(75, 21);
            this.retrieveButton.TabIndex = 2;
            this.retrieveButton.Text = "Retrieve";
            this.retrieveButton.UseVisualStyleBackColor = true;
            this.retrieveButton.Click += new System.EventHandler(this.retrieveButton_Click);
            // 
            // newButton
            // 
            this.newButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.newButton.Location = new System.Drawing.Point(148, 3);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(58, 21);
            this.newButton.TabIndex = 2;
            this.newButton.Text = "New";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Unique Keys";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nameTextBox.Location = new System.Drawing.Point(87, 134);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(202, 20);
            this.nameTextBox.TabIndex = 6;
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // uniqueKeysLabel
            // 
            this.uniqueKeysLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.uniqueKeysLabel.AutoSize = true;
            this.uniqueKeysLabel.Location = new System.Drawing.Point(87, 105);
            this.uniqueKeysLabel.Name = "uniqueKeysLabel";
            this.uniqueKeysLabel.Size = new System.Drawing.Size(190, 13);
            this.uniqueKeysLabel.TabIndex = 17;
            this.uniqueKeysLabel.Text = "Id: [not assigned], Short Name: [empty]";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.flowLayoutPanel3, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.statementTabControl, 0, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(404, 477);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.previewXmlButton);
            this.flowLayoutPanel3.Controls.Add(this.saveXmlButton);
            this.flowLayoutPanel3.Controls.Add(this.loadXmlButton);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 448);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(398, 26);
            this.flowLayoutPanel3.TabIndex = 3;
            // 
            // previewXmlButton
            // 
            this.previewXmlButton.Location = new System.Drawing.Point(3, 3);
            this.previewXmlButton.Name = "previewXmlButton";
            this.previewXmlButton.Size = new System.Drawing.Size(113, 23);
            this.previewXmlButton.TabIndex = 2;
            this.previewXmlButton.Text = "Preview HTML";
            this.previewXmlButton.UseVisualStyleBackColor = true;
            this.previewXmlButton.Click += new System.EventHandler(this.previewXmlButton_Click);
            // 
            // saveXmlButton
            // 
            this.saveXmlButton.Location = new System.Drawing.Point(122, 3);
            this.saveXmlButton.Name = "saveXmlButton";
            this.saveXmlButton.Size = new System.Drawing.Size(113, 23);
            this.saveXmlButton.TabIndex = 0;
            this.saveXmlButton.Text = "Save to XML...";
            this.saveXmlButton.UseVisualStyleBackColor = true;
            this.saveXmlButton.Click += new System.EventHandler(this.saveXmlButton_Click);
            // 
            // loadXmlButton
            // 
            this.loadXmlButton.Location = new System.Drawing.Point(241, 3);
            this.loadXmlButton.Name = "loadXmlButton";
            this.loadXmlButton.Size = new System.Drawing.Size(113, 23);
            this.loadXmlButton.TabIndex = 1;
            this.loadXmlButton.Text = "Load from XML...";
            this.loadXmlButton.UseVisualStyleBackColor = true;
            this.loadXmlButton.Click += new System.EventHandler(this.loadXmlButton_Click);
            // 
            // statementTabControl
            // 
            this.statementTabControl.Controls.Add(this.tabPage1);
            this.statementTabControl.Controls.Add(this.tabPage2);
            this.statementTabControl.Controls.Add(this.tabPage3);
            this.statementTabControl.Controls.Add(this.xmlTabPage);
            this.statementTabControl.Controls.Add(this.tabPage4);
            this.statementTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statementTabControl.Location = new System.Drawing.Point(3, 3);
            this.statementTabControl.Name = "statementTabControl";
            this.statementTabControl.SelectedIndex = 0;
            this.statementTabControl.Size = new System.Drawing.Size(398, 439);
            this.statementTabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.descriptionTextBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(390, 413);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Description";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.DataContext = null;
            this.descriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.descriptionTextBox.Location = new System.Drawing.Point(3, 3);
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(384, 407);
            this.descriptionTextBox.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.inputTextBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(390, 413);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Input";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // inputTextBox
            // 
            this.inputTextBox.DataContext = null;
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTextBox.Location = new System.Drawing.Point(3, 3);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(384, 407);
            this.inputTextBox.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.outputTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(390, 413);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Output";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // outputTextBox
            // 
            this.outputTextBox.DataContext = null;
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Location = new System.Drawing.Point(3, 3);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(384, 407);
            this.outputTextBox.TabIndex = 0;
            // 
            // xmlTabPage
            // 
            this.xmlTabPage.Controls.Add(this.xmlTextBox);
            this.xmlTabPage.Location = new System.Drawing.Point(4, 22);
            this.xmlTabPage.Name = "xmlTabPage";
            this.xmlTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.xmlTabPage.Size = new System.Drawing.Size(390, 413);
            this.xmlTabPage.TabIndex = 3;
            this.xmlTabPage.Text = "XML";
            this.xmlTabPage.UseVisualStyleBackColor = true;
            // 
            // xmlTextBox
            // 
            this.xmlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmlTextBox.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmlTextBox.Location = new System.Drawing.Point(3, 3);
            this.xmlTextBox.Name = "xmlTextBox";
            this.xmlTextBox.ReadOnly = true;
            this.xmlTextBox.Size = new System.Drawing.Size(384, 407);
            this.xmlTextBox.TabIndex = 0;
            this.xmlTextBox.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.browserPreview);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(390, 413);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Preview";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // browserPreview
            // 
            this.browserPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserPreview.Location = new System.Drawing.Point(3, 3);
            this.browserPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserPreview.Name = "browserPreview";
            this.browserPreview.Size = new System.Drawing.Size(384, 407);
            this.browserPreview.TabIndex = 0;
            // 
            // insertFromfilesToolStripMenuItem
            // 
            this.insertFromfilesToolStripMenuItem.Name = "insertFromfilesToolStripMenuItem";
            this.insertFromfilesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.insertFromfilesToolStripMenuItem.Text = "Insert from &files...";
            this.insertFromfilesToolStripMenuItem.Click += new System.EventHandler(this.insertFromfilesToolStripMenuItem_Click);
            // 
            // ProblemManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ProblemManager";
            this.Size = new System.Drawing.Size(814, 477);
            this.testCasesListViewContextMenuStrip.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.testCasesGroupBox.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.statementTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.xmlTabPage.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip testCasesListViewContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem insertnewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertparsedToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog xmlSaveDialog;
        private System.Windows.Forms.OpenFileDialog xmlOpenDialog;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.GroupBox testCasesGroupBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ListView testCasesListView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.RichTextBox testCaseOutputTextBox;
        private System.Windows.Forms.RichTextBox testCaseInputTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.ComboBox sourceComboBox;
        private System.Windows.Forms.Button manageSourcesButton;
        private System.Windows.Forms.CheckBox visibleCheckBox;
        private System.Windows.Forms.Label label5;
        private NumericOnlyTextBox memoryLimitTextBox;
        private System.Windows.Forms.Label label4;
        private NumericOnlyTextBox timeLimitTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.RadioButton idRadioButton;
        private System.Windows.Forms.RadioButton nameRadioButton;
        private NumericOnlyTextBox byIdTextBox;
        private System.Windows.Forms.ComboBox byNameComboBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button retrieveButton;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label uniqueKeysLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button previewXmlButton;
        private System.Windows.Forms.Button saveXmlButton;
        private System.Windows.Forms.Button loadXmlButton;
        private System.Windows.Forms.TabControl statementTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private ProblemFormatter descriptionTextBox;
        private System.Windows.Forms.TabPage tabPage2;
        private ProblemFormatter inputTextBox;
        private System.Windows.Forms.TabPage tabPage3;
        private ProblemFormatter outputTextBox;
        private System.Windows.Forms.TabPage xmlTabPage;
        private System.Windows.Forms.RichTextBox xmlTextBox;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.WebBrowser browserPreview;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button submitChangesButton;
        private System.Windows.Forms.CheckBox updateTestCasesCheckBox;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.ToolStripMenuItem insertFromfilesToolStripMenuItem;
    }
}
