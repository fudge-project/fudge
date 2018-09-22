using System;

public partial class Users_Notifications : FudgePage {
    public Users_Notifications()
        : base(MenuItem.MyProfile) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        notificationsSource.WhereParameters["UserId"].DefaultValue = FudgeUser.UserId.ToString();
        Title += ".Users[" + FudgeUser.FullName + "].Notifications";
    }

}
