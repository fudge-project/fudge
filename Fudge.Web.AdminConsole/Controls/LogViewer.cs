using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using System.IO;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class LogViewer : UserControl {        
        public RichTextBoxWriter TextBoxWriter { get; private set; }

        public LogViewer() {
            
            InitializeComponent();

            TextBoxWriter = new RichTextBoxWriter(logTextBox);
        }
    }
}
