using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Services
{
    public class ClothesService : IClothesService
    {
        private readonly IAsyncRepository<ClothesType> _clothesTypes;
        private readonly IAsyncRepository<ClothesMark> _marks;
        private readonly IAsyncRepository<Size> _sizes;
        private readonly IAsyncRepository<Clothes> _clothes;

        public ClothesService(IAsyncRepository<ClothesType> clothesTypes, IAsyncRepository<Clothes> clothes, IAsyncRepository<ClothesMark> marks, IAsyncRepository<Size> sizes)
        {
            _clothesTypes = clothesTypes;
            _clothes = clothes;
            _marks = marks;
            _sizes = sizes;
        }

        private bool CheckClothesCategory(string category)
        {
            if (!Enum.GetNames<ClothesDestinantion>().Any(e => e == category))
                return false;
            else
                return true;
        }

        public async Task<IEnumerable<Clothes>> GetClothesByTypeAndCategory(string type, string category)
        {
            var types = await GetClothesTypesByCategory(category);
            var result = types.FirstOrDefault(e => e.Name == type);
            if (result == null) throw new ArgumentException("Bad type!");


            var clothes = (await _clothes.GetBy(e => e.ClothesType == result)).ToList();
            return clothes ?? new List<Clothes>();
        }

        public async Task<IEnumerable<ClothesType>> GetClothesTypesByCategory(string category)
        {
            if (!CheckClothesCategory(category))
                throw new ArgumentException("Bad category!");

            var enumDestination = Enum.Parse<ClothesDestinantion>(category);
            var result = await _clothesTypes.GetBy(e => e.Destinantion == enumDestination);
            return result;
        }
        public async Task<IEnumerable<Clothes>> GetTopDiscountClothes(int count)
        {
            var clothes = await _clothes.GetAll();
            var orderedClothes = clothes.OrderByDescending(e => e.PromoutionPercent);
            return orderedClothes?.Take(count).ToList();
        }

        public async Task AddUnitsToClothes(int sizeId, int clothesId, int count)
        {
            var clothes = await _clothes.GetById(clothesId);
            if (clothes != null)
            {
                var mark = (await _marks.GetBy(e => e.ClothesId == clothesId && e.SizeId == sizeId)).FirstOrDefault();
                var size = (await _sizes.GetById(sizeId));
                if (size == null) return;

                if (mark == null)
                    await _marks.Create(new ClothesMark { Clothes = clothes, CountInStock = count, Size = size });
                else
                {
                    mark.CountInStock += count;
                    await _marks.Update(mark);
                }
            }
        }
    }
}
