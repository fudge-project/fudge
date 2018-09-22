using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using Fudge.Web.AdminConsole.Forms;
using System.IO;
using Fudge.Web.AdminConsole.Controls;

namespace Fudge.Web.AdminConsole {
    public partial class SchoolEditForm : Form {

        private FudgeDataContext DataContext;
        private Entity School;      
        
        public SchoolEditForm(FudgeDataContext dataContext) {
            DataContext = dataContext;
            InitializeComponent();

            countryComboBox.Items.AddRange(DataContext.Countries.ToArray());
            countryComboBox.SelectedIndex = 0;            
        }

        public DialogResult ShowDialog(Entity school) {

            School = school;
            nameTextBox.Text = school.Name;
            domainTextBox.Text = school.Domain;

            if (school.Picture != null) {
                pictureBox.Image = System.Drawing.Image.FromStream(new MemoryStream(School.Picture.Data.ToArray()));
            }

            countryComboBox.SelectedItem = school.Country;
            typeComboBox.Value = school.Type;

            return ShowDialog();
        }

        private void okButton_Click(object sender, EventArgs e) {                                    
            School.Name = nameTextBox.Text;
            School.Domain = domainTextBox.Text;
            School.Country = (Country)countryComboBox.SelectedItem;
            School.Type = typeComboBox.Value;
        }

        private void pictureBox_Click(object sender, EventArgs e) {
            PictureForm pictureForm = new PictureForm(DataContext);

            if (pictureForm.ShowDialog() == DialogResult.OK) {
                School.Picture = pictureForm.SelectedPicture;
                pictureBox.Image = System.Drawing.Image.FromStream(new MemoryStream(School.Picture.Data.ToArray()));
            }
        }
    }
}
