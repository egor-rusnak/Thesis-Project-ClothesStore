using ClothesStore.Domain.Entities;
using System.Collections.Generic;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class ManagerIndexViewModel
    {
        public IEnumerable<Order> Orders { get; set; }

    }
}
