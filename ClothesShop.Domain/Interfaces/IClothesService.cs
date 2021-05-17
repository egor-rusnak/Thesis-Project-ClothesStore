using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Interfaces
{
    public interface IClothesService
    {
        Task<IEnumerable<ClothesType>> GetClothesTypesByCategory(string category);
        Task<IEnumerable<Clothes>> GetClothesByTypeAndCategory(string type, string category);
    }
}
