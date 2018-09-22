using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Web.AdminConsole.Forms;
using Fudge.Web.AdminConsole.Controls;
using Fudge.Framework.Database;
using System.IO;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class PictureManager : ManagerControl {

        private class PictureListView : PagedListView<Picture> { }

        public Picture SelectedPicture { get { return pictureListView.SelectedItem; } }
        
        public PictureManager() {
            InitializeComponent();

            pictureListView.PageSize = 32;
            pictureListView.RetrieveItem += new PagedListView<Picture>.RetrieveItemEventHandler(pictureListView_RetrieveItem);
            pictureListView.SelectedItemChanged += new PagedListView<Picture>.SelectedItemChangedEventHandler(pictureListView_SelectedItemChanged);

            pictureListView.Selector = (ListViewItem item) => { return DataContext.Pictures.SingleOrDefault(p => p.PictureId == Int32.Parse(item.SubItems[0].Text)); };
            pictureListView.Sorter = (IQueryable<Picture> items, int parameter, SortDirection direction) => {
                switch (parameter) {
                    case 0: return (direction == SortDirection.Ascending ? items.OrderBy(p => p.PictureId) : items.OrderByDescending(p => p.PictureId));
                    case 1: return (direction == SortDirection.Ascending ? items.OrderBy(p => p.Title) : items.OrderByDescending(p => p.Title));                    
                }

                return items;
            };
        }

        public override void Initialize() {
            pictureListView.Update(DataContext.Pictures);

            filterControl.AddFilter("Search", FilterControl.FilterType.Text);
            filterControl.Filter += new EventHandler(filterControl_Filter);
        }

        void filterControl_Filter(object sender, EventArgs e) {
            var pictures = DataContext.Pictures.Where(p => p.Title.Contains(filterControl["Search"]));

            pictureListView.Update(pictures);
        }

        void pictureListView_RetrieveItem(object sender, PagedListView<Picture>.RetrieveItemEventArgs e) {
            e.ListViewItem = new ListViewItem(new[] { e.Item.PictureId.ToString(), e.Item.Title });
        }

        private void addButton_Click(object sender, EventArgs e) {
            PictureEditForm pictureForm = new PictureEditForm();
            Picture picture = new Picture();

            if (pictureForm.ShowDialog(picture) == DialogResult.OK) {
                DataContext.Pictures.InsertOnSubmit(picture);
                DataContext.SubmitChanges();

                pictureListView.Update(DataContext.Pictures, DataContext.Pictures.Count() - 1);
                pictureListView.Select();
            }
        }

        private void editButton_Click(object sender, EventArgs e) {
            PictureEditForm pictureForm = new PictureEditForm();

            if (pictureForm.ShowDialog(SelectedPicture) == DialogResult.OK) {
                DataContext.SubmitChanges();
                pictureListView.Update(DataContext.Pictures, pictureListView.SelectedIndex);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to delete the selected picture?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                DataContext.Pictures.DeleteOnSubmit(SelectedPicture);
                DataContext.SubmitChanges();
                
                pictureListView.Update(DataContext.Pictures, Math.Max(0, pictureListView.SelectedIndex - 1));
            }
        }


        void pictureListView_SelectedItemChanged(object sender, EventArgs e) {
            if (pictureListView.SelectedItem != null) {
                editButton.Enabled = true;
                deleteButton.Enabled = true;

                previewPictureBox.Image = System.Drawing.Image.FromStream(new MemoryStream(SelectedPicture.Data.ToArray()));
                previewPictureBox.Size = previewPictureBox.Image.Size;
            }
            else {
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        private void previousButton_Click(object sender, EventArgs e) {
            pictureListView.Previous();
        }

        private void nextButton_Click(object sender, EventArgs e) {
            pictureListView.Next();
        }
    }
}
