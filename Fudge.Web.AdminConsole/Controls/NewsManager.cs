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
using System.Text.RegularExpressions;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class NewsManager : ManagerControl {

        public News SelectedNews { get; private set; }

        public NewsManager() {
            InitializeComponent();
        }

        private void UpdateNews() {
            newsListView.Items.Clear();

            foreach (News news in DataContext.News) {
                newsListView.Items.Add(new ListViewItem(new[] { news.NewsId.ToString(), news.Title, news.UrlName, news.Type.ToString(), news.Timestamp.ToString() }));
            }
            
            newsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public override void Initialize() {
            UpdateNews();
            newsListView.SelectedIndices.Add(0);            
        }

        private void newsListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (newsListView.SelectedItems.Count > 0) {
                SelectedNews = DataContext.News.Single(n => n.NewsId == Int32.Parse(newsListView.SelectedItems[0].SubItems[0].Text));

                updateButton.Enabled = true;
                updateButton.Text = "Update";

                deleteButton.Enabled = true;
            }
            else {
                SelectedNews = null;

                updateButton.Enabled = false;
                deleteButton.Enabled = false;
            }

            UpdateSelectedNews();
        }

        private void UpdateSelectedNews() {
            if (SelectedNews == null) {
                titleTextBox.Text = String.Empty;
                contentRichTextBox.Text = String.Empty;
                pictureBox.Image = null;
            }
            else {
                titleTextBox.Text = SelectedNews.Title;
                contentRichTextBox.Text = SelectedNews.Text;

                if (SelectedNews.Picture != null) {
                    pictureBox.Image = new Bitmap(new MemoryStream(SelectedNews.Picture.Data.ToArray()));
                }
            }
        }

        private void newButton_Click(object sender, EventArgs e) {
            newsListView.SelectedItems.Clear();

            updateButton.Enabled = true;
            updateButton.Text = "Add";

            SelectedNews = new News();
            SelectedNews.Rating = new Rating();
        }

        private string GetShortName(string name) {
            Regex invalidRegex = new Regex(@"[^a-zA-Z0-9\s]");
            return invalidRegex.Replace(name, String.Empty).Replace(' ', '_');
        }

        private void updateButton_Click(object sender, EventArgs e) {
            SelectedNews.Title = titleTextBox.Text;
            SelectedNews.Text = contentRichTextBox.Text;

            if (SelectedNews.NewsId == 0) {
                SelectedNews.Timestamp = DateTime.UtcNow;
                SelectedNews.UrlName = GetShortName(SelectedNews.Title);
                SelectedNews.NewsLink = @"http://fudge.fit.edu/News/" + SelectedNews.UrlName;
                SelectedNews.Type = 0;

                DataContext.News.InsertOnSubmit(SelectedNews);
            }           

            DataContext.SubmitChanges();
            UpdateNews();
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e) {
            if (SelectedNews != null) {
                PictureForm pictureForm = new PictureForm(DataContext);

                if (pictureForm.ShowDialog() == DialogResult.OK) {
                    SelectedNews.Picture = pictureForm.SelectedPicture;
                    pictureBox.Image = new Bitmap(new MemoryStream(SelectedNews.Picture.Data.ToArray()));
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Are you sure you want to delete \"" + SelectedNews.Title + "\"?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                DataContext.News.DeleteOnSubmit(SelectedNews);
                DataContext.SubmitChanges();

                UpdateNews();
            }
        }
    }
}
