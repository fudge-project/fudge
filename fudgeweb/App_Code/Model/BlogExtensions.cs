using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fudge.Framework.Database;

public static class BlogExtensions {
    public static string GetRss(this Blog blog) {
        return new XElement("rss",
                            new XAttribute("version", "2.0"),
                            new XElement("channel",
                                new XElement("title", blog.Name),
                                new XElement("link", "http://" + Util.BaseUrl + "/Community/Blogs/" + blog.UrlName),
                                new XElement("description", blog.Description),
                                from t in blog.Forum.Topics                                
                                select new XElement("item",
                                            new XElement("title", t.Title),
                                            new XElement("link", "http://" + Util.BaseUrl + "/Community/Blogs/" + blog.UrlName + "/" + t.TopicId),
                                            new XElement("description", t.Posts[0].Message),
                                            new XElement("pubDate", t.Timestamp)
                                            )
                                )
                ).ToString();
    }
}
