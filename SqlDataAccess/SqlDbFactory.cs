using System;
using WEI.Domain.Interface;

namespace WEI.SqlDataAccess
{
    public class SqlDbFactory : IDbFactory
    {
        private readonly BlogContext _blogContext;

        public SqlDbFactory(string connectionString)
        {
            if(String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            _blogContext = new BlogContext(connectionString);
        }

        public object GetDbContext()
        {
            return _blogContext;
        }
    }
}
