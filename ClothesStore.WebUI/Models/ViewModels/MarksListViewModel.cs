using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class MarksListViewModel
    {
        public Clothes Clothes { get; set; }
        public IEnumerable<ClothesMark> Marks { get; set; }
    }
}
