using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Fudge.Framework.Database {
    public partial class Picture {
        public static int TeamDefaultPicture = 325;
        public static int ProfileDefaultPicture = 324;

        public static int CreateFrom(string title,byte[] image) {
            FudgeDataContext db = new FudgeDataContext();
            Picture newPic = new Picture {
                Data = new Binary(image),
                Title = title,
            };
            db.Pictures.InsertOnSubmit(newPic);
            db.SubmitChanges();
            return newPic.PictureId;
        }

        public static Picture GetPictureById(int id) {
            FudgeDataContext db = new FudgeDataContext();
            return db.Pictures.SingleOrDefault(p => p.PictureId == id);
        }
    }
}
