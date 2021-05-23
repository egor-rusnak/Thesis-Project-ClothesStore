using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ClothesStore.WebUI.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<string>
            {
                nameof(Brands),
                nameof(Clothes),
                nameof(ClothesMarks),
                nameof(ClothesOrders),
                nameof(ClothesTypes),
                nameof(Orders),
                nameof(Sizes)
            });
        }


        public IActionResult Brands() { return View(); }
        public IActionResult Clothes() { return View(); }
        public IActionResult ClothesMarks() { return View(); }
        public IActionResult ClothesOrders() { return View(); }
        public IActionResult ClothesTypes() { return View(); }
        public IActionResult Orders() { return View(); }
        public IActionResult Sizes() { return View(); }
    }
}
