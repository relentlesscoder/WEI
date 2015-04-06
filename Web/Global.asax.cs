using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Elmah;
using WEI.Domain.Interface;
using WEI.Web.Filters;
using WEI.Web.Windsor;

namespace WEI.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandleErrorAttribute());
            //filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elmah.axd");

            routes.MapRoute(
                "Item", // Route name
                "{controller}/{id}/{title}", // URL with parameters
                new { action = "Index", id = @"\d+" }
            );

            routes.MapRoute(
                "Action", // Route name
                "{controller}/{action}" // URL with parameters
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
        
        // Castle Windsor, convention-based
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(WindsorContainerFactory.Instance);

            // use windsor controller factory
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Configure Elmah to ignore 404 exception
        /// </summary>
        public void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            HttpException httpException = e.Exception as HttpException;
            if(httpException != null && httpException.GetHttpCode() == 404)
            {
                e.Dismiss();
            }
        }
    }
}