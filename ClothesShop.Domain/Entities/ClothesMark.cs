using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesStore.Domain.Entities
{
    public class ClothesMark
    {
        [Range(0, 1000)]
        public int CountInStock { get; set; }
        public int SizeId { get; private set; }
        public int ClothesId { get; private set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Cost { get; set; }

        public virtual Clothes Clothes { get; set; }
        public virtual Size Size { get; set; }
    }
}
