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
using Fudge.Web.AdminConsole.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class CountryManager : ManagerControl {
        public CountryManager() {
            InitializeComponent();
        }

        public override FudgeDataContext DataContext { get; set; }
        public Country SelectedCountry { get; private set; }

        public override void Initialize() {          
            foreach (Country country in DataContext.Countries) {

                if (country.Picture != null) {
                    countryImageList.Images.Add(country.Name, new Bitmap(new MemoryStream(country.Picture.Data.ToArray())));
                }

                ListViewItem item = new ListViewItem(new [] { String.Empty, country.CountryId.ToString(), country.Name, country.Name });                
                item.ImageKey = country.Name;

                countryListView.Items.Add(item);
            }

            countryListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void editFlagToolStripMenuItem_Click(object sender, EventArgs e) {
            if (SelectedCountry != null) {
                PictureForm pictureForm = new PictureForm(DataContext);

                if (pictureForm.ShowDialog() == DialogResult.OK) {
                    SelectedCountry.Picture = pictureForm.SelectedPicture;
                    DataContext.SubmitChanges();

                    countryImageList.Images.RemoveByKey(SelectedCountry.Name);
                    countryImageList.Images.Add(SelectedCountry.Name, new Bitmap(new MemoryStream(SelectedCountry.Picture.Data.ToArray()))); 
                }
            }
        }

        private void countryListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (countryListView.SelectedItems.Count > 0) {
                SelectedCountry = DataContext.Countries.SingleOrDefault(c => c.CountryId == Int32.Parse(countryListView.SelectedItems[0].SubItems[1].Text));
            }
            else {
                SelectedCountry = null;
            }
        }
    }
}
