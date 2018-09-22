using System;
using System.Linq;
using Fudge.Framework.Database;

public partial class Controls_Rating : System.Web.UI.UserControl {
    public int? RatingId {
        get {
            return (int?)ViewState["RatingId"];
        }
        set {
            ViewState["RatingId"] = value;
        }
    }
    
    private Rating Rating {
        get {            
            return db.Ratings.SingleOrDefault(r => r.RatingId == RatingId.Value);
        }
    }

    private UserRating UserRating {
        get {
            return Rating.UserRatings.SingleOrDefault(r => r.UserId == User.LoggedInUser.UserId);
        }
    }

    FudgeDataContext db = new FudgeDataContext();

    protected void Page_Load(object sender, EventArgs e) {
        if (User.LoggedInUser == null) {
            Visible = false;
        }
        else {
            if (!Page.IsPostBack) {
                Bind();
            }
        }
    }

    public void Bind() {
        if (RatingId.HasValue && User.LoggedInUser != null) {            
            var userRating = UserRating;            
            if (userRating == null) {
                userRating = new UserRating {
                    UserId = User.LoggedInUser.UserId,
                    RatingId = Rating.RatingId,
                    Value = 0
                };                
                db.UserRatings.InsertOnSubmit(userRating);
                db.SubmitChanges();
            }
            
            rating.CurrentRating = userRating.Value;

            if (userRating.Value == 0) {
                descMessage.InnerText = "Rate it!";
            }
            else {
                descMessage.InnerText = String.Format("Current Vote {0}", userRating.Value);
            }
            Visible = true;
        }
        else {
            Visible = false;
        }
    }

    protected void Rating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e) {
        //get the new rating
        int value = Int32.Parse(e.Value);        
        if (UserRating.Value == 0) {
            //initially the rating is 0
            Rating.Sum += value;
            Rating.Count++;
        }
        else {
            //remove old value
            Rating.Sum -= UserRating.Value;
            //add new value
            Rating.Sum += value;
        }
        //update the user rating
        UserRating.Value = value;        
        db.SubmitChanges();

        //rebind the data
        Bind();
    }
}
