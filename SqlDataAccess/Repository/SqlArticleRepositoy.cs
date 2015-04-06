using WEI.Domain.Interface;
using WEI.Domain.Model;

namespace WEI.SqlDataAccess.Repository
{
    public class SqlArticleRepositoy : SqlEntityRepositoy<Article>, IArticleRepository
    {
        public SqlArticleRepositoy(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
