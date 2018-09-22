using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Web.AdminConsole.Utils {
    public static class LazyListViewExtensions {

        public const int DefaultLength = 128;

        public static void Lazify(this ListView listView) {
            foreach (ListViewItem item in listView.Items) {
                item.Lazify();
            }
        }

        public static void Lazify(this ListView listView, int[] subItemIndices) {
            foreach (ListViewItem item in listView.Items) {
                item.Lazify(subItemIndices);
            }
        }

        public static void Lazify(this ListViewItem item) {
            foreach (ListViewItem.ListViewSubItem subItem in item.SubItems) {
                subItem.Lazify();
            }
        }

        public static void Lazify(this ListViewItem item, int[] subItemIndices) {
            foreach (int subItemIndex in subItemIndices) {
                item.SubItems[subItemIndex].Lazify();
            }
        }

        public static void Lazify(this ListViewItem.ListViewSubItem subItem) {
            subItem.Tag = subItem.Text;
            subItem.Text = subItem.Text.Substring(0, Math.Min(DefaultLength, subItem.Text.Length));
        }
    }
}
