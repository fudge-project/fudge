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
using Fudge.Framework.Database;
using System.IO;
using System.Text;
using System.Collections.Generic;

public partial class Users_Invite : FudgePage {
    public Users_Invite()
        : base(MenuItem.MyProfile) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Users[" + FudgeUser.FullName + "].InviteFriends()";
    }

    protected void Invite_Click(object sender, EventArgs e) {       
        var badEmails = new List<string>();
        foreach (var address in emails.Text.Split(',')) {
            //send the invitation email
            if (!FudgeUser.Invite(address)) {
                badEmails.Add(address);
            }
        }
        message.Visible = true;
        if (badEmails.Count > 0) {
            message.InnerText = "There was an error sending invite, to the following emails " + badEmails.Join(",");
        }
        else {
            message.Attributes["class"] = "fudge_message";
            message.InnerText = "Invitation sent successfully";
        }
        emails.Text = String.Empty;
    }
}
