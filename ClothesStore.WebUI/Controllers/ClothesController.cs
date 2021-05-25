using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Extensions;
using ClothesStore.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Controllers
{
    public class ClothesController : Controller
    {
        private readonly IClothesService _clothes;
        private readonly IAsyncRepository<Clothes> _clothesStore;
        private readonly IAsyncRepository<Brand> _brands;
        private readonly IWebHostEnvironment _enviroments;
        public ClothesController(IClothesService clothes, IWebHostEnvironment enviroments, IAsyncRepository<Clothes> clothesStore, IAsyncRepository<Brand> brands)
        {
            _clothes = clothes;
            _enviroments = enviroments;
            _clothesStore = clothesStore;
            _brands = brands;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string category)
        {
            var clothesTypes = await _clothes.GetClothesTypesByCategory(category);
            var viewModel = new CategoryViewModel();

            viewModel.Category = category;
            viewModel.Types = clothesTypes;

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var clothes = await _clothesStore.GetById(id);
            if (clothes == null) return NotFound();

            var model = new EditViewModel<Clothes>()
            {
                Entity=clothes,
               //TODO ADD A FILE TO EDIT MODE NOT EXACTLY LIKE THAT
                Image=System.IO.File. Path.Combine(_enviroments.WebRootPath,"uploads//clothes",clothes.ImageName)
            }


        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string returnUrl)
        {
            var clothes = await _clothesStore.GetById(id);
            var viewModel = new EditViewModel<ClothesViewModel>
            {
                ReturnUrl = returnUrl,
                Entity = ClothesViewModel.CreateClothesView(clothes)
            };
            ViewBag.Sizes = viewModel.Entity.Sizes;
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> ClothesList(string category, string type)
        {
            try
            {
                var clothes = await _clothes.GetClothesByTypeAndCategory(type, category);

                return View(new ClothesListViewModel()
                {
                    CategoryName = category,
                    TypeName = type,
                    Clothes = clothes.Select(e => ClothesViewModel.CreateClothesView(e)),
                    Brands = clothes.Select(e => new BrandsCheck { BrandId = e.Brand.Id, BrandName = e.Brand.Name, Checked = true })
                    .GroupBy(e => e.BrandId).Select(e => e.First()).ToList()
                }) ;
            }
            catch (ArgumentException)
            {
                return View("Error", new Models.ErrorViewModel() { RequestId = "ERROROR" });
            }
        }

        [Authorize(Policy = "Manager")]
        [HttpGet]
        public async Task<IActionResult> Create(string type, string category, string returnUrl)
        {
            var brands = await _brands.GetAll();
            if (!brands.Any()) return RedirectToAction("Error", "Home", new { message = "No brands!" });
            var clothes = new Clothes();
            var clothesTypes = await _clothes.GetClothesTypesByCategory(category);
            clothes.ClothesTypeId = clothesTypes.FirstOrDefault(e => e.Name == type).Id;
            var model = new CreateClothesViewModel() { Entity = clothes, ReturnUrl = returnUrl, Brands=brands };
            return View(model);
        }

        [Authorize(Policy = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateClothesViewModel clothesModel)
        {
            if (ModelState.IsValid)
            {
                var clothes = await _clothesStore.Create(clothesModel.Entity);
                await HttpContext.WriteImageClothes(_enviroments, clothesModel);

                await _clothesStore.Update(clothes);

                return Redirect(clothesModel.ReturnUrl);
            }
            return View(clothesModel);
        }
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Delete(int id, string returnUrl)
        {
            var clothes =await _clothesStore.GetById(id);
            try
            {
                if (clothes.ImageName != null)
                    System.IO.File.Delete(Path.Combine("uploads//clothes", clothes.ImageName));
            }
            catch (Exception) { }
            await _clothesStore.Delete(id);
            return Redirect(returnUrl);
        }

        private SelectList GetBrandsSelectList(IEnumerable<Brand> brands)
        {
            return new SelectList(brands, "Id", "Value");
        }
        private SelectList GetSizeSelectList(IEnumerable<Size> sizes)
        {
            return new SelectList(sizes, "Id", "Mark");
        }
    }
}
