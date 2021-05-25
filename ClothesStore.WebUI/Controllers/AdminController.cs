using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;

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
                nameof(Sizes),
                nameof(Register)
            });
        }

        [DisplayName("Зареєструватися")]
        public IActionResult Register() { return RedirectToAction("Register", "Account"); }
        [DisplayName("Бренди")]
        public IActionResult Brands() { return RedirectToAction("Index", "Brands"); }
        [DisplayName("Розміри")]
        public IActionResult Sizes() { return RedirectToAction("Index", "Sizes"); }
    }
}
