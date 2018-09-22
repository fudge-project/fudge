using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using System.Net.Mail;
using System.Data.Linq.SqlClient;
using System.Collections;
using System.Net;
using Fudge.Web.AdminConsole.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class SchoolManager : ManagerControl {

        private class SchoolListView : PagedListView<Entity> { }
        private class SuggestedSchoolListView : PagedListView<SuggestedEntity> { }

        public SchoolManager() {
            InitializeComponent();

            schoolListView.PageSize = 50;
            suggestedSchoolListView.PageSize = 256;

            schoolListView.RetrieveItem += new PagedListView<Entity>.RetrieveItemEventHandler(schoolListView_RetrieveItem);
            suggestedSchoolListView.RetrieveItem += new PagedListView<SuggestedEntity>.RetrieveItemEventHandler(suggestedSchoolListView_RetrieveItem);

            schoolListView.Selector = (ListViewItem item) => { return DataContext.Entities.SingleOrDefault(s => s.EntityId == Int32.Parse(item.SubItems[0].Text)); };
            suggestedSchoolListView.Selector = (ListViewItem item) => { return DataContext.SuggestedEntities.SingleOrDefault(s => s.SuggestedEntityId == Int32.Parse(item.SubItems[0].Text)); };

            schoolListView.SelectedItemChanged += new PagedListView<Entity>.SelectedItemChangedEventHandler(schoolListView_SelectedItemChanged);
            suggestedSchoolListView.SelectedItemChanged += new PagedListView<SuggestedEntity>.SelectedItemChangedEventHandler(suggestedSchoolListView_SelectedItemChanged);

            schoolListView.Sorter = (IQueryable<Entity> items, int parameter, SortDirection direction) => {
                switch (parameter) {
                    case 0: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.EntityId) : items.OrderByDescending(s => s.EntityId));
                    case 1: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.Name) : items.OrderByDescending(s => s.Name));
                    case 2: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.Domain) : items.OrderByDescending(s => s.Domain));
                    case 3: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.Country.Name) : items.OrderByDescending(s => s.Country.Name));
                    case 4: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.Type) : items.OrderByDescending(s => s.Type));
                }

                return items;
            };

            suggestedSchoolListView.Sorter = (IQueryable<SuggestedEntity> items, int parameter, SortDirection direction) => {
                switch (parameter) {
                    case 0: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.SuggestedEntityId) : items.OrderByDescending(s => s.SuggestedEntityId));
                    case 1: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.Name) : items.OrderByDescending(s => s.Name));
                    case 2: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.Domain) : items.OrderByDescending(s => s.Domain));
                    case 3: return (direction == SortDirection.Ascending ? items.OrderBy(s => s.NotifyEmail) : items.OrderByDescending(s => s.NotifyEmail));
                }

                return items;
            };
        }

        void schoolListView_SelectedItemChanged(object sender, EventArgs e) {
            if (schoolListView.SelectedItem != null) {
                editButton.Enabled = true;
                deleteButton.Enabled = true;
            }
            else {
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            }
        }

        void suggestedSchoolListView_SelectedItemChanged(object sender, EventArgs e) {
            if (suggestedSchoolListView.SelectedItem != null) {
                schoolBrowser.Navigate(suggestedSchoolListView.SelectedItem.Domain, false);
            }
        }

        void suggestedSchoolListView_RetrieveItem(object sender, PagedListView<SuggestedEntity>.RetrieveItemEventArgs e) {
            e.ListViewItem = new ListViewItem(new[] { e.Item.SuggestedEntityId.ToString(), e.Item.Name, e.Item.Domain, e.Item.NotifyEmail });
        }

        void schoolListView_RetrieveItem(object sender, PagedListView<Entity>.RetrieveItemEventArgs e) {
            e.ListViewItem = new ListViewItem(new[] { e.Item.EntityId.ToString(), e.Item.Name, e.Item.Domain, e.Item.Country.Name, e.Item.Type.ToString() });
        }

        private void UpdateSchools() {
            var schools = DataContext.Entities.Where(s => s.Name.Contains(filterControl["Name"]) &&
                                                    s.Domain.Contains(filterControl["Domain"]) &&
                                                    s.Country.Name.Contains(filterControl["Country"]));

            if (!String.IsNullOrEmpty(filterControl["Type"])) {
                schools = schools.Where(s => s.Type == (EntityType)Enum.Parse(typeof(EntityType), filterControl["Type"]));
            }

            schoolListView.Update(schools);
            suggestedSchoolListView.Update(DataContext.SuggestedEntities);
        }

        public override void Initialize() {
            suggestedSchoolListView.Update(DataContext.SuggestedEntities);
            schoolListView.Update(DataContext.Entities);

            filterControl.AddFilter("Type", new EnumComboBox<EntityType>(true));
            filterControl.AddFilter("Country", FilterControl.FilterType.ComboText, DataContext.Countries.Select(c => c.Name).ToArray());
            filterControl.AddFilter("Domain", FilterControl.FilterType.Text);
            filterControl.AddFilter("Name", FilterControl.FilterType.Text);
  
            filterControl.Filter += new EventHandler(filterControl_Filter);
        }

        void filterControl_Filter(object sender, EventArgs e) {
            UpdateSchools();
        }

        private void addButton_Click(object sender, EventArgs e) {

            SchoolEditForm schoolForm = new SchoolEditForm(DataContext);
            Entity school = new Entity();

            school.Country = DataContext.Countries.SingleOrDefault(c => c.Name == "United States");
            school.Type = EntityType.University;

            if (suggestedSchoolListView.SelectedItem != null) {
                school.Name = suggestedSchoolListView.SelectedItem.Name;
                school.Domain = suggestedSchoolListView.SelectedItem.Domain;
            }

            if (schoolForm.ShowDialog(school) == DialogResult.OK) {

                school.Topic = new Topic();
                school.Topic.ForumId = 7;
                school.Topic.UserId = 2;
                school.Topic.Timestamp = DateTime.UtcNow;
                school.Topic.Title = "Wall for " + school.Name;
                school.Topic.Status = 0;
                school.Topic.Visible = false;

                if (suggestedSchoolListView.SelectedItem != null) {
                    if (!String.IsNullOrEmpty(suggestedSchoolListView.SelectedItems[0].SubItems[3].Text.Trim())) {
                        MailMessage message = new MailMessage(new MailAddress("fudge@fit.edu", "Fudge"), new MailAddress(suggestedSchoolListView.SelectedItem.NotifyEmail));
                        message.Subject = "Your school has been added to Fudge!";
                        message.Body = school.Name + " has been added as a school on Fudge. Thank you for your suggestion. You can now register to Fudge by using any email address ending in " + school.Domain + "\n\nThanks,\nThe Fudge Team";

                        try {
                            new EmailSettingsForm().GetClient().Send(message);                      
                        }
                        catch (Exception ex) {
                            MessageBox.Show(ex.Message, "Smtp Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    DataContext.SuggestedEntities.DeleteOnSubmit(suggestedSchoolListView.SelectedItem);
                }

                DataContext.SubmitChanges();
                UpdateSchools();
            }
        }

        private void editButton_Click(object sender, EventArgs e) {
            SchoolEditForm schoolForm = new SchoolEditForm(DataContext);

            if (schoolForm.ShowDialog(schoolListView.SelectedItem) == DialogResult.OK) {
                DataContext.SubmitChanges();
                UpdateSchools();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            if (schoolListView.SelectedItem != null) {
                if (MessageBox.Show("Are you sure you want to delete " + schoolListView.SelectedItem.Name + "?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                    DataContext.Entities.DeleteOnSubmit(schoolListView.SelectedItem);
                    DataContext.SubmitChanges();

                    UpdateSchools();
                }
            }
        }

        private void refreshButton_Click(object sender, EventArgs e) {
            UpdateSchools();
        }

        private void previousButton_Click(object sender, EventArgs e) {
            schoolListView.Previous();
        }

        private void nextButton_Click(object sender, EventArgs e) {
            schoolListView.Next();
        }
    }
}
