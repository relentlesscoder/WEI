using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WEI.Core;
using WEI.Domain.Model;
using WEI.Service.Interface;
using WEI.Web.Filters;
using WEI.Web.Models;

namespace WEI.Web.Controllers
{
    public class ArticleController : Controller
    {
        #region Instance Variables

        private readonly IArticleService _articleService;

        #endregion

        #region Constructors

        public ArticleController(IArticleService articleService)
        {
            if(articleService == null)
            {
                throw new ArgumentNullException("articleService");
            }

            this._articleService = articleService;
        }

        #endregion

        #region Action Methods

        // GET: /Article/
        [ViewCounter]
        [ElmahHandleError]
        public ActionResult Index(int id)
        {
            if(id > 0)
            {
                ViewBag.ArticleId = id;
                return View();
            }

            return View("Error");
        }

        [ElmahHandleError]
        public ActionResult Category(int id)
        {
            if(id > 0)
            {

            }

            return View("Error");
        }

        [ChildActionOnly]
        [ElmahHandleError]
        public ActionResult Article(int id)
        {
            Article article = _articleService.GetArticleById(id);
            if(article != null)
            {
                ArticleViewModel viewModel = GetArticleViewModel(article);
                return PartialView("_Article", viewModel);
            }

            return View("Error");
        }

        [ElmahHandleError]
        public ActionResult MostRecent()
        {
            int articleCount = ConfigSetting.ArticleWidgetArticleNumber;
            List<Article> articles =
                _articleService.GetMostRecentArticles(articleCount);
            string htmlContent = GetArticleWidgetContentHtml(articles);
            return Content(htmlContent);
        }

        [ElmahHandleError]
        public ActionResult MostPopular()
        {
            int articleCount = ConfigSetting.ArticleWidgetArticleNumber;
            List<Article> articles = _articleService.GetMostPopularArticles(articleCount);
            string htmlContent = GetArticleWidgetContentHtml(articles);
            return Content(htmlContent);
        }

        #endregion

        #region Private Methods

        private string GetArticleWidgetContentHtml(List<Article> articles)
        {
            if(articles.Any())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<ul>");
                string articleLink;
                foreach (Article article in articles)
                {
                    articleLink = "<a href=\"" + URL.GetArticleUrl(article.Title, article.ArticleId) + "\" title=\"" + article.Title + "\">" + article.Title + "</a>";
                    sb.Append("<li>" + articleLink + "</li>");
                }
                sb.Append("</ul>");
                return sb.ToString();
            }

            return "Sorry, content temporarily unavailable!";
        }

        private ArticleViewModel GetArticleViewModel(Article article)
        {
            return new ArticleViewModel
                       {
                           Id = article.ArticleId,
                           Body = article.Body,
                           Deck = article.Deck,
                           Title = article.Title,
                           EntryContent = article.EntryContent,
                           PublishDate = article.PublishDate.ToString("MMMM dd, yyyy"),
                           ImageUrl = article.ImageUrl
                       };
        }

        #endregion
    }
}
