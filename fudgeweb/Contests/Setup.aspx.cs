using System;

public partial class Contests_Setup : FudgePage {
    public Contests_Setup()
        : base(MenuItem.Contests) {

    }

    protected void Page_Load(object sender, EventArgs e) {
        contestType.Attributes["onchange"] = "showDescription(this.selectedIndex)";
    }
}
