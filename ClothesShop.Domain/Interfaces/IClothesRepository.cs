using ClothesStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IClothesRepository
    {
        Task<IEnumerable<Clothes>> GetClothesWithSizesAndBrands();
    }
}
