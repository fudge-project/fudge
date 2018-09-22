using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;

namespace Fudge.Web.AdminConsole.Forms {
    public partial class PictureForm : Form {

        public Picture SelectedPicture { get; private set; }

        public PictureForm(FudgeDataContext dataContext) {
            InitializeComponent();
            
            pictureManager.DataContext = dataContext;
            pictureManager.Initialize();
        }

        private void okButton_Click(object sender, EventArgs e) {
            SelectedPicture = pictureManager.SelectedPicture;

            if (SelectedPicture == null) {
                MessageBox.Show("Please select an image, or cancel", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
