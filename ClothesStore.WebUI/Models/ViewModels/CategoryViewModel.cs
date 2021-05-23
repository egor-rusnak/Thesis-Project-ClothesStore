using ClothesStore.Domain.Entities;
using System.Collections.Generic;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string Category { get; set; }
        public IEnumerable<ClothesType> Types { get; set; }
    }
}
