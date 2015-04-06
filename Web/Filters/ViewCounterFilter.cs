using System.Web.Mvc;
using WEI.Service.Interface;
using WEI.Web.Windsor;

namespace WEI.Web.Filters
{
    public class ViewCounterAttribute : FilterAttribute, IActionFilter
    {
        private readonly IArticleService _articleService;

        public ViewCounterAttribute()
        {
            _articleService = WindsorContainerFactory.Instance.Resolve<IArticleService>();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int articleId;
            int.TryParse(filterContext.RouteData.Values["id"].ToString(), out articleId);

            if (articleId > 0)
            {
                _articleService.IncrementArticleCount(articleId);
            }
        }
    }
}