using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class SiteManager : ManagerControl {
        public SiteManager() {
            InitializeComponent();
        }

        public override FudgeDataContext DataContext { get; set; }

        public override void Initialize() {
            countryManager.DataContext = DataContext;
            countryManager.Initialize();
        }
    }
}
