using System;
using System.Net;
using System.Web.Mvc;

namespace WEI.Web.Filters
{
    /// <summary>
    /// Create custom authorize attribute handle authenticated but unauthorized request
    /// Ref. http://stackoverflow.com/questions/238437/why-does-authorizeattribute-redirect-to-the-login-page-for-authentication-and-au
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if(filterContext.HttpContext.Request.IsAuthenticated)
            {
                // return HTTP 403 forbidden code for authenticated but unauthorized request
                filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);  
            }
        }
    }
}