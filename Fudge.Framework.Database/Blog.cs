using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Framework.Database {
    public partial class Blog {
        public static Blog GetBlogById(int id) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Blogs.SingleOrDefault(b => b.BlogId == id);
        }        
    }
}
