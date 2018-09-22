using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Framework.Setup.Sheets {
    public partial class UsersSheet : Sheet {
        public UsersSheet() {
            InitializeComponent();
        }

        public override bool ValidateData() {
            if (compilationPassword.Text != compilationConfirmPassword.Text) {
                MessageBox.Show("Your compilation passwords don't match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (executionPassword.Text != executionConfirmPassword.Text) {
                MessageBox.Show("Your execution passwords don't match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public override void Execute() {
            DirectoryEntry entry = new DirectoryEntry("WinNT://" + Environment.MachineName + ", computer");

            DirectoryEntry compileUser = entry.Children.Add(compilationUser.Text, "user");
            compileUser.Invoke("SetPassword", new object[] { compilationPassword.Text });
            compileUser.CommitChanges();

            DirectoryEntry executeUser = entry.Children.Add(executionUser.Text, "user");
            executeUser.Invoke("SetPassword", new object[] { executionPassword.Text });
            executeUser.CommitChanges();
        }
    }
}
