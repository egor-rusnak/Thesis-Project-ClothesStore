using System.Collections.Generic;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ClothesViewModel> Carousel { get; set; }
        public IEnumerable<ClothesViewModel> DiscountProducts { get; set; }
    }
}
