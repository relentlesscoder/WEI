using System;

namespace WEI.Core
{
    public class URL
    {
        private static readonly string localDomainUrl;
        private static readonly string domainUrl;

        static URL()
        {
            // used on local testing environment
            localDomainUrl = ConfigSetting.LocalDomainUrl;
            // used on production server
            domainUrl = ConfigSetting.DomainUrl;
        }

        #region URL

        public static string GetArticleUrl(string title, int articleId)
        {
            string url = String.IsNullOrEmpty(localDomainUrl) ? domainUrl : localDomainUrl;
            title = title.GetTitleForSEOFriendlyUrl();

            return (url + "/article/" + articleId + "/" + title + "/").ToLower();
        }

        #endregion
    }
}
