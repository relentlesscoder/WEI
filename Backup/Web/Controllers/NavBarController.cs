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
    public class NavBarController : Controller
    {
        #region Instance Variables

        private readonly IMenuItemService _menuItemService;

        #endregion

        #region Constructors

        public NavBarController(IMenuItemService menuItemService)
        {
            if(menuItemService == null)
            {
                throw new ArgumentNullException("menuItemService");
            }
            _menuItemService = menuItemService;
        }

        #endregion

        #region Action Methods

        [ChildActionOnly]
        [ElmahHandleError]
        public ActionResult MainNavBar()
        {
            List<MenuItem> menuItems = _menuItemService.GetAllMenuItems();
            IEnumerable<MenuItemViewModel> menuItemViewModels = GetMenuItemViewModels(menuItems);
            return PartialView("_NavBar", menuItemViewModels);
        }

        #endregion

        #region Private Methods

        IEnumerable<MenuItemViewModel> GetMenuItemViewModels(List<MenuItem> menuItems)
        {
            if (menuItems != null && menuItems.Any())
            {
                string domainUrl = String.IsNullOrEmpty(ConfigSetting.LocalDomainUrl)
                                       ? ConfigSetting.DomainUrl
                                       : ConfigSetting.LocalDomainUrl;
                List<MenuItemViewModel> viewModels = new List<MenuItemViewModel>();
                MenuItemViewModel viewModel;
                foreach (MenuItem menuItem in menuItems)
                {
                    viewModel = new MenuItemViewModel
                                    {
                                        Id = menuItem.MenuItemId,
                                        Name = menuItem.Name,
                                        ParentMenuItemId = menuItem.ParentMenuItemId,
                                        Position = menuItem.Position,
                                        Url =  domainUrl + menuItem.Url
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
