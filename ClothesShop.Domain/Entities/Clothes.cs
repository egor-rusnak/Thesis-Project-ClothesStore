using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    public class Clothes : TEntity
    {
        [Reqired]
        [StringLength(500)]
        [System.ComponentModel.DisplayName("Назва одягу")]
        public string Name { get; set; }
        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }


        [Range(0, 100)]
        [DisplayFormat(DataFormatString = "#.## %")]
        [System.ComponentModel.DisplayName("Акційний процент")]
        public float PromoutionPercent { get; set; } = 0;
        [StringLength(100)]
        [System.ComponentModel.DisplayName("Перелік матеріалів")]
        [Required]
        public string Material { get; set; }
        public virtual IEnumerable<ClothesMark> ClothesMarksInStock { get; set; }
        public virtual ClothesType ClothesType { get; set; }
        public int ClothesTypeId { get; set; }
        [System.ComponentModel.DisplayName("Ціна за одяг")]
        [Required]
        public decimal Cost { get; set; }
        [System.ComponentModel.DisplayName("Зображення")]
        [Required]
        public string ImageName { get; set; }

    }
}
