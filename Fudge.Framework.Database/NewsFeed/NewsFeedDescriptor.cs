using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fudge.Framework.Database;

namespace Fudge.Framework.Database {
    public static class NewsFeedDescriptor {

        public static string BuildNewsFeedDescriptorList(params object[] items) {
            string result = String.Empty;

            foreach (object item in items) {
                result += item + ",";
            }

            return result.TrimEnd(',');
        }

        public static T GetIdentifiable<T>(IQueryable<T> pool, Func<T, int> selector, string id) where T : class {
            return pool.SingleOrDefault(i => selector(i) == Int32.Parse(id)) as T;
        }
    }
}
