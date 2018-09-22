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
    public partial class AffiliationManager : ManagerControl {
        private class AffiliationsListView : PagedListView<Affiliation> { }

        public AffiliationManager() {
            InitializeComponent();

            affiliationsListView.RetrieveItem += new PagedListView<Affiliation>.RetrieveItemEventHandler(affiliationsListView_RetrieveItem);

            affiliationsListView.Selector = (ListViewItem item) => { return DataContext.Affiliations.SingleOrDefault(a => a.AffiliationId == Int32.Parse(item.SubItems[0].Text)); };
            affiliationsListView.Sorter = (IQueryable<Affiliation> items, int parameter, SortDirection direction) => {
                switch (parameter) {
                    case 0: return (direction == SortDirection.Ascending ? items.OrderBy(a => a.AffiliationId) : items.OrderByDescending(a => a.AffiliationId));
                    case 1: return (direction == SortDirection.Ascending ? items.OrderBy(a => a.Entity.Name) : items.OrderByDescending(a => a.Entity.Name));
                    case 2: return (direction == SortDirection.Ascending ? items.OrderBy(a => a.Entity.Type) : items.OrderByDescending(a => a.Entity.Type));
                    case 3: return (direction == SortDirection.Ascending ? items.OrderBy(a => a.Email) : items.OrderByDescending(a => a.Email));
                    case 4: return (direction == SortDirection.Ascending ? items.OrderBy(a => a.JoinTime) : items.OrderByDescending(a => a.JoinTime));
                    case 5: return (direction == SortDirection.Ascending ? items.OrderBy(a => a.LeaveTime) : items.OrderByDescending(a => a.LeaveTime));
                    case 6: return (direction == SortDirection.Ascending ? items.OrderBy(a => a.Type) : items.OrderByDescending(a => a.Type));
                    case 7: return (direction == SortDirection.Ascending ? items.OrderBy(a => a.Status) : items.OrderByDescending(a => a.Status));
                }

                return items;
            };
        }

        void affiliationsListView_RetrieveItem(object sender, PagedListView<Affiliation>.RetrieveItemEventArgs e) {
            e.ListViewItem = new ListViewItem(new [] {e.Item.AffiliationId.ToString(), e.Item.Entity.Name, e.Item.Entity.Type.ToString(),
                                                        e.Item.Email, e.Item.JoinTime.ToString(), e.Item.LeaveTime.ToString(),
                                                        e.Item.Type.ToString(), e.Item.Status.ToString()});
        }

        public void Initialize(User user) {
            affiliationsListView.Update(DataContext.Affiliations.Where(a => a.UserId == user.UserId));
        }
    }
}
