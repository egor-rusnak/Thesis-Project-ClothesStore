using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    public enum ClothesDestinantion
    {
        Чоловіки,
        Жінки,
        Діти
    }
    public class ClothesType : TEntity
    {
        [StringLength(100)]
        [System.ComponentModel.DisplayName("Назва типу")]
        public string Name { get; set; }
        [System.ComponentModel.DisplayName("Категорія")]
        public ClothesDestinantion Destinantion { get; set; }

        [System.ComponentModel.DisplayName("Рисунок для категорії")]
        public string ImageName { get; set; }
        public virtual IEnumerable<Clothes> Clothes { get; set; }
    }
}
