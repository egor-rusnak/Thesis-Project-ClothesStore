using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    public class ClothesMark : TEntity
    {
        [Range(0, 1000)]
        [System.ComponentModel.DisplayName("Кількість на складі")]
        public int CountInStock { get; set; } = 0;
        [System.ComponentModel.DisplayName("Розмір")]
        public int SizeId { get; set; }
        [System.ComponentModel.DisplayName("Одяг")]
        public int ClothesId { get; set; }
        public virtual Clothes Clothes { get; set; }
        public virtual Size Size { get; set; }
    }
}
