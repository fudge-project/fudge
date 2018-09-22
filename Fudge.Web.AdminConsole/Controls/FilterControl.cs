using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public partial class FilterControl : UserControl {
        public enum FilterType {
            Custom,
            Text,
            Combo,
            ComboText,
        }

        private Dictionary<string, Control> Filters { get; set; }

        public string this[string index] {
            get { return Filters[index].Text; }
        }

        private event EventHandler _filter;
        public event EventHandler Filter { add { _filter += value; } remove { _filter -= value; } }

        public FilterControl() {
            InitializeComponent();
            Filters = new Dictionary<string, Control>();
        }

        private void SetComboBoxWidth(ComboBox c, int maxWidth) {
            int maxSize = 0;
            System.Drawing.Graphics g = CreateGraphics();
            
            for (int i = 0; i < c.Items.Count; i++) {
                c.SelectedIndex = i;
                SizeF size = g.MeasureString(c.Text, c.Font);
                if (maxSize < (int)size.Width) {
                    maxSize = (int)size.Width;
                }
            }

            c.DropDownWidth = c.Width;
            if (c.DropDownWidth < maxSize) {
                c.DropDownWidth = maxSize;
            }

            c.SelectedIndex = -1;

            c.Width = Math.Min(c.DropDownWidth, maxWidth);
        }

        public void AddFilter(string key, FilterType type) {
            AddFilter(key, type, new string[] { });
        }

        public void AddFilter(string key, FilterType type, string[] sources) {
            AddFilter(key, type, sources, null);
        }

        public void AddFilter(string key, Control customFilter) {
            AddFilter(key, FilterType.Custom, null, customFilter);
        }

        private void AddFilter(string key, FilterType type, string[] sources, Control customFilter) {
            Control filter = null;

            switch (type) {
                case FilterType.Text: {
                        filter = new TextBox();
                    } break;

                case FilterType.Combo:
                case FilterType.ComboText: {
                        filter = new ComboBox();

                        if (type == FilterType.Combo) {
                            ((ComboBox)filter).DropDownStyle = ComboBoxStyle.DropDownList;
                        }
                        else if (type == FilterType.ComboText) {
                            ((ComboBox)filter).DropDownStyle = ComboBoxStyle.DropDown;
                            ((ComboBox)filter).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            ((ComboBox)filter).AutoCompleteSource = AutoCompleteSource.CustomSource;
                        }

                        foreach (string source in sources) {
                            ((ComboBox)filter).Items.Add(source);
                            ((ComboBox)filter).AutoCompleteCustomSource.Add(source);
                        }

                        SetComboBoxWidth((ComboBox)filter, 256);

                    } break;

                case FilterType.Custom: {
                        filter = customFilter;
                    } break;
            }

            if (filter == null) {
                throw new Exception("Unsupported filter type.");
            }            

            filtersPanel.Controls.Add(filter);
            filtersPanel.Controls.Add(new Label { Text = key + ":", Anchor = AnchorStyles.Left, AutoSize = true });            

            Filters.Add(key, filter);
        }

        private void filterButton_Click(object sender, EventArgs e) {
            _filter(sender, e);
        }
    }
}
