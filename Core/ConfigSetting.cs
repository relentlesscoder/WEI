using System;
using System.Configuration;

namespace WEI.Core
{
    public static class ConfigSetting
    {
        private static int _articleWidgetArticleNumber;
        private static string _localDomainUrl;
        private static string _domainUrl;

        public static int ArticleWidgetArticleNumber
        {
            get
            {
                if(_articleWidgetArticleNumber <= 0)
                {
                    _articleWidgetArticleNumber = Convert.ToInt32(ConfigurationManager.AppSettings["ArticleWidgetArticleNumber"]);
                }
                return _articleWidgetArticleNumber;
            }
        }

        public static string LocalDomainUrl
        {
            get
            {
                if(String.IsNullOrEmpty(_localDomainUrl))
                {
                    _localDomainUrl = ConfigurationManager.AppSettings["LocalDomainUrl"];
                }
                return _localDomainUrl;
            }
        }

        public static string DomainUrl
        {
            get
            {
                if(String.IsNullOrEmpty(_domainUrl))
                {
                    _domainUrl = ConfigurationManager.AppSettings["DomainUrl"];
                }
                return _domainUrl;
            }
        }
    }
}
