using System.Collections.Generic;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class ClothesListViewModel
    {
        public string CategoryName { get; set; }
        public string TypeName { get; set; }
        public IEnumerable<ClothesViewModel> Clothes { get; set; }
    }
}
