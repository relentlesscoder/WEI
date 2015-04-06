using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WEI.Core;

namespace WEI.Domain.Interface
{
    public interface IEntityRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Delete(Func<T, Boolean> predicate);
        T GetById(int Id);
        T FindOne(Func<T, Boolean> where);
        List<T> GetAll<T, TKey>(Expression<Func<T, TKey>> keySelector, Enums.SortOrder sortOrder);
        List<T> Find<T, TKey>(Expression<Func<T, TKey>> keySelector, Enums.SortOrder sortOrder,
                              Expression<Func<T, bool>> where = null, int count = -1);
    }
}
