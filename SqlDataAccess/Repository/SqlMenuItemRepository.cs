using WEI.Domain.Interface;
using WEI.Domain.Model;

namespace WEI.SqlDataAccess.Repository
{
    public class SqlMenuItemRepository : SqlEntityRepositoy<MenuItem>, IMenuItemRepository
    {
        public SqlMenuItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
