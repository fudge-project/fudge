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
using System.Collections.Generic;

public partial class Controls_TimezonesDropDown : System.Web.UI.UserControl {
    public string SelectedTimezone {
        get {
            return timezones.SelectedValue;
        }
        set {
            timezones.SelectedValue = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {
        if (timezones.Items.Count == 0) {
            var zones = new List<TimeZoneInfo>(TimeZoneInfo.GetSystemTimeZones());
            zones.Sort((left, right) => {
                int comparison = left.BaseUtcOffset.CompareTo(right.BaseUtcOffset);
                if (comparison == 0) {
                    return String.CompareOrdinal(left.DisplayName, right.DisplayName);
                }
                return comparison;

            });

            timezones.DataSource = zones;
            timezones.DataBind();
        }
    }
}
