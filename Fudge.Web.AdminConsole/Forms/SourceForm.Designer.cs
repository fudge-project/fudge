namespace Fudge.Web.AdminConsole {
    partial class SourceForm {
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
            this.sourceManager = new Fudge.Web.AdminConsole.Controls.SourceManager();
            this.SuspendLayout();
            // 
            // sourceManager1
            // 
            this.sourceManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceManager.Location = new System.Drawing.Point(0, 0);
            this.sourceManager.Name = "sourceManager1";
            this.sourceManager.Size = new System.Drawing.Size(397, 237);
            this.sourceManager.TabIndex = 0;
            // 
            // SourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 237);
            this.Controls.Add(this.sourceManager);
            this.Name = "SourceForm";
            this.Text = "Manage Sources";
            this.ResumeLayout(false);

        }

        #endregion

        private Fudge.Web.AdminConsole.Controls.SourceManager sourceManager;
    }
}