using System.ComponentModel.DataAnnotations;

namespace ClothesStore.Domain.Entities
{
    public class TEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
