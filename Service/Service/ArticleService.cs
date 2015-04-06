using System;
using System.Collections.Generic;
using WEI.Domain.Interface;
using WEI.Domain.Model;
using WEI.Service.Interface;
using WEI.Core;

namespace WEI.Service.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            if(articleRepository == null)
            {
                throw new ArgumentNullException("articleRepository");
            }
            _articleRepository = articleRepository;
        }

        #region IArticleService Members

        public Article GetArticleById(int articleId)
        {
            return _articleRepository.GetById(articleId);
        }

        public List<Article> GetFeaturedArticles()
        {
            return _articleRepository.Find((Article p) => p.PublishDate, Enums.SortOrder.Desc, p => p.IsFeatured);
        }

        public List<Article> GetMostRecentArticles(int articleCount)
        {
            return _articleRepository.Find((Article p) => p.PublishDate, Enums.SortOrder.Desc, count: articleCount);
        }

        public List<Article> GetMostPopularArticles(int articleCount)
        {
            return _articleRepository.Find((Article p) => p.Count, Enums.SortOrder.Desc, count: articleCount);
        }

        public void IncrementArticleCount(int articleId)
        {
            if(articleId > 0)
            {
                Article article = _articleRepository.GetById(articleId);
                int count = article.Count >= 0 ? article.Count : 0;
                article.Count = count + 1;
                _articleRepository.Update(article);
            }
        }

        #endregion
    }
}
