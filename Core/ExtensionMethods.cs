using System;
using System.Text;
using System.Text.RegularExpressions;

namespace WEI.Core
{
    public static class ExtensionMethods
    {
        #region String Methods

        public static string GetTitleForSEOFriendlyUrl(this string title)
        {
            string titleForUrl = string.Empty;

            if(!String.IsNullOrEmpty(title))
            {
                titleForUrl = title.RemoveAccent();
                titleForUrl = Regex.Replace(titleForUrl, @"[^A-Za-z0-9\s-]", ""); // Remove all non valid chars          
                titleForUrl = Regex.Replace(titleForUrl, @"\s+", " ").Trim(); // convert multiple spaces into one space  
                titleForUrl = Regex.Replace(titleForUrl, @"\s", "-"); // //Replace spaces by dashes
            }

            return titleForUrl;
        }

        public static string RemoveAccent(this string text)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
            return Encoding.ASCII.GetString(bytes);
        }

        public static string StripHtml(this string htmlContent)
        {
            string plainText = string.Empty;

            if(!String.IsNullOrEmpty(htmlContent))
            {
                plainText = Regex.Replace(htmlContent, @"<[^>]*(>|$)", "");
                plainText = Regex.Replace(plainText, @"[\s\r\n]+", " ");
                plainText = plainText.Trim();
            }

            return plainText;
        }

        public static string GetCharacters(this string content, int count)
        {
            string text = string.Empty;

            if(!String.IsNullOrEmpty(content))
            {
                if(content.Length <= count)
                {
                    text = content;
                }
                else
                {
                    text = content.Substring(0, count) + "...";
                }
            }

            return text;
        }

        #endregion
    }
}
