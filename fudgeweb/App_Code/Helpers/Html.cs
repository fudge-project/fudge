using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Fudge.Framework.Database;
using Resources;
using System.Web;

/// <summary>
/// Helper methods for generating Html
/// </summary>
public static class Html {
    public const int DefaultPageSize = 10;
    private const int Delta = 10;
    /// <summary>
    /// Creates the markup for a link &lt;a href="url"&gt;text&lt;/a&gt;
    /// </summary>
    /// <param name="url">url of the site</param>
    /// <param name="text">text for the link</param>
    /// <returns></returns>
    public static string Link(string url, string text, params object[] content) {
        return AsLink(url, text, content).ToString();
    }

    public static string LinkToProblem(int problemId) {
        var problem = Problem.GetProblemById(problemId);
        return LinkToProblem(problemId, problem.Name);
    }

    public static string LinkToPicture(int pictureId) {
        return new XElement("img",
            new XAttribute("src", "/Images/" + pictureId)).ToString();
    }

    public static string LinkToTag(int tagId) {
        FudgeDataContext db = new FudgeDataContext();
        var tag = db.Tags.SingleOrDefault(s => s.TagId == tagId);
        return Link("/Problems/Archive/Tags/" + tag.UrlName, tag.Keyword);
    }

    public static string LinkToProblem(int problemId, string linkText) {
        var problem = Problem.GetProblemById(problemId);
        return Link("/Problems/Archive/" + problem.UrlName, linkText);
    }

    public static string LinkToRuns(int? uid, int? pid, int? lid, string name) {
        var query = Util.CreateQueryString(new Dictionary<string, object> {
            { "uid", uid },
            { "pid", pid },
            { "lid", lid }
        });
        return Html.Link("/Problems/Runs.aspx" + query, name);
    }

    public static string LinkToProfile(int userId) {
        var user = User.GetUserById(userId);
        return LinkToProfile(userId, user.DisplayName);
    }


    public static string LinkToContest(int contestId) {
        var contest = Contest.GetContestById(contestId);
        return Link("/Contests/" + contest.UrlName, contest.Name);
    }

    public static string LinkToProfile(int userId, string displayText) {
        var user = User.GetUserById(userId);
        return Link("/Users/Profile/" + userId, displayText);
    }

    public static string LinkToSchoolProfile(int schoolId) {
        var db = new FudgeDataContext();

        var school = db.Schools.SingleOrDefault(s => s.SchoolId == schoolId);

        return Link("/Schools/" + schoolId, school.Name);
    }

    public static string LinkToCountryProfile(int countryId) {
        var db = new FudgeDataContext();

        var country = db.Countries.SingleOrDefault(c => c.CountryId == countryId);

        return Link("/Country/" + countryId, country.Name);
    }

    public static string LinkToTeamProfile(int teamId) {
        var db = new FudgeDataContext();

        var team = db.Teams.SingleOrDefault(t => t.TeamId == teamId);

        return Link("/Teams/" + teamId, team.Name);
    }

    public static string LinkToTopics(int forumId) {
        var db = new FudgeDataContext();

        var topic = db.Topics.SingleOrDefault(t => t.ForumId == forumId);

        return Link("/Community/Forum/Topic/" + topic.ForumId, topic.Title);
    }

    public static string LinkToPosts(int topicId) {
        var db = new FudgeDataContext();

        var post = db.Posts.FirstOrDefault(p => p.TopicId == topicId);

        return Link("/Community/Forum/Posts/" + post.TopicId, post.Title);
    }

    public static string LinkToPost(int postId) {
        var db = new FudgeDataContext();

        var post = db.Posts.SingleOrDefault(p => p.PostId == postId);

        return Link("/Community/Forum/Posts/" + post.TopicId, post.Title);
    }

    public static string LinkToBlog(int blogId, string text) {
        var db = new FudgeDataContext();

        var blog = db.Blogs.SingleOrDefault(b => b.BlogId == blogId);

        return Link("/Community/Blogs/" + blog.UrlName, text);
    }

    public static string LinkToBlog(int blogId) {
        var db = new FudgeDataContext();

        var blog = db.Blogs.SingleOrDefault(b => b.BlogId == blogId);

        return LinkToBlog(blogId, blog.Name);
    }

    public static XElement LinkToCompileError(Run run) {
        string linkHref = String.Format("javascript:viewErrorPopup('{0}', '{1}')", run.RunId, run.RunId + "_error");
        return AsLink(linkHref, Resource.CompilationError,
                new XAttribute("class", "badrun"),
                new XAttribute("id", run.RunId));
    }

    public static XElement LinkToRuntimeError(Run run) {
        string linkHref = String.Format("javascript:viewErrorPopup('{0}', '{1}')", run.RunId, run.RunId + "_error");
        return AsLink(linkHref, Resource.RunTimeError,
                new XAttribute("class", "badrun"),
                new XAttribute("id", run.RunId));
    }


    /// <summary>
    /// Creates the markup for a link &lt;a href="url"&gt;text&lt;/a&gt; as an XElement
    /// </summary>
    /// <param name="url">url of the site</param>
    /// <param name="text">text for the link</param>
    /// <returns>XElement representation of the link</returns>
    public static XElement AsLink(string url, string text, params object[] content) {
        return new XElement("a",
                new XText(text),
                new XAttribute("href", url),
                content
            );
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string Image(string image) {
        return AsImage(image).ToString();
    }

    public static string Image(string image, string alt, params object[] args) {
        return AsImage(image, new XAttribute("alt", alt), args).ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static XElement AsImage(string image, params object[] args) {
        return new XElement("img",
                new XAttribute("src", "/site/images/" + image),
                args
            );
    }

    public static XElement AsProfileImage(int userId, params object[] args) {
        return new XElement("img",
                new XAttribute("src", "/Common/Images/" + User.GetUserById(userId).PictureId),
                args
            );
    }


    public static XElement AsColor(string text, string color) {
        return new XElement("span",
                    new XText(text),
                new XAttribute("style", "color:" + color));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static string Color(string text, string color) {
        return AsColor(text, color).ToString();
    }

    public static string Rss(string url, params object[] args) {
        return new XElement("link",
                new XAttribute("rel", "alternate"),
                new XAttribute("type", "application/rss+xml"),
                new XAttribute("title", "RSS"),
                new XAttribute("href", String.Format(url, args))).ToString();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="page"></param>
    /// <param name="count"></param>
    /// <param name="href"></param>
    /// <returns></returns>
    public static string CreateNumericPagerLinks(int pageSize, int page, int count, string href) {
        //get the page count
        int pageCount = ((count - 1) / pageSize) + 1;

        //calculate page group
        int group = (page / Delta) * Delta;

        int end = group + Math.Min(Delta, pageCount - group);
        if (end - group < Delta) {
            int g = group;
            group -= (Delta - (end - group));
            if (group <= 0) {
                group = g;
            }
        }
        int start = group > 0 ? group - 1 : group + 1;
        int finish = group + 1 == end ? end - 1 : end;

        var links = new List<XElement>();
        for (int i = start; i <= finish; i++) {
            string pageText = i.ToString();
            if (group > 0 && (i == start || i == finish)) {
                pageText = "...";
            }
            if (i != page) {
                links.Add(Html.AsLink(String.Format(href, i), pageText));
            }
            else {
                links.Add(new XElement("span",
                        new XText(i.ToString()),
                        new XAttribute("class", "active")));
            }
        }
        var pagingDiv = new XElement("div",
                            new XAttribute("class", "pager"),
                            links);

        return pagingDiv.ToString();
    }


    public static string CreateNextPrevPagerLinks(int pageSize, int page, int count, string href) {
        int pageCount = ((count - 1) / pageSize) + 1;
        var sb = new StringBuilder();
        if (page == 1) {
            sb.Append("Prev");
        }
        else {
            sb.Append(Html.Link(String.Format(href, page - 1), "Prev"));
        }
        sb.Append(" ");
        if (page == pageCount) {
            sb.Append("Next");
        }
        else {
            sb.Append(Html.Link(String.Format(href, page + 1), "Next"));
        }

        return sb.ToString();
    }

    public static string LinkToScoreboard(int contestId, string name) {
        FudgeDataContext db = new FudgeDataContext();
        var contest = db.Contests.SingleOrDefault(c => c.ContestId == contestId);
        return Html.Link("/Contests/Scoreboard/" + contest.UrlName, name);
    }
}
