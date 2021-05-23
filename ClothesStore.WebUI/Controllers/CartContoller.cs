using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Models;
using ClothesStore.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IAsyncRepository<ClothesMark> _clothes;
        private readonly Cart cart;

        public CartController(IAsyncRepository<ClothesMark> clothes, Cart cart)
        {
            _clothes = clothes;
            this.cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public async Task<RedirectToActionResult> AddToCart(int sizeId, int clothesId, string returnUrl)
        {
            var product = (await _clothes.GetAll()).FirstOrDefault(p => p.SizeId == sizeId && p.ClothesId == clothesId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public async Task<RedirectToActionResult> RemoveFromCart(int sizeId, int clothesId, string returnUrl)
        {
            var product = (await _clothes.GetAll()).FirstOrDefault(p => p.SizeId == sizeId && p.ClothesId == clothesId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
