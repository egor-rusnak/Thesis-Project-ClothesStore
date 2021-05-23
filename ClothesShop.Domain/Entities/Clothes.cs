using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    public class Clothes : TEntity
    {
        [StringLength(500)]
        [System.ComponentModel.DisplayName("Назва одягу")]
        public string Name { get; set; }
        public virtual Brand Brand { get; set; }
        [Range(0, 100)]
        [DisplayFormat(DataFormatString = "#.## %")]
        [System.ComponentModel.DisplayName("Акційний процент")]
        public float PromoutionPercent { get; set; }
        [StringLength(100)]
        [System.ComponentModel.DisplayName("Перелік матеріалів")]
        public string Material { get; set; }
        public virtual IEnumerable<ClothesMark> ClothesMarksInStock { get; set; }
        public virtual ClothesType ClothesType { get; set; }
        public int ClothesTypeId { get; set; }
        [System.ComponentModel.DisplayName("Ціна за одяг")]
        public decimal Cost { get; set; }
        [System.ComponentModel.DisplayName("Зображення")]
        public string ImageName { get; set; }

    }
}
