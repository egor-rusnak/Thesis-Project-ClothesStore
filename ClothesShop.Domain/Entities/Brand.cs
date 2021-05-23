namespace ClothesStore.Domain.Entities
{
    public class Brand : TEntity
    {
        [System.ComponentModel.DisplayName("Назва бренду")]
        public string Name { get; set; }
    }
}
