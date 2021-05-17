using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Services
{
    public class ClothesService:IClothesService
    {
        private readonly IAsyncRepository<ClothesType> _clothesTypes;
        private readonly IAsyncRepository<Clothes> _clothes;

        public ClothesService(IAsyncRepository<ClothesType> clothesTypes, IAsyncRepository<Clothes> clothes)
        {
            _clothesTypes = clothesTypes;
            _clothes = clothes;
        }

        private bool CheckClothesCategory(string category)
        {
            if (!Enum.GetNames<ClothesDestinantion>().Any(e => e == category))
                return false;
            else
                return true;
        }
        public async Task<IEnumerable<Clothes>> GetClothesByTypeAndCategory(string type,string category)
        {
            var types = await GetClothesTypesByCategory(category);
            var result = types.FirstOrDefault(e => e.Name == type);
            if (result == null) throw new ArgumentException("Bad type!");

            

            return await _clothes.GetBy(e=>e.ClothesType==result);
        }

        public async Task<IEnumerable<ClothesType>> GetClothesTypesByCategory(string category)
        {
            if (!CheckClothesCategory(category))
                throw new ArgumentException("Bad category!");

            var enumDestination = Enum.Parse<ClothesDestinantion>(category);
            var result = await _clothesTypes.GetBy(e => e.Destinantion == enumDestination);
            return result;
        }
    }
}
