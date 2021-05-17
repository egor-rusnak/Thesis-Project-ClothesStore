using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Models.Clothes
{
    public class ClothesCategoryView
    {
        public string Category { get; set; }
        public string RusCategory { get; set; }
        public IEnumerable<ClothesType> Types { get; set; }
    }
}
