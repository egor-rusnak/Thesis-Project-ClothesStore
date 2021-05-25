using ClothesStore.Domain.Entities;
using System.Collections.Generic;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class ClothesListViewModel
    {
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public IEnumerable<ClothesViewModel> Clothes { get; set; }
        public IEnumerable<BrandsCheck> Brands { get; set; }
    }
    public class BrandsCheck
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool Checked { get;set; }
    }
}
