using System;

public partial class Help_Faq : FudgePage {
    public Help_Faq()
        : base(MenuItem.Help, false) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        Title += ".Help.Faq";
    }
}
