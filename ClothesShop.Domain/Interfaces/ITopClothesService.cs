using ClothesStore.Domain.Entities;
using System.Collections.Generic;

namespace ClothesStore.Domain.Interfaces
{
    public interface ITopClothesService
    {
        IEnumerable<Clothes> GetPopularByLastMonth(int count);

    }
}
