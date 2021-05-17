using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Infrastructure.Services
{
    public class EfRepository<T> : IAsyncRepository<T> where T:TEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _set;
        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            var result = await _set.AddAsync(entity);
            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public async Task Delete(int id)
        {
            var entity = _set.FirstOrDefault(e => e.Id == id);
            _set.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> predicate)
        {
            var elems = await _set.Where(predicate).ToListAsync();

            return elems;
        }

        public async Task<T> GetById(int id)
        {
            return await _set.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> Update(T entity)
        {
            var resEntity = _context.Update(entity);
            await _context.SaveChangesAsync();

            return resEntity.Entity;
        }
    }
}
