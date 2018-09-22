using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fudge.Framework.Database {
    public partial class Rating {

        public double Popularity {
            get {
                return Count == 0 ? 1 : 1 + (Sum / Count) * Math.Log10(Count + 1);
            }
        }

        //creates a new rating and returns the id
        public static int NewRating() {
            FudgeDataContext db = new FudgeDataContext();
            var rating = new Rating {
                Sum = 0,
                Count = 0
            };
            db.Ratings.InsertOnSubmit(rating);
            db.SubmitChanges();
            return rating.RatingId;
        }
    }
}
