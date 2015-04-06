using System.Configuration;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using WEI.Domain.Interface;
using WEI.Service.Interface;
using WEI.Service.Service;
using WEI.SqlDataAccess;
using WEI.SqlDataAccess.Repository;
using WEI.Web.Controllers;

namespace WEI.Web.Windsor
{
    public class WebWindsorInstaller : IWindsorInstaller 
    {
        public void Install(IWindsorContainer container, 
            IConfigurationStore store)
        {
            string connString = ConfigurationManager.ConnectionStrings["WeiCms"].ConnectionString;

            container.Register(
                Component.For<IDbFactory>().ImplementedBy<SqlDbFactory>().LifestyleTransient().DependsOn(new { connectionString = connString }));
            container.Register(
                Component.For<IUnitOfWork>().ImplementedBy<SqlUnitOfWork>().LifestyleTransient());

            #region Repositories
            container.Register(
                Component.For<IArticleRepository>().ImplementedBy<SqlArticleRepositoy>().LifestyleTransient());
            container.Register(
                Component.For<IMenuItemRepository>().ImplementedBy<SqlMenuItemRepository>().LifestyleTransient());
            #endregion

            #region Services
            container.Register(
                Component.For<IArticleService>().ImplementedBy<ArticleService>().LifestyleTransient());
            container.Register(
                Component.For<IMenuItemService>().ImplementedBy<MenuItemService>().LifestyleTransient());
            container.Register(
                Component.For<IAccountService>().ImplementedBy<AccountService>().LifestyleTransient());
            #endregion

            #region Controllers
            // Both Classes and Types are new in Windsor 3. Previous versions had just one type to do the job - AllTypes. 
            //In Windsor 3, usage of AllTypes is discouraged, because its name is misleading. 
            //While it suggests that it behaves like Types, truth is it's exactly the same as Classes, pre-filtering all types to just non-abstract classes. To avoid confusion, use one of the two new types. 
            container.Register(
                AllTypes.FromAssemblyContaining<HomeController>()
                .BasedOn<IController>()
                .LifestyleTransient());
            #endregion

            //container.Register(
            //    AllTypes.
            //    FromAssemblyContaining<SqlArticleRepositoy>()
            //    .Where(t => t.Name.StartsWith("Sql"))
            //    .WithService.FirstInterface()
            //    .LifestyleTransient()
            //    .Configure(c => c.DependsOn(new { connectionString = connString })));
        }
    }
}