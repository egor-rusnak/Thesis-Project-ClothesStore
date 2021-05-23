using ClothesStore.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T : TEntity
    {
        Task<T> GetById(int id);
        Task<IQueryable<T>> GetAll();

        Task<IQueryable<T>> GetBy(Expression<Func<T, bool>> predicate);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
