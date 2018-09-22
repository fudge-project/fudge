using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fudge.Web.AdminConsole.Controls {
    public class EnumComboBox<T> : ComboBox {
        public EnumComboBox(bool hasEmptyEnum)
            : base() {

            HasEmptyEnum = hasEmptyEnum;
            DropDownStyle = ComboBoxStyle.DropDownList;
            Enumerator = typeof(T);
        }

        private bool _hasEmptyEnum;
        public bool HasEmptyEnum {
            get {
                return _hasEmptyEnum;
            }

            set {
                _hasEmptyEnum = value;
                Enumerator = _enum;
            }
        }

        private Type _enum;
        public Type Enumerator {
            get {
                return _enum;
            }

            private set {
                _enum = value;

                if (Enumerator == null) {
                    return;
                }

                Items.Clear();

                if (HasEmptyEnum) {
                    Items.Add(String.Empty);
                }

                Items.AddRange(Enum.GetNames(Enumerator));
                SelectedIndex = 0;
            }
        }

        public T Value {
            get {
                if (Enumerator == null || (HasEmptyEnum && SelectedIndex == 0)) {
                    return default(T);
                }
                
                return Enum.GetValues(Enumerator).Cast<T>().ToArray().ElementAt(HasEmptyEnum ? SelectedIndex - 1 : SelectedIndex);
            }

            set {
                SelectedItem = Enum.GetName(Enumerator, value);
            }
        }        
    }
}
