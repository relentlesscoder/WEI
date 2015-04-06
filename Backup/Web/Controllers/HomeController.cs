using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WEI.Core;
using WEI.Domain.Model;
using WEI.Service.Interface;
using WEI.Web.Filters;
using WEI.Web.Models;

namespace WEI.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Instance Variables

        private readonly IArticleService _articleService;

        #endregion

        #region Constructors

        // Constructor Injection
        public HomeController(IArticleService articleService)
        {
            if (articleService == null)
            {
                throw new ArgumentNullException("articleService");
            }

            this._articleService = articleService;
        }

        #endregion

        #region Action Methods
        //
        // GET: /Home/
        [ElmahHandleError]
        public ActionResult Index()
        {
            List<Article> articles = _articleService.GetFeaturedArticles();
            IEnumerable<ArticleViewModel> viewModels = SetViewModels(articles);

            return View(viewModels);
        }

        #endregion

        #region Private Methods

        private IEnumerable<ArticleViewModel> SetViewModels(List<Article> articles)
        {
            if(articles != null && articles.Any())
            {
                List<ArticleViewModel> viewModels = new List<ArticleViewModel>();
                ArticleViewModel viewModel;
                foreach (Article article in articles)
                {
                    viewModel = new ArticleViewModel
                                    {
                                        Id = article.ArticleId,
                                        Title = article.Title,
                                        Body = article.Body,
                                        Deck = article.Deck,
                                        ArticleUrl = URL.GetArticleUrl(article.Title, article.ArticleId),
                                        Abstract = article.EntryContent.StripHtml().GetCharacters(250),
                                        ImageUrl = article.ImageUrl
                                    };
                    viewModels.Add(viewModel);
                }
                return viewModels;
            }
            return null;
        }

        #endregion
    }
}
