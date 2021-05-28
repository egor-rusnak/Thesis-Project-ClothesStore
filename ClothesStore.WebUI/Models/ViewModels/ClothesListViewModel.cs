using System.Collections.Generic;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public enum SortModeView
    {
        NoSort,
        CostAsc,
        CostDesc
    }
    public class ClothesListViewModel
    {

        public PageViewModel PageModel { get; set; }
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public decimal StartCost { get; set; }
        public decimal EndCost { get; set; }
        public SortModeView Sort { get; set; } = SortModeView.NoSort;
        public IEnumerable<ClothesViewModel> Clothes { get; set; }
        public IEnumerable<BrandsCheck> Brands { get; set; }
    }
    public class BrandsCheck
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool Checked { get; set; }
    }
}
