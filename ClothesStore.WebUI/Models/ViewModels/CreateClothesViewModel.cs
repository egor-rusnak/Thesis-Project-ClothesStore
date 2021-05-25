using ClothesStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class CreateClothesViewModel
    {
        public Clothes Entity { get; set; }
        public IEnumerable<ClothesMark> Marks { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public string ReturnUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}
