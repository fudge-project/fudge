using System;
using System.Linq;
using System.Xml.Linq;
using Fudge.Framework.Database;

/// <summary>
/// Summary description for NewsFeedExtensions
/// </summary>
public static class NewsFeedExtensions {
    public static string GetTitle(this NewsFeed feed) {
        switch (feed.Type) {
            case NewsFeedType.NewProblem:
                return "New Problem Posted";
            case NewsFeedType.SolvedProblem:
                return "Problem Solved";
            case NewsFeedType.ShortestSolution:
                return "New Shortest solution";
            case NewsFeedType.CodeSnippet:
                return "New CodeSnippet Posted";
            case NewsFeedType.News:
                return "New News Posted";
            case NewsFeedType.ForumPost:
                return "New Forum Post";
            case NewsFeedType.SourcePost:
                return "New Source code Post";
            case NewsFeedType.ProblemPost:
                return "New Problem Post";
            case NewsFeedType.NewContest:
                return "New Contest Scheduled";            
        }
        return String.Empty;
    }

    public static string GetRss() {
        var db = new FudgeDataContext();
        return new XElement("rss",
                            new XAttribute("version", "2.0"),
                            new XElement("channel",
                                new XElement("title", "Fudge Live Feed"),
                                new XElement("link", "http://" + Util.BaseUrl),
                                new XElement("description", "See whats happenning on Fudge!"),
                                from feed in db.NewsFeeds.AsEnumerable()                                
                                select new XElement("item",
                                            new XElement("title", feed.GetTitle()),
                                            new XElement("link", feed.Link),
                                            new XElement("description",feed.Text),
                                            new XElement("pubDate", feed.Timestamp)
                                            )
                                )
                ).ToString();
    }
}
