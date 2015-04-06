using System;
using System.Collections.Generic;
using WEI.Core;
using WEI.Domain.Interface;
using WEI.Domain.Model;
using WEI.Service.Interface;

namespace WEI.Service.Service
{
    public class MenuItemService : IMenuItemService
    {
        #region Instance Variables

        private readonly IMenuItemRepository _menuItemRepository;

        #endregion

        #region Constructors

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            if (menuItemRepository == null)
            {
                throw new ArgumentNullException("menuItemRepository");
            }
            _menuItemRepository = menuItemRepository;
        }

        #endregion

        #region IMenuItemService Members

        public List<MenuItem> GetAllMenuItems()
        {
            return _menuItemRepository.GetAll((MenuItem p) => p.MenuItemId, Enums.SortOrder.Asc);
        }

        #endregion
    }
}
