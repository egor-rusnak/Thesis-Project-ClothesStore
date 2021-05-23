using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IAsyncRepository<Order> _orders;
        private Cart cart;

        public OrderController(IAsyncRepository<Order> orders, Cart cart)
        {
            this._orders = orders;
            this.cart = cart;
        }
        [Authorize(Policy = "Manager")]
        public async Task<ViewResult> List() => View(await _orders.GetAll());
        [HttpPost]

        [Authorize(Policy = "Manager")]
        public async Task<IActionResult> MarkShipped(int id)
        {
            var order = (await _orders.GetAll()).FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.Shiped = true;
                await _orders.Update(order);
            }
            return RedirectToAction(nameof(List));
        }

        public IActionResult Checkout()
        {
            if (cart.Lines.Count() > 0)
                return View(new Order());
            else
                return RedirectToAction("Index", "Cart");
        }

        [HttpPost]

        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                order.ClothesOrders = cart.Lines.ToArray();
                _orders.Update(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            cart.Clear();
            return View();
        }
    }
}
