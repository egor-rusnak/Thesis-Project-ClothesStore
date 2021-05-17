using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Models.Clothes
{
    public class ClothesByTypeAndCategoryViewModel
    {
        public string CategoryName { get; set; }
        public IEnumerable<ClothesViewModel> Clothes { get; set; }
    }
}
