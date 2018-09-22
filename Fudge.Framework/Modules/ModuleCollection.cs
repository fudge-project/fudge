using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.ModuleInterface;

namespace Fudge.Framework {
    public class ModuleCollection : IModuleCollection {        

        private List<IModule> list = new List<IModule>();

        public void Add(IModule item) {
            list.Add(item);
        }

        public void Clear() {
            list.Clear();
        }

        public bool Contains(IModule item) {
            return list.Contains(item);
        }

        public void CopyTo(IModule[] array, int arrayIndex) {
            list.CopyTo(array, arrayIndex);
        }

        public int Count {
            get { return list.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public bool Remove(IModule item) {
            return list.Remove(item);
        }

        public IEnumerator<IModule> GetEnumerator() {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
