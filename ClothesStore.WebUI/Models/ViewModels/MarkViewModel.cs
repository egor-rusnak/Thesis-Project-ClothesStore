using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class MarkViewModel
    {
        public ClothesMark Entity { get; set; }
        public string ClothesName { get; set; }
        public string ReturnUrl { get; set; }
    }
}
