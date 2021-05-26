using ClothesStore.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class ClothesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public IEnumerable<SizeViewModel> Sizes { get; set; }
        public float PromoutionPercent { get; set; }
        public string Image { get; set; }
        public static ClothesViewModel CreateClothesView(Clothes clothes)
        {
            return new ClothesViewModel()
            {
                Id = clothes.Id,
                Name = clothes.Name,
                Description = clothes.Material + " " + clothes.Brand?.Name + " " + clothes.Name,
                Cost = clothes.Cost,
                Sizes = clothes.ClothesMarksInStock.Where(e=>e.CountInStock>0).Select(e => new SizeViewModel { SizeId = e.Size.Id, SizeMark = e.Size.Mark }),
                Image = clothes.ImageName,
                PromoutionPercent = clothes.PromoutionPercent
            };
        }
    }
}
