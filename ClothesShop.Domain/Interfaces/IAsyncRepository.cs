using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IAsyncRepository<T> where T:class
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> predicate);
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
