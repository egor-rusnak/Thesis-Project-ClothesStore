using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Entities
{
    public class Clothes:TEntity
    {
        [StringLength(500)]
        public string Name { get; set; }
        public Brand Brand { get; set; }
        [Range(0,100)]
        [DisplayFormat(DataFormatString ="#.## %")]
        public float PromoutionPercent { get; set; }
        [StringLength(100)]
        public string Material { get; set; }
        public IEnumerable<ClothesMark> ClothesMarksInStock { get; set; }
        public ClothesType ClothesType { get; set; }
    }
}
