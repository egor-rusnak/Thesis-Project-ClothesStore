using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Controllers
{
    public class ClothesController : Controller
    {
        private readonly IClothesService _clothes;
        private readonly IAsyncRepository<Clothes> _clothesStore;
        private readonly IWebHostEnvironment _enviroments;
        public ClothesController(IClothesService clothes, IWebHostEnvironment enviroments, IAsyncRepository<Clothes> clothesStore)
        {
            _clothes = clothes;
            _enviroments = enviroments;
            _clothesStore = clothesStore;
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
                    Brands =clothes.Select(e => e.Brand!=null?new BrandsCheck { BrandId = e.Brand.Id, BrandName = e.Brand.Name, Checked = true }:null).Distinct()
                });
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
            var clothes = new Clothes();
            var clothesTypes = await _clothes.GetClothesTypesByCategory(category);
            clothes.ClothesTypeId = clothesTypes.FirstOrDefault(e => e.Name == type).Id;
            var model = new CreateViewModel<Clothes>() { Entity = clothes, ReturnUrl = returnUrl };
            return View(model);
        }

        [Authorize(Policy = "Manager")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateClothesViewModel clothesModel)
        {
            if (ModelState.IsValid)
            {
                
                var clothes = await _clothesStore.Create(clothesModel.Entity);
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_enviroments.WebRootPath, "uploads\\clothes");
                        if (file.Length > 0)
                        {
                            var fileName = clothes.Id + "_" + clothes.Name + Path.GetExtension(file.FileName);
                            Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                clothes.ImageName = fileName;
                            }
                        }
                    }
                }

                await _clothesStore.Update(clothes);

                return Redirect(clothesModel.ReturnUrl);
            }
            return View(clothesModel);
        }
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Delete(int id, string returnUrl)
        {
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
