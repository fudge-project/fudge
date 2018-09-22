namespace Fudge.Web.AdminConsole.Controls {
    partial class ProblemFormatter {
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ftmPreButton = new System.Windows.Forms.Button();
            this.fmtIButton = new System.Windows.Forms.Button();
            this.ftmSupButton = new System.Windows.Forms.Button();
            this.fmtSubButton = new System.Windows.Forms.Button();
            this.symbolsButton = new System.Windows.Forms.Button();
            this.imageButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.symbolContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.symbolLessEqualTo = new System.Windows.Forms.ToolStripMenuItem();
            this.symbolGreaterEqualTo = new System.Windows.Forms.ToolStripMenuItem();
            this.symbolNotEqualTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.symbolContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(431, 337);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ftmPreButton);
            this.flowLayoutPanel1.Controls.Add(this.fmtIButton);
            this.flowLayoutPanel1.Controls.Add(this.ftmSupButton);
            this.flowLayoutPanel1.Controls.Add(this.fmtSubButton);
            this.flowLayoutPanel1.Controls.Add(this.symbolsButton);
            this.flowLayoutPanel1.Controls.Add(this.imageButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 308);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(425, 26);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ftmPreButton
            // 
            this.ftmPreButton.Location = new System.Drawing.Point(3, 3);
            this.ftmPreButton.Name = "ftmPreButton";
            this.ftmPreButton.Size = new System.Drawing.Size(44, 23);
            this.ftmPreButton.TabIndex = 1;
            this.ftmPreButton.Text = "<tt>";
            this.ftmPreButton.UseVisualStyleBackColor = true;
            this.ftmPreButton.Click += new System.EventHandler(this.ftmPreButton_Click);
            // 
            // fmtIButton
            // 
            this.fmtIButton.Location = new System.Drawing.Point(53, 3);
            this.fmtIButton.Name = "fmtIButton";
            this.fmtIButton.Size = new System.Drawing.Size(30, 23);
            this.fmtIButton.TabIndex = 2;
            this.fmtIButton.Text = "<i>";
            this.fmtIButton.UseVisualStyleBackColor = true;
            this.fmtIButton.Click += new System.EventHandler(this.fmtIButton_Click);
            // 
            // ftmSupButton
            // 
            this.ftmSupButton.Location = new System.Drawing.Point(89, 3);
            this.ftmSupButton.Name = "ftmSupButton";
            this.ftmSupButton.Size = new System.Drawing.Size(45, 23);
            this.ftmSupButton.TabIndex = 4;
            this.ftmSupButton.Text = "<sup>";
            this.ftmSupButton.UseVisualStyleBackColor = true;
            this.ftmSupButton.Click += new System.EventHandler(this.ftmSupButton_Click);
            // 
            // fmtSubButton
            // 
            this.fmtSubButton.Location = new System.Drawing.Point(140, 3);
            this.fmtSubButton.Name = "fmtSubButton";
            this.fmtSubButton.Size = new System.Drawing.Size(46, 23);
            this.fmtSubButton.TabIndex = 3;
            this.fmtSubButton.Text = "<sub>";
            this.fmtSubButton.UseVisualStyleBackColor = true;
            this.fmtSubButton.Click += new System.EventHandler(this.fmtSubButton_Click);
            // 
            // symbolsButton
            // 
            this.symbolsButton.Location = new System.Drawing.Point(192, 3);
            this.symbolsButton.Name = "symbolsButton";
            this.symbolsButton.Size = new System.Drawing.Size(75, 23);
            this.symbolsButton.TabIndex = 5;
            this.symbolsButton.Text = "Symbols...";
            this.symbolsButton.UseVisualStyleBackColor = true;
            this.symbolsButton.Click += new System.EventHandler(this.symbolsButton_Click);
            // 
            // imageButton
            // 
            this.imageButton.Location = new System.Drawing.Point(273, 3);
            this.imageButton.Name = "imageButton";
            this.imageButton.Size = new System.Drawing.Size(75, 23);
            this.imageButton.TabIndex = 6;
            this.imageButton.Text = "Images...";
            this.imageButton.UseVisualStyleBackColor = true;
            this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("Consolas", 11F);
            this.textBox.Location = new System.Drawing.Point(3, 3);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(425, 299);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "";
            // 
            // symbolContextMenuStrip
            // 
            this.symbolContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.symbolLessEqualTo,
            this.symbolGreaterEqualTo,
            this.symbolNotEqualTo});
            this.symbolContextMenuStrip.Name = "symbolContextMenuStrip";
            this.symbolContextMenuStrip.ShowImageMargin = false;
            this.symbolContextMenuStrip.Size = new System.Drawing.Size(58, 70);
            // 
            // symbolLessEqualTo
            // 
            this.symbolLessEqualTo.Name = "symbolLessEqualTo";
            this.symbolLessEqualTo.Size = new System.Drawing.Size(57, 22);
            this.symbolLessEqualTo.Tag = "&#x2264;";
            this.symbolLessEqualTo.Text = "≤";
            this.symbolLessEqualTo.Click += new System.EventHandler(this.symbol_Click);
            // 
            // symbolGreaterEqualTo
            // 
            this.symbolGreaterEqualTo.Name = "symbolGreaterEqualTo";
            this.symbolGreaterEqualTo.Size = new System.Drawing.Size(57, 22);
            this.symbolGreaterEqualTo.Tag = "&#x2265;";
            this.symbolGreaterEqualTo.Text = "≥";
            this.symbolGreaterEqualTo.Click += new System.EventHandler(this.symbol_Click);
            // 
            // symbolNotEqualTo
            // 
            this.symbolNotEqualTo.Name = "symbolNotEqualTo";
            this.symbolNotEqualTo.Size = new System.Drawing.Size(57, 22);
            this.symbolNotEqualTo.Tag = "&#x2260;";
            this.symbolNotEqualTo.Text = "≠";
            this.symbolNotEqualTo.Click += new System.EventHandler(this.symbol_Click);
            // 
            // ProblemFormatter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProblemFormatter";
            this.Size = new System.Drawing.Size(431, 337);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.symbolContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button ftmPreButton;
        private System.Windows.Forms.Button fmtIButton;
        private System.Windows.Forms.Button ftmSupButton;
        private System.Windows.Forms.Button fmtSubButton;
        private System.Windows.Forms.Button symbolsButton;
        private System.Windows.Forms.ContextMenuStrip symbolContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem symbolLessEqualTo;
        private System.Windows.Forms.ToolStripMenuItem symbolGreaterEqualTo;
        private System.Windows.Forms.ToolStripMenuItem symbolNotEqualTo;
        private System.Windows.Forms.Button imageButton;
        private System.Windows.Forms.RichTextBox textBox;
    }
}
