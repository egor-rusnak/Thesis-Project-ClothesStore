using ClothesStore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IClothesService
    {
        Task<IEnumerable<ClothesType>> GetClothesTypesByCategory(string category);
        Task<IEnumerable<Clothes>> GetClothesByTypeAndCategory(string type, string category);
        Task<IEnumerable<Clothes>> GetTopDiscountClothes(int count);
    }
}
