using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Framework.Database {
    public partial class Entity {
        FudgeDataContext db = new FudgeDataContext();

        public static Entity GetEntityById(int id) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Entities.SingleOrDefault(e => e.EntityId == id);
        }
    }
}
