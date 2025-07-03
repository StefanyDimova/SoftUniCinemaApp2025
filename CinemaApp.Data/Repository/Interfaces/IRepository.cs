using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Data.Repository.Interfaces
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity? GetById(TKey id);
        TEntity? SingleOrDefault(Func<TEntity, bool> predicate);
        TEntity? FirstOrDefault(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAllAttached();
        int Count();
        void Add(TEntity item);
        void AddRange(IEnumerable<TEntity> items);
        bool Delete(TEntity entity);
        bool HardDelete(TEntity entity);
        bool Update(TEntity item);
        void SaveChanges();


    }
}
