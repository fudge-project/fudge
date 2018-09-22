using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fudge.Framework.Database;
using System.Data.Linq.SqlClient;
using Fudge.Web.AdminConsole.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class UserManager : ManagerControl {

        private class UserListView : PagedListView<User> { }

        public UserManager() {
            InitializeComponent();

            userListView.RetrieveItem += new UserListView.RetrieveItemEventHandler(userListView_RetrieveItem);

            userListView.Selector = (ListViewItem item) => { return DataContext.Users.SingleOrDefault(u => u.UserId == Int32.Parse(item.SubItems[0].Text)); };
            userListView.Sorter = (IQueryable<User> items, int parameter, SortDirection direction) => {
                switch (parameter) {
                    case 0: return (direction == SortDirection.Ascending ? items.OrderBy(u => u.UserId) : items.OrderByDescending(u => u.UserId));
                    case 1: return (direction == SortDirection.Ascending ? items.OrderBy(u => u.Email) : items.OrderByDescending(u => u.Email));
                    case 2: return (direction == SortDirection.Ascending ? items.OrderBy(u => u.FirstName) : items.OrderByDescending(u => u.FirstName));
                    case 3: return (direction == SortDirection.Ascending ? items.OrderBy(u => u.LastName) : items.OrderByDescending(u => u.LastName));
                    case 4: return (direction == SortDirection.Ascending ? items.OrderBy(u => u.ShortName) : items.OrderByDescending(u => u.ShortName));
                    case 5: return (direction == SortDirection.Ascending ? items.OrderBy(u => u.Affiliation.Entity.Name) : items.OrderByDescending(u => u.Affiliation.Entity.Name));
                    case 6: return (direction == SortDirection.Ascending ? items.OrderBy(u => u.Country.Name) : items.OrderByDescending(u => u.Country.Name));
                    case 7: return (direction == SortDirection.Ascending ? items.OrderBy(u => u.Status) : items.OrderByDescending(u => u.Status));
                }

                return items;
            };
        }

        void userListView_RetrieveItem(object sender, PagedListView<User>.RetrieveItemEventArgs e) {
            e.ListViewItem = new ListViewItem(new[] {  e.Item.UserId.ToString(), e.Item.Email, e.Item.FirstName, e.Item.LastName, 
                                                        e.Item.ShortName, e.Item.Affiliation.Entity.Name, e.Item.Country.Name, e.Item.Status.ToString()});
        }

        public override void Initialize() {
            filterControl.AddFilter("Status", new EnumComboBox<UserStatus>(true));
            filterControl.AddFilter("Country", FilterControl.FilterType.ComboText, DataContext.Countries.Select(c => c.Name).ToArray());
            filterControl.AddFilter("Affiliation", FilterControl.FilterType.ComboText, DataContext.Entities.Select(s => s.Name).ToArray());
            filterControl.AddFilter("Name", FilterControl.FilterType.Text);

            filterControl.Filter += new EventHandler(filterControl_Filter);

            userListView.Update(DataContext.Users);
        }

        void filterControl_Filter(object sender, EventArgs e) {            
            var users = DataContext.Users.Where(u => (((u.FirstName + " " + u.LastName).Contains(filterControl["Name"]) ||
                                                        u.ShortName.Contains(filterControl["Name"])) &&
                                                        u.Affiliations.Any(a => a.Entity.Name.Contains(filterControl["Affiliation"]) &&
                                                        u.Country.Name.Contains(filterControl["Country"]))));


            if (!String.IsNullOrEmpty(filterControl["Status"])) {
                users = users.Where(u => u.Status == (UserStatus)Enum.Parse(typeof(UserStatus), filterControl["Status"]));
            }

            userListView.Update(users);
        }

        private void previousButton_Click(object sender, EventArgs e) {
            userListView.Previous();
        }

        private void nextButton_Click(object sender, EventArgs e) {
            userListView.Next();
        }

        private void userListView_DoubleClick(object sender, EventArgs e) {
            if (userListView.SelectedItem != null) {
                new AffiliationsForm(DataContext).ShowDialog(userListView.SelectedItem);
            }
        }
    }
}
