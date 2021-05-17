using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStore.Domain.Entities
{
    public class ClothesOrder
    {
        [Range(0,1000)]
        public int Count { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName ="decimal(10,2)")]
        public decimal CostPerSingle { get; set; }

        public int ClothesUnitId { get; private set; }
        public int OrderId { get; private set; }

        public virtual ClothesMark ClothesUnit { get; set; }
        public virtual Order Order { get; set; }
    }
}