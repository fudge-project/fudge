namespace Fudge.Web.AdminConsole.Forms {
    partial class ContestAnalyzeForm {
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
            this.contestAnalyzer = new Fudge.Web.AdminConsole.Controls.ContestAnalyzer();
            this.SuspendLayout();
            // 
            // contestAnalyzer
            // 
            this.contestAnalyzer.Contest = null;            
            this.contestAnalyzer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contestAnalyzer.Location = new System.Drawing.Point(0, 0);
            this.contestAnalyzer.Name = "contestAnalyzer";
            this.contestAnalyzer.Size = new System.Drawing.Size(570, 689);
            this.contestAnalyzer.TabIndex = 0;
            // 
            // ContestAnalyzeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 689);
            this.Controls.Add(this.contestAnalyzer);
            this.Name = "ContestAnalyzeForm";
            this.Text = "ContestAnalyzeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Fudge.Web.AdminConsole.Controls.ContestAnalyzer contestAnalyzer;
    }
}