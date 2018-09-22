using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Web.AdminConsole.Controls {

    public enum SortDirection { Ascending, Descending }

    public class PagedListView<T> : ListView {

        public const int DefaultPageSize = 100;

        public class RetrieveItemEventArgs {
            public RetrieveItemEventArgs(T item) {
                Item = item;
            }

            public ListViewItem ListViewItem { get; set; }
            public T Item { get; private set; }
        }

        public delegate void SelectedItemChangedEventHandler(object sender, EventArgs e);
        public delegate void RetrieveItemEventHandler(object sender, RetrieveItemEventArgs e);
        public event RetrieveItemEventHandler RetrieveItem;
        public event SelectedItemChangedEventHandler SelectedItemChanged;

        public T SelectedItem { get; private set; }
        public int SelectedIndex { get { return Page * PageSize + SelectedIndices[0]; } }
        public Func<ListViewItem, T> Selector { get; set; }

        private IQueryable<T> VirtualItems { get; set; }

        public int Page { get; set; }
        public int PageCount { get; private set; }
        public int PageSize { get; set; }
        
        public int SortColumn { get; set; }
        public SortDirection SortDirection { get; set; }
        public Func<IQueryable<T>, int, SortDirection, IQueryable<T>> Sorter { get; set; }

        public PagedListView() : base() {
            PageSize = DefaultPageSize;
            SortDirection = SortDirection.Ascending;            

            FullRowSelect = true;
            MultiSelect = false;
            
            SelectedIndexChanged += new EventHandler(PagedListView_SelectedIndexChanged);
            ColumnClick += new ColumnClickEventHandler(PagedListView_ColumnClick); 
        }

        void PagedListView_ColumnClick(object sender, ColumnClickEventArgs e) {
            if (Sorter == null) {
                return;
            }

            if (SortColumn == e.Column) {
                SortDirection = (SortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending;
            }
            else {
                SortDirection = SortDirection.Ascending;
            }

            SortColumn = e.Column;
            Update(Sorter(VirtualItems, SortColumn, SortDirection));            
        }

        // TODO: account for multiple selection
        void PagedListView_SelectedIndexChanged(object sender, EventArgs e) {
            if (Selector == null) {
                return;
            }

            if (SelectedItems.Count == 1) {
                SelectedItem = Selector(SelectedItems[0]);
            }
            else {
                SelectedItem = default(T);
            }

            if (SelectedItemChanged != null) {
                SelectedItemChanged(this, new EventArgs());
            }
        }

        private void Display(int selectIndex) {
            Display();

            if (selectIndex != -1) {
                SelectedIndices.Add(selectIndex % PageSize);
                EnsureVisible(SelectedIndices[0]);
            }
        }

        private void Display() {
            BeginUpdate();
            
            Items.Clear();
            var pagedItems = VirtualItems.Skip(Page * PageSize).Take(PageSize);

            foreach(T item in pagedItems) {
                RetrieveItemEventArgs e = new RetrieveItemEventArgs(item);
                RetrieveItem(this, e);

                if (e.ListViewItem == null) {
                    throw new Exception("ListViewItem cannot be null");
                }

                Items.Add(e.ListViewItem);
            }

            AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            EndUpdate();
        }

        public void Update(IQueryable<T> items, int selectIndex) {
            if (selectIndex >= items.Count()) {
                throw new Exception("Selected index must be within bounds");
            }
            
            VirtualItems = (Sorter != null) ? Sorter(items, SortColumn, SortDirection) : items;            
            PageCount = VirtualItems.Count() / PageSize;

            if (selectIndex != -1) {
                Page = selectIndex / PageSize;                               
            }
            else {
                Page = PageCount;                
            }

            Display(selectIndex);
        }

        public void Update(IQueryable<T> items) {
            Update(items, -1);
        }

        public void Next() {
            if (Page < PageCount) {
                Page++;
                Display();
            }
        }

        public void Previous() {
            if (Page > 0) {
                Page--;
                Display();
            }
        }
    }
}
