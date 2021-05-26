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
        private readonly IAsyncRepository<ClothesMark> _marks;
        private readonly IAsyncRepository<Size> _sizes;
        private readonly IWebHostEnvironment _enviroments;
        public ClothesController(IClothesService clothes, IWebHostEnvironment enviroments, IAsyncRepository<Clothes> clothesStore, IAsyncRepository<Brand> brands, IAsyncRepository<Size> sizes, IAsyncRepository<ClothesMark> marks)
        {
            _clothes = clothes;
            _enviroments = enviroments;
            _clothesStore = clothesStore;
            _brands = brands;
            _sizes = sizes;
            _marks = marks;
        }

        public async Task<IActionResult> AddMark(int id)
        {
            var clothes = await _clothesStore.GetById(id);
            if (clothes == null) return NotFound();
            var sizes = await _sizes.GetAll();

            if (sizes.Count()==0) return RedirectToAction("Error", "Home", new { message = "Немає розмірів!" });
            ViewBag.Sizes = sizes;

            var model = new MarkViewModel() 
            {
                Entity = new ClothesMark { ClothesId = id, CountInStock = 5 },
                ClothesName = clothes.Name, 
                ReturnUrl = "" 
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMark(MarkViewModel model)
        {
            if (ModelState.IsValid)
            {

                await _marks.Create(model.Entity);
                return RedirectToReturnUrlOrHome(model.ReturnUrl);
            }
            else
            {
                ViewBag.Sizes = await _sizes.GetAll();
                return View(model);
            }
        }
        private IActionResult RedirectToReturnUrlOrHome(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");
            else return Redirect(returnUrl);
        }
        [HttpGet]
        public async Task<IActionResult> EditMark(int id, string returnUrl)
        {
            var mark = await _marks.GetById(id);
            ViewBag.Sizes = await _sizes.GetAll();
            if (mark == null) return NotFound();

            return View(new MarkViewModel { ClothesName=mark.Clothes.Name, Entity = mark, ReturnUrl=returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> EditMark(MarkViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _marks.Update(model.Entity);
                return RedirectToReturnUrlOrHome(model.ReturnUrl);
            }
            ViewBag.Sizes = await _sizes.GetAll();
            return View(model); 
        }


        [HttpGet]
        public async Task<IActionResult> Index(string category)
        {
            try
            {
                var clothesTypes = await _clothes.GetClothesTypesByCategory(category);
                var viewModel = new CategoryViewModel();
                if (clothesTypes == null) return NotFound();
                viewModel.Category = category;
                viewModel.Types = clothesTypes;

                return View(viewModel);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Edit(int id,string returnUrl)
        {
            var clothes = await _clothesStore.GetById(id);
            if (clothes == null) return NotFound();
            ViewBag.Brands = await _brands.GetAll();
            var model = new EditViewModel<Clothes>()
            {
                Entity = clothes,
                ReturnUrl=returnUrl
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel<Clothes> model)
        {

            if (ModelState.IsValid)
            {
                ViewBag.Brands = await _brands.GetAll();
                await HttpContext.WriteImageClothes(_enviroments, model.Entity);

                await _clothesStore.Update(model.Entity);
                return RedirectToReturnUrlOrHome(model.ReturnUrl);
            }
            else return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string returnUrl)
        {
            var clothes = await _clothesStore.GetById(id);
            if (clothes == null) return NotFound();
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
            catch(ArgumentException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home", new { message = "Bad category or type" });
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
                await HttpContext.WriteImageClothes(_enviroments, clothesModel.Entity);

                await _clothesStore.Update(clothes);

                return RedirectToReturnUrlOrHome(clothesModel.ReturnUrl);
            }
            return View(clothesModel);
        }
        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> Delete(int id, string returnUrl)
        {
            var clothes =await _clothesStore.GetById(id);
            if (clothes == null) return NotFound();
            try
            {
                if (clothes.ImageName != null)
                    System.IO.File.Delete(Path.Combine("uploads//clothes", clothes.ImageName));
            }
            catch (Exception) { }
            await _clothesStore.Delete(id);
            return RedirectToReturnUrlOrHome(returnUrl);
        }

        private SelectList GetBrandsSelectList(IEnumerable<Brand> brands)
        {
            return new SelectList(brands, "Id", "Value");
        }
        private SelectList GetSizeSelectList(IEnumerable<Size> sizes)
        {
            return new SelectList(sizes, "Id", "Mark");
        }

        public async Task<IActionResult> ClothesMarks(int id)
        {
            var clothes = await _clothesStore.GetById(id);
            if (clothes == null) return NotFound();

            var model = new MarksListViewModel() { Clothes = clothes, Marks = clothes.ClothesMarksInStock };
            
            return View(model);
        }
    }
}
