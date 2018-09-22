using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Web.AdminConsole {
    public class RichTextBoxWriter : System.IO.TextWriter {

        public System.Windows.Forms.RichTextBox RichTextBox { get; private set; }

        private Encoding encoding;
        public override Encoding Encoding {
            get {
                if (encoding == null) {
                    encoding = new UnicodeEncoding(false, false);
                }

                return encoding;
            }
        }

        public RichTextBoxWriter(System.Windows.Forms.RichTextBox richTextBox) {
            RichTextBox = richTextBox;
        }

        public override void Write(string value) {
            RichTextBox.AppendText(value);
        }

        public override void Write(char[] buffer) {
            Write(new String(buffer));
        }

        public override void Write(char[] buffer, int index, int count) {
            Write(new String(buffer, index, count));
        }
    }
}
