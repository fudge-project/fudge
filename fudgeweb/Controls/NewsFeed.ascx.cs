using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Fudge.Framework.Database;

public partial class Controls_NewsFeed : System.Web.UI.UserControl {

    protected NewsFeedType? SelectedType {
        get {
            return (NewsFeedType?)ViewState["SelectedType"];
        }
        set {
            ViewState["SelectedType"] = value;
        }
    }


    protected string GetImage(object type) {
        NewsFeedType feedType = (NewsFeedType)type;
        return FormatHelper.GetNewsFeedImage(feedType);
    }

    protected void Page_Load(object sender, EventArgs e) {

    }

    protected void feedSource_Selecting(object sender, LinqDataSourceSelectEventArgs e) {
        FudgeDataContext db = new FudgeDataContext();
        var feeds = SelectedType.HasValue ? db.NewsFeeds.Where(f => f.Type == SelectedType.Value) : db.NewsFeeds;
        e.Result = feeds.OrderByDescending(f => f.Timestamp).Take(20);
    }

    protected string ParseDates(string text) {
        string dateRegex = @"{(?<date>.*?)}";
        foreach (Match m in Regex.Matches(text, dateRegex)) {
            string date = m.Groups["date"].Value;
            text = Regex.Replace(text, "{" + date + "}", FormatHelper.FormatDateTimeNice(DateTime.Parse(date)));
        }
        return text;
    }

    protected void feed_ItemCommand(object sender, ListViewCommandEventArgs e) {
        if (e.CommandName == "Filter") {
            SelectedType = (NewsFeedType)Convert.ToInt32(e.CommandArgument);
            feed.DataBind();
        }
    }
}
