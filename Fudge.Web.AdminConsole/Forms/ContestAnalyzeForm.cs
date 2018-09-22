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
    public partial class ContestAnalyzeForm : Form {
        public ContestAnalyzeForm() {
            InitializeComponent();
        }

        public void ShowDialog(FudgeDataContext dataContext, Contest contest) {
            contestAnalyzer.DataContext = dataContext;
            contestAnalyzer.Contest = contest;
            contestAnalyzer.Initialize();

            base.ShowDialog();
        }
    }
}
