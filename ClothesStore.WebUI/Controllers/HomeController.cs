using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Models;
using ClothesStore.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITopClothesService _topService;
        private readonly IClothesService _clothes;

        public HomeController(ILogger<HomeController> logger, ITopClothesService topService, IClothesService clothes)
        {
            _logger = logger;
            _topService = topService;
            _clothes = clothes;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel()
            {
                Carousel = _topService.GetPopularByLastMonth(5).Select(e => ClothesViewModel.CreateClothesView(e)),
                DiscountProducts = (await _clothes.GetTopDiscountClothes(5)).Select(e => ClothesViewModel.CreateClothesView(e))
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage=message });
        }
    }
}
