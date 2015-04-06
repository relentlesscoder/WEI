using System;
using WEI.Domain.Interface;

namespace WEI.SqlDataAccess
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly IDbFactory _dbFactory;
        private readonly BlogContext _blogContext;

        public SqlUnitOfWork(IDbFactory dbFactory)
        {
            if(dbFactory == null)
            {
                throw new ArgumentNullException("dbFactory");
            }
            _dbFactory = dbFactory;
            _blogContext = (BlogContext) _dbFactory.GetDbContext();
        }

        public object DataContext
        {
            get { return _blogContext ?? ((BlogContext)_dbFactory.GetDbContext()); }
        }

        public void Commit()
        {
            _blogContext.Commit();
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _blogContext.Dispose();
                }
            }
            this._disposed = true;
        }
        #endregion
    }
}
