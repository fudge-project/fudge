using System;
using System.Text.RegularExpressions;
using System.Web;
using Fudge.Framework.Database;
using System.Xml.Linq;

/// <summary>
/// Summary description for FormatHelper
/// </summary>
public static class FormatHelper {
    private static string[] AllowedPostTags = { "annot", "abbr", "acronym", "blockquote", "b", "em", "i", 
                                                 "li", "ol", "p", "pre", "strike", "sub", "sup", "strong", "u", "ul", "tt" };
    private const string HtmlRegexTemplate = @"&lt;(?<tag>{0})(?<attributes>.*?)&gt;(?<text>.*?)&lt;/(?<tag>{0})&gt;";

    //http://regexlib.com/RETester.aspx?regexp_id=96
    private const string LinkRegex = @"(?<url>(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)";


    public static string FormatPercentage(double value) {
        if (value == 0) {
            return "-";
        }
        return String.Format("{0:0} %", value);
    }

    public static string FormatSolved(bool solved) {
        return solved ? Html.Image("green_tick.gif") : String.Empty;
    }

    private static string FormatDateInternal(DateTime date) {
        return String.Format("{0:d}", date);
    }

    private static string FormatDateTimeInteral(DateTime date) {
        return String.Format("{0:g}", date);
    }

    private static string FormatDateTimeNiceInternal(DateTime date) {
        return String.Format("{0:f}", date);
    }

    private static string FormatDateNiceInternal(DateTime date) {
        return String.Format("{0:D}", date);
    }

    public static string FormatDateTimeNice(DateTime date) {
        if (User.LoggedInUser != null) {
            return FormatDateTimeNiceInternal(User.LoggedInUser.ToUserTimezone(date));
        }
        return FormatDateTimeNiceInternal(date);
    }

    public static string FormatDateNice(DateTime date) {
        if (User.LoggedInUser != null) {
            return FormatDateNiceInternal(User.LoggedInUser.ToUserTimezone(date));
        }
        return FormatDateNiceInternal(date);
    }

    public static string FormatDate(DateTime date) {
        User current = User.LoggedInUser;
        if (current != null) {
            return FormatDateInternal(current.ToUserTimezone(date));
        }
        return FormatDateInternal(date);
    }

    public static string FormatDateTime(DateTime date) {
        User current = User.LoggedInUser;
        if (current != null) {
            return FormatDateTimeInteral(current.ToUserTimezone(date));
        }
        return FormatDateTimeInteral(date);
    }


    private static string FormatTimeInternal(DateTime dateTime) {
        return String.Format("{0:t}", dateTime);
    }

    public static string FormatTime(DateTime dateTime) {
        User current = User.LoggedInUser;
        if (current != null) {
            return FormatTimeInternal(current.ToUserTimezone(dateTime));
        }
        return FormatTimeInternal(dateTime);
    }

    public static string FormatTime(TimeSpan span, bool hideSeconds) {
        return span.Hours.ToString().PadLeft(2, '0') + ":" +
               span.Minutes.ToString().PadLeft(2, '0') +
               (hideSeconds ? String.Empty : ":" + span.Seconds.ToString().PadLeft(2, '0'));
    }

    public static string FormatTime(TimeSpan span) {
        return FormatTime(span, false);
    }

    public static string FormatNewProblem(int pid) {
        return Problem.GetProblemById(pid).IsNew ? Html.Image("new.gif") : String.Empty;
    }

    public static string TextToMarkup(string rawText) {
        return HttpContext.Current.Server.HtmlEncode(rawText)
            .Replace("\r\n", "\n")
            .Replace("\n", "<br/>");
    }

    public static string LiteralFormatting(string rawText) {
        return rawText.Replace(" ", "&nbsp;");
    }

    public static string MarkupToText(string markup) {
        return HttpContext.Current.Server.HtmlDecode(markup.Replace("\n", "\r\n").Replace("<br/>", "\r\n"));
    }

    public static string FormatPost(string rawText) {
        string markup = TextToMarkup(rawText);
        foreach (var tag in AllowedPostTags) {
            string tagFormat = String.Format(HtmlRegexTemplate, tag);
            markup = Regex.Replace(markup, tagFormat, "<${tag}>${text}</${tag}>");
        }
        markup = Regex.Replace(markup, LinkRegex, Html.Link("${url}", "${url}"));

        return markup;
    }

    public static string FormatError(string errorText) {
        if (errorText != null) {
            //hack! java puts weird characters in the error field
            char badChar = (char)0x1A;
            int stopIndex = errorText.IndexOf(badChar);
            if (stopIndex >= 0) {
                errorText = errorText.Substring(0, stopIndex);
            }
            errorText = errorText.Trim().Replace("'", "\"").Replace("\"\"", String.Empty);
        }
        return errorText;
    }

    public static string FormatOnlineStatus(int userId) {
        return UserExtensions.OnlineUsers.Contains(userId) ?
            Html.Image("status_online.png", "online") :
            Html.Image("status_offline.png", "offline");
    }

    public static string GetNewsFeedImage(NewsFeedType type) {
        switch (type) {
            case NewsFeedType.NewProblem:
                return "/site/images/page_white_add.png";
            case NewsFeedType.SolvedProblem:
                return "/site/images/problemsolved.gif";
            case NewsFeedType.ShortestSolution:
                return "/site/images/text_letter_omega.png";                
            case NewsFeedType.CodeSnippet:
                return "/site/images/page_white_zip.png";
            case NewsFeedType.News:
                return "/site/images/newnews.jpg";
            case NewsFeedType.ForumPost:
            case NewsFeedType.SourcePost:
            case NewsFeedType.ProblemPost:
                return "/site/images/newpost.gif";
            case NewsFeedType.NewContest:
                return "/site/images/contest.gif";     
            case NewsFeedType.FirstUserFromSchool:
                return "/site/images/user_add.png";
        }
        return String.Empty;
    }

    public static string GetNewsFeedLink(NewsFeedType type) {
        switch (type) {
            case NewsFeedType.NewProblem:
                break;
            case NewsFeedType.SolvedProblem:
                return "View problem description";
            case NewsFeedType.ShortestSolution:
                break;
            case NewsFeedType.CodeSnippet:
                return "Check out this code snippet";
            case NewsFeedType.News:
                break;
            case NewsFeedType.ForumPost:
                break;
            case NewsFeedType.SourcePost:
                break;
            case NewsFeedType.ProblemPost:
                break;
            case NewsFeedType.NewContest:
                break;
            default:
                break;
        }
        return String.Empty;
    }

    public static string GetNotificationImage(int type) {
        return Html.Image("note.png");
    }

    public static string NewProblemSolved(bool solved) {
        return !solved ? String.Empty : Html.Image("bullet_star.png", "Fudge points awarded for this problem!");
    }
}
