using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IAsyncRepository<ClothesMark> _clothes;
        private Cart cart;
        public CartController(IAsyncRepository<ClothesMark> clothes, Cart cart)
        {
            _clothes = clothes;
            this.cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int id, string returnUrl)
        {
            var product = repository.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
