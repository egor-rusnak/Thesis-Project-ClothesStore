using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Models.Clothes;
using System.Linq;

namespace ClothesStore.WebUI.Controllers
{
    public class ClothesController : Controller
    {
        private readonly IClothesService _clothes;

        public ClothesController(IClothesService clothes)
        {
            _clothes = clothes;
        }

        [Route("{category}")]
        public async Task<IActionResult> Index(string category)
        {
            var clothesTypes = await _clothes.GetClothesTypesByCategory(category);
            var viewModel = new ClothesCategoryView();

            viewModel.Category = category;

            var rusCategory = "";
            switch (category)
            {
                case "Men": rusCategory = "Чоловіча"; break;
                case "Women": rusCategory = "Жіноча"; break;
                case "Children": rusCategory = "Діти"; break;
            }
            viewModel.RusCategory = rusCategory;
            viewModel.Types = clothesTypes;

            return View(viewModel);
        }

        [Route("{category}/{type}")]
        public async Task<IActionResult> ClothesByTypeAndCategory(string category,string type)
        {
            var clothes = await _clothes.GetClothesByTypeAndCategory(category,type);
            return View(new ClothesByTypeAndCategoryViewModel() 
            { 
                Clothes = clothes.Select(e => new ClothesViewModel() 
                { 
                    Namge = e.Name, 
                    CostDefault = "none", 
                    Sizes = "none" 
                }) 
            });
        }
    }
}
