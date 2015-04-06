using System.Collections.Generic;
using WEI.Domain.Model;

namespace WEI.Service.Interface
{
    public interface IMenuItemService
    {
        List<MenuItem> GetAllMenuItems();
    }
}
