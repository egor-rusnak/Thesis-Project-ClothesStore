using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    [System.ComponentModel.DisplayName("Розмір")]
    public class Size : TEntity
    {
        [System.ComponentModel.DisplayName("Ідентифікатор розміру")]
        [Required]
        public string Mark { get; set; }
    }
}
