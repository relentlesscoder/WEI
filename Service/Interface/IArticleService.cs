using System.Collections.Generic;
using WEI.Domain.Model;

namespace WEI.Service.Interface
{
    public interface IArticleService
    {
        Article GetArticleById(int articleId);
        List<Article> GetFeaturedArticles();
        List<Article> GetMostRecentArticles(int articleCount);
        List<Article> GetMostPopularArticles(int articleCount);
        void IncrementArticleCount(int articleId);
    }
}
