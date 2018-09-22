using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Extensions;

public partial class Controls_PopupContent : System.Web.UI.UserControl {
    private ITemplate _popupTemplate;

    [TemplateContainer(typeof(PopupContainer))]
    public ITemplate PopupTemplate {
        get {
            return _popupTemplate;
        }
        set {
            _popupTemplate = value;
        }
    }

    public string TargetControlID {
        get {
            return ModalPopupExtender1.TargetControlID;
        }
        set {
            ModalPopupExtender1.TargetControlID = value;
            ConfirmButtonExtender1.TargetControlID = value;
        }
    }

    public string OkButtonText {
        get {
            return ButtonOk.Text;
        }
        set {
            ButtonOk.Text = value;
        }
    }

    public Unit Width {
        get {
            return PopupPanel.Width;
        }
        set {
            PopupPanel.Width = value;
        }
    }

    public bool ShowCloseButton {
        get {
            bool? showClose = (bool?)ViewState["ShowCloseButton"];
            return showClose.HasValue ? showClose.Value : false;
        }
        set {
            ViewState["ShowCloseButton"] = value;
        }
    }

    protected void Page_Init() {
        if (_popupTemplate != null) {
            var container = new PopupContainer();
            _popupTemplate.InstantiateIn(container);
            popupContent.Controls.Add(container);
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        if (ShowCloseButton) {
            ModalPopupExtender1.CancelControlID = CloseButton.ID;
        }
        else {
            ModalPopupExtender1.CancelControlID = ButtonCanel.ID;
        }

    }

    public T FindTemplateControl<T>(string id) where T : Control {
        var content = PopupPanel.FindControl<PlaceHolder>("popupContent");
        if (content != null) {
            var panel = content.Controls[0];
            if (panel != null) {
                return panel.FindControl<T>(id);
            }
        }
        return null;
    }

    public class PopupContainer : Control, INamingContainer {

    }
}
