using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using WEI.Core;
using WEI.Domain.Interface;
using System.Linq;

namespace WEI.SqlDataAccess.Repository
{
    public class SqlEntityRepositoy<T> : IEntityRepository<T> where T : class
    {
        private readonly BlogContext _blogContext;
        private readonly IDbSet<T> _dbSet;
        private readonly IUnitOfWork _unitOfWork;

        #region Constructor

        protected SqlEntityRepositoy(IUnitOfWork unitOfWork)
        {
            if(unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;

            _blogContext = (BlogContext)_unitOfWork.DataContext;
            _dbSet = _blogContext.Set<T>();
        }

        #endregion

        #region IEntityRepository<T> Members

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _blogContext.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if(_blogContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
            SaveChanges();
        }

        public virtual void Delete(int id)
        {
            T entity = _dbSet.Find(id);
            if(entity == null)
            {
                throw new ArgumentNullException("id");
            }
            Delete(entity);
            SaveChanges();
        }

        public virtual void Delete(Func<T, Boolean> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                if (_blogContext.Entry(obj).State == EntityState.Detached)
                {
                    _dbSet.Attach(obj);
                }
                _dbSet.Remove(obj);
            }
            SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual T FindOne(Func<T, Boolean> where)
        {
            return _dbSet.Where<T>(where).DefaultIfEmpty(null).FirstOrDefault<T>();
        }

        public virtual List<T> GetAll<T, TKey>(Expression<Func<T, TKey>> keySelector, 
            Enums.SortOrder sortOrder)
        {
            IOrderedQueryable<T> orderedEntityList;
            switch (sortOrder)
            {
                case Enums.SortOrder.Desc:
                    orderedEntityList = ((IQueryable<T>)_dbSet).OrderByDescending(keySelector);
                    break;
                default:
                    orderedEntityList = ((IQueryable<T>)_dbSet).OrderBy(keySelector);
                    break;
            }
            return orderedEntityList.ToList();
        }

        public virtual List<T> Find<T, TKey>(Expression<Func<T, TKey>> keySelector,
            Enums.SortOrder sortOrder, Expression<Func<T, bool>> where = null, 
            int count = -1)
        {
            IQueryable<T> entityList;
            IOrderedQueryable<T> orderedEntityList;
            if(where != null)
            {
                entityList = ((IQueryable<T>)_dbSet).Where(where);
            }
            else
            {
                entityList = (IQueryable<T>) _dbSet;
            }
            if (count > 0)
                entityList = entityList.Take(count);
            switch (sortOrder)
            {
                case Enums.SortOrder.Desc:
                    orderedEntityList = entityList.OrderByDescending(keySelector);
                    break;
                default:
                    orderedEntityList = entityList.OrderBy(keySelector);
                    break;
            }
            
            return orderedEntityList.ToList();
        }

        private void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        #endregion
    }
}
