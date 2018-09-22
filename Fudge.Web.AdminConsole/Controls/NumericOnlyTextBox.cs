using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public class NumericOnlyTextBox : TextBox {

        public int DefaultValue { get; set; }
        public int Value { get { return Int32.Parse(Text); } set { Text = value.ToString(); } }

        public NumericOnlyTextBox()
            : base() {

            TextChanged += new EventHandler(NumericOnlyTextBox_TextChanged);
        }

        public NumericOnlyTextBox(int defaultValue)
            : this() {

            DefaultValue = defaultValue;
        }

        void NumericOnlyTextBox_TextChanged(object sender, EventArgs e) {
            if (String.IsNullOrEmpty(Text.Trim())) {
                Text = DefaultValue.ToString();
            }
            else {
                int caretPosition = SelectionStart - 1;
                string validText = Text;

                foreach (char c in validText) {
                    if (!Char.IsDigit(c)) {
                        validText = validText.Replace(c.ToString(), String.Empty);
                    }
                }

                if (validText != Text) {
                    Text = validText;
                    SelectionStart = Math.Min(caretPosition, Text.Length);

                    ToolTip errorTip = new ToolTip();
                    errorTip.IsBalloon = true;
                    errorTip.ToolTipIcon = ToolTipIcon.Error;
                    errorTip.ToolTipTitle = "Invalid entry!";
                    errorTip.ShowAlways = true;
                    errorTip.Show("Only numbers are allowed", this, -16, -76, 750);
                }
            }
        }
    }
}
