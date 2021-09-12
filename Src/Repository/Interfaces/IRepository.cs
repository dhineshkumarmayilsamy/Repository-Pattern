using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveById(int id);
        void RemoveRange(IEnumerable<TEntity> entities);

        IEnumerable<TEntity> FromSqlRaw(string query);
        int ExecuteSqlRaw(string query);
    }
}
