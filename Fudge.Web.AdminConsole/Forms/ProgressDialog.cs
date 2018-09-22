using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Fudge.Web.AdminConsole.Forms {
    public partial class ProgressDialog : Form {

        public bool Cancel { get; private set; }

        public ProgressDialog(bool showCancelButton, bool isMarquee)
            : this(showCancelButton) {
            if (isMarquee) {
                progressBar.Style = ProgressBarStyle.Marquee;
            }
        }

        public ProgressDialog(bool showCancelButton)
            : this() {
            if (!showCancelButton) {
                Height -= 8;
                cancelButton.Visible = false;
            }
        }

        public ProgressDialog() {
            InitializeComponent();
            Cancel = false;
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            cancelButton.Enabled = false;
            Cancel = true;
        }
    }
}
