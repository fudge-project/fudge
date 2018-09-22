using System;

public partial class VisualProgress : System.Web.UI.UserControl {
    public int MaxValue {
        get {
            return (int)ViewState["MaxValue"];
        }
        set {
            ViewState["MaxValue"] = value;
        }
    }

    public int Numerator {
        get {
            return (int)ViewState["Numerator"];
        }
        set {
            ViewState["Numerator"] = value;
        }
    }

    public int Denominator {
        get {
            return (int)ViewState["Denominator"];
        }
        set {
            ViewState["Denominator"] = value;
        }
    }

    public bool RenderTextAsLink {
        get {
            object o = ViewState["RenderTextAsLink"];
            return o == null ? false : (bool)o;
        }
        set {
            ViewState["RenderTextAsLink"] = value;
        }
    }
    public string Href {
        get {
            return (string)ViewState["Href"];
        }
        set {
            ViewState["Href"] = value;
        }
    }

    protected int ProgressWidth {
        get {
            if (Denominator == 0) {
                return MaxValue;
            }
            return (MaxValue * Numerator) / Denominator;
        }
    }

    public string ProgressIndicatorColor {
        get {
            return Denominator == 0 ? BackgroundColor : (string)ViewState["ProgressIndicatorColor"];
        }
        set {
            ViewState["ProgressIndicatorColor"] = value;
        }
    }

    public string BackgroundColor {
        get {
            return (string)ViewState["BackgroundColor"];
        }
        set {
            ViewState["BackgroundColor"] = value;
        }
    }

    public string TextColor {
        get {
            return (string)ViewState["TextColor"];
        }
        set {
            ViewState["TextColor"] = value;
        }
    }    

    public string BorderColor {
        get {
            return (string)ViewState["BorderColor"];
        }
        set {
            ViewState["BorderColor"] = value;
        }
    }    
    
    
    public string Text {
        get {
            string text = (string)ViewState["DisplayText"];
            if (String.IsNullOrEmpty(text)) {
                return "&nbsp";
            }
            return text;
        }
        set {
            ViewState["DisplayText"] = value;
        }
    }    

    protected void Page_Load(object sender, EventArgs e) {

    }
}
