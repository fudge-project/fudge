using System;
using System.Linq;
using Fudge.Framework.Database;

public partial class Controls_Tooltip : System.Web.UI.UserControl {
    FudgeDataContext db = new FudgeDataContext();
    public string Text {
        get {
            return (string)ViewState["Text"];
        }
        set {
            ViewState["Text"] = value;
        }
    }

    public bool RenderAsError {
        get {
            bool? renderAsError = (bool?)ViewState["RenderAsError"];
            return renderAsError.HasValue ? renderAsError.Value : false;
        }
        set {
            ViewState["RenderAsError"] = value;
            if (value) {
                message.Attributes["class"] = "error";
            }
            else {
                message.Attributes["class"] = "fudge_message";
            }
        }
    }

    public bool IsClosable {
        get {
            bool? renderAsError = (bool?)ViewState["IsClosable"];
            return renderAsError.HasValue ? renderAsError.Value : true;
        }
        set {
            ViewState["IsClosable"] = value;
        }
    }

    public User.Tooltips? TipType {
        get {
            object o = ViewState["TipType"];
            return o != null ? (User.Tooltips?)o : null;
        }
        set {
            ViewState["TipType"] = value;
        }
    }

    private event EventHandler _close;
    public event EventHandler Close {
        add {
            _close += value;
        }
        remove {
            _close -= value;
        }
    }

    public bool IsLoadingTip {
        get {
            object o = ViewState["IsLoadingTip"];
            return o != null ? (bool)o : false;
        }
        set {
            if (value) {
                if (String.IsNullOrEmpty(Text)) {
                    Text = "Loading...";
                }
                IsClosable = false;
            }
            ViewState["IsLoadingTip"] = value;
        }
    }

    public void Show() {
        Visible = true;
    }

    public void Hide() {
        Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e) {
        //TODO:may not need this
        message.Attributes["class"] = RenderAsError ? "error" : "fudge_message";

        if (TipType.HasValue) {
            Visible = !User.IsTooltipSet(TipType.Value);
        }
    }

    protected User User {
        get {
            return db.Users.Single(u => u.UserId == User.LoggedInUser.UserId);
        }
    }

    protected void closeMessage_Click(object sender, EventArgs e) {
        //hide the control
        Hide();

        if (_close != null) {
            _close(this, EventArgs.Empty);
        }

        if (TipType.HasValue) {
            User.TooltipFlag &= ~(int)TipType;
            db.SubmitChanges();
        }
    }
}
