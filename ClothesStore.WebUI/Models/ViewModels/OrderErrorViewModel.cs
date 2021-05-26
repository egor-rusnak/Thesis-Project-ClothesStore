using ClothesStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Models.ViewModels
{
    public class OrderErrorViewModel
    {
        public string ErrorMessage { get; set; }
        public IEnumerable<ClothesMark> MarksThatIsNotInStock { get; set; }
    }
}
