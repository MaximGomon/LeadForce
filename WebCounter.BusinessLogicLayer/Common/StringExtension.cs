using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebCounter.BusinessLogicLayer.Common
{
    public static class StringExtension
    {
        public static T ToEnum<T>(this string text)
        {
            return (T) Enum.Parse(typeof (T), text);
        }



        /*public static T To<T>(this string text)
        {
            return (T)Convert.ChangeType(text, typeof(T));
        }*/



        public static Guid ToGuid(this string text)
        {
            return Guid.Parse(text);
        }



        public static int ToInt(this string text)
        {
            return int.Parse(text);
        }



        /// <summary>
        /// Truncates the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="length">The length.</param>
        /// <param name="atWord">if set to <c>true</c> [at word].</param>
        /// <param name="addDots">if set to <c>true</c> [add dots].</param>
        /// <returns></returns>
        public static string Truncate(this string text, int length, bool atWord, bool addDots)
        {
            if (text == null || text.Length <= length)
                return text;

            var result = text.Substring(0, length);

            if (atWord)
            {
                var alternativeCutOffs = new List<char>() {' ', ',', '.', '?', '/', ':', ';', '\'', '\"', '\'', '-'};
                var lastSpace = result.LastIndexOf(' ');

                if (lastSpace != -1 &&
                    (text.Length >= length + 1 && !alternativeCutOffs.Contains(text.ToCharArray()[length])))
                    result = result.Remove(lastSpace);
            }

            if (addDots)
                result += "...";

            return result;
        }



        /// <summary>
        /// Toes the HTML.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string ToHtml(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            const string regex = @"(?<link>(?<!href=['""]?)(?<!\>)(www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";
            var r = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            text = r.Replace(text, "<a href=\"${link}\" target=\"&#95;blank\">${link}</a>");

            text = text.Replace("href=\"www", "href=\"http://www");

            return text.Replace("\n", "<br/>");
        }



        /// <summary>
        /// Removes all html tags from string and leaves only plain text
        /// Removes content of <xml></xml> and <style></style> tags as aim to get text content not markup /meta data.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HtmlStrip(this string input)
        {
            input = Regex.Replace(input, "<style>(.|\n)*?</style>", string.Empty);
            input = Regex.Replace(input, @"<xml>(.|\n)*?</xml>", string.Empty); // remove all <xml></xml> tags and anything inbetween.  
            return Regex.Replace(input, @"<(.|\n)*?>", string.Empty); // remove any tags but not there content "<p>bob<span> johnson</span></p>" becomes "bob johnson"
        }
    }
}