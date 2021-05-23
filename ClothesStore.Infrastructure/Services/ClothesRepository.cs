using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothesStore.Infrastructure.Services
{
    public class ClothesRepository : IClothesRepository
    {
        private readonly ApplicationDbContext _context;

        public ClothesRepository(ApplicationDbContext context)
        {
            _context = context;
            _set = _context.Set<Clothes>();
        }

        private readonly DbSet<Clothes> _set;

        public async Task<IEnumerable<Clothes>> GetClothesWithSizesAndBrands()
        {
            var result = _set.Include(e => e.Brand).Include(e => e.ClothesMarksInStock);
            return result;
        }
    }
}
