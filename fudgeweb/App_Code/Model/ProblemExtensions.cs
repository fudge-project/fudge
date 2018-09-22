using System;
using System.Linq;
using System.Xml.Linq;
using Fudge.Framework.Database;

/// <summary>
/// Summary description for ProblemExtensions
/// </summary>
public static class ProblemExtensions {
    public static string GetRss() {
        var db = new FudgeDataContext();
        return new XElement("rss",
                            new XAttribute("version", "2.0"),
                            new XElement("channel",
                                new XElement("title", "New Problems"),
                                new XElement("link", "http://" + Util.BaseUrl + "/Problems/Archive"),
                                new XElement("description", "Freshly baked problems from Fudge!"),
                                from p in db.Problems.AsEnumerable()
                                where p.IsNew && p.Visible
                                select new XElement("item",
                                            new XElement("title", p.Name),
                                            new XElement("link", "http://" + Util.BaseUrl + "/Problems/Archive/" + p.UrlName),
                                            new XElement("description",
                                                p.Statement.Element("description").Value.Truncate(250) + "<br/><br/>" +
                                                Html.Link("http://" + Util.BaseUrl + "/Problems/Runs.aspx?pid=" + p.ProblemId, "Submissions")),
                                            new XElement("pubDate", DateTime.Now)
                                            )
                                )
                ).ToString();
    }
}
