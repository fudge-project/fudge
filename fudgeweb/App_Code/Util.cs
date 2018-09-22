using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Fudge.Framework.Database;
using System.Drawing.Drawing2D;
using Resources;


/// <summary>
/// General helper methods used site-wide
/// </summary>
public static class Util {
    private static HashSet<string> ImageExtensions = new HashSet<string> { ".jpg", ".jpeg", ".bmp", ".gif", ".png" };
    //pool of characters from which to create the activation code
    private const string Pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    //fit's smtp hostname
    private const string MailServer = "mailhost.fit.edu";
    public static string AdminEmail = "fudge@fit.edu";
    //fudge admin mailing address
    private static MailAddress FudgeAdmin = new MailAddress(AdminEmail, "Fudge");
    public static HttpContext context = HttpContext.Current;
    //base url of the website
    public static string BaseUrl = (context.Request.Url.Host == "localhost" && context.Request.Url.IsDefaultPort ? "fudge.fit.edu" : context.Request.Url.Host)
                                 + (context.Request.Url.IsDefaultPort ? String.Empty : ":" + context.Request.Url.Port);

    public static string ToMD5(this string hash) {
        MD5CryptoServiceProvider encrypter = new MD5CryptoServiceProvider();
        byte[] hashedBytes = encrypter.ComputeHash(Encoding.Default.GetBytes(hash));
        return hashedBytes.Select(b => b.ToString("x2").ToLower()).Join();
    }

    public static string Join(this IEnumerable<string> value, string separator) {
        return String.Join(separator, value.ToArray());
    }

    public static string Join(this IEnumerable<string> value) {
        return String.Join(String.Empty, value.ToArray());
    }

    public static string Join(this IEnumerable<char> chars) {
        return new string(chars.ToArray());
    }

    public static string CreateQueryString(Dictionary<string, object> values) {
        StringBuilder sb = new StringBuilder();
        bool first = true;
        foreach (var pair in values) {
            if (pair.Value != null) {
                if (first) {
                    sb.AppendFormat("?{0}={1}", pair.Key, pair.Value);
                    first = false;
                }
                else {
                    sb.AppendFormat("&{0}={1}", pair.Key, pair.Value);
                }
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// Determines if a querystring value is null
    /// </summary>
    /// <param name="request"></param>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public static bool IsQueryStringNull(this HttpRequest request, string queryString) {
        return String.IsNullOrEmpty(request.QueryString[queryString]);
    }

    /// <summary>
    /// Resizes the image to the specified dimensions
    /// </summary>
    /// <param name="image">original image</param>
    /// <param name="width">new width</param>
    /// <param name="height">new height</param>
    /// <returns></returns>
    public static Image Resize(this Image image, int width, int height) {
        if (image.Width <= width && image.Height <= height) {
            return image;
        }

        float scale = 1.0f;

        if (image.Width > width || image.Height > height) {
            if (image.Width > image.Height) {
                scale = width / (float)image.Width;
            }
            else {
                scale = height / (float)image.Height;
            }
        }

        Bitmap result = new Bitmap(width, height);

        using (Graphics g = Graphics.FromImage(result)) {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(image, 0, 0, width, height);
        }

        return result;
    }

    /// <summary>
    /// Converts this image to a byte array, with jpeg format
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    public static byte[] ToByteArray(this Image image, string extension) {
        extension = extension.ToLower();
        MemoryStream ms = new MemoryStream();
        if (extension == ".jpg" || extension == ".jpeg") {
            image.Save(ms, ImageFormat.Jpeg);
        }
        else if (extension == ".gif") {
            image.Save(ms, ImageFormat.Gif);
        }
        else if (extension == ".png") {
            image.Save(ms, ImageFormat.Png);
        }
        else if (extension == ".bmp") {
            image.Save(ms, ImageFormat.Bmp);
        }        
        return ms.ToArray();
    }

    public static bool IsSupportedImageExtension(string extension) {
        return ImageExtensions.Contains(extension);
    }

    /// <summary>
    /// Truncate the string to maxChars
    /// </summary>
    /// <param name="s"></param>
    /// <param name="maxChars"></param>
    /// <returns></returns>
    public static string Truncate(this string s, int maxChars) {
        if (s.Length > maxChars) {
            return s.Substring(0, maxChars) + "...";
        }
        return s;
    }

    public static XElement ToXElement(this string text, params object[] content) {
        return new XElement("span", new XText(text), content);
    }

    public static string GenerateActivationCode() {
        return GenerateRandomString(16);
    }

    public static string GenerateRandomString(int length) {
        StringBuilder sb = new StringBuilder();
        Random gen = new Random((int)DateTime.Now.Ticks);
        for (int i = 0; i < length; i++) {
            //get a random index to select the next character from the pool
            int value = gen.Next(Pool.Length - 1);
            sb.Append(Pool[value]);
        }
        return sb.ToString();
    }

    public static bool SendActivationEmail(User user) {
        return Email.WelcomeEmail.Send(user, "Fudge Account Activation", user.FirstName, BaseUrl, user.ActivationCode);
    }

    public static bool SendForgotPassword(User user) {
        return Email.ForgotPasswordEmail.Send(user, "Fudge Forgot Password", BaseUrl, user.ActivationCode);
    }

    public static bool SendEmail(User user, Email email, string subject, params object[] args) {
        return SendEmail(AdminEmail, user.SecondaryEmail, subject, String.Format(email.Content, args), false);        
    }

    public static bool SendEmail(string from, string to, string subject, string body, bool isHtml) {
        try {
            SmtpClient client = new SmtpClient(MailServer, 25);
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.Subject = subject;
            //add recepient                

            message.To.Add(to);
            message.IsBodyHtml = isHtml;
            message.Body = body;
            client.Send(message);
        }
        catch {
            return false;
        }
        return true;
    }

    public static bool Invite(this User user, string to) {
        string body = String.Format(Email.InviteEmail.Content, Util.BaseUrl, user.ReferralCode, user.FirstName);
        return SendEmail(AdminEmail, to, "Fudge Invitation", body, false);
    }
}

