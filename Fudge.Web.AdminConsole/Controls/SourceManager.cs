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
    public partial class SourceManager : UserControl {

        public FudgeDataContext DataContext { get; set; }

        public SourceManager() {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e) {            

            Source source = new Source();
            source.Name = sourceTextBox.Text;

            DataContext.Sources.InsertOnSubmit(source);
            DataContext.SubmitChanges();

            sourceListView.Items.Add(new ListViewItem(new [] {source.SourceId.ToString(), source.Name}));
        }

        private void filterButton_Click(object sender, EventArgs e) {
            UpdateSourceListView();

            //TODO: where() contains()...
            foreach (ListViewItem item in sourceListView.Items) {
                if (!item.SubItems[1].Text.ToLower().Contains(sourceTextBox.Text.ToLower().Trim())) {
                    sourceListView.Items.Remove(item);
                }
            }
        }

        public void UpdateFields() {
            UpdateSourceListView();
        }

        private void UpdateSourceListView() {            

            sourceListView.Items.Clear();

            foreach (Source source in DataContext.Sources) {
                sourceListView.Items.Add(new ListViewItem(new[] { source.SourceId.ToString(), source.Name }));
            }
        }
    }
}
