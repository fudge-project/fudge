using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;

namespace Fudge.Web.AdminConsole.Forms {
    public partial class AffiliationsForm : Form {
        public AffiliationsForm(FudgeDataContext dataContext) {
            InitializeComponent();

            affiliationManager.DataContext = dataContext;            
        }

        public DialogResult ShowDialog(User user) {
            affiliationManager.Initialize(user);

            return ShowDialog();
        }
    }
}
