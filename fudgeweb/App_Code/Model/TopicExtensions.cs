using System.Linq;
using System.Xml.Linq;
using Fudge.Framework.Database;

/// <summary>
/// Summary description for TopicExtensions
/// </summary>
public static class TopicExtensions {
    public static string GetRss(this Topic topic) {
        return new XElement("rss",
                            new XAttribute("version", "2.0"),
                            new XElement("channel",
                                new XElement("title", topic.Title),
                                new XElement("link", "http://" + Util.BaseUrl + "/Community/Forum/Posts/" + topic.TopicId),
                                new XElement("description", topic.Title),
                                from p in topic.Posts
                                select new XElement("item",
                                            new XElement("title", p.Title),
                                            new XElement("link", "http://" + Util.BaseUrl + "/Community/Forum/Posts/" + topic.TopicId),
                                            new XElement("description", p.Message),
                                            new XElement("pubDate", p.Timestamp)
                                            )
                                )
                ).ToString();
    }
}
