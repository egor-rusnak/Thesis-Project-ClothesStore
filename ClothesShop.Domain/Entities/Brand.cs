using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    [System.ComponentModel.DisplayName("Бренд")]
    public class Brand : TEntity
    {
        [System.ComponentModel.DisplayName("Назва бренду")]
        [Required]
        public string Name { get; set; }
    }
}
