using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesStore.Domain.Entities
{
    public class ClothesOrder : TEntity
    {
        [Range(0, 1000)]
        [System.ComponentModel.DisplayName("Кількість в замовленні")]
        [Required]
        public int Count { get; set; }
        [Required]

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(10,2)")]
        [System.ComponentModel.DisplayName("Ціна за одиницю")]
        public decimal CostPerSingle { get; set; }





        [System.ComponentModel.DisplayName("Назва одягу")]
        public int ClothesUnitId { get; private set; }
        [System.ComponentModel.DisplayName("Код замовлення")]
        public int OrderId { get; private set; }

        public virtual ClothesMark ClothesUnit { get; set; }
        public virtual Order Order { get; set; }
    }
}