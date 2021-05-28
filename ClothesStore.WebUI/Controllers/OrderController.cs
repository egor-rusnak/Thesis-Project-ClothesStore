using ClothesStore.Domain.Entities;
using ClothesStore.Domain.Interfaces;
using ClothesStore.WebUI.Models;
using ClothesStore.WebUI.Models.Identity;
using ClothesStore.WebUI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClothesStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IAsyncRepository<Order> _orders;
        private IOrderService _orderService;
        private readonly IAsyncRepository<Manager> _managers;
        private readonly IAsyncRepository<Client> _clients;
        private readonly UserManager<User> _userManager;
        private Cart cart;

        public OrderController(IAsyncRepository<Order> orders, Cart cart, IOrderService orderService, IAsyncRepository<Client> clients, UserManager<User> userManager, IAsyncRepository<Manager> managers)
        {
            this._orders = orders;
            this.cart = cart;
            _orderService = orderService;
            _clients = clients;
            _userManager = userManager;
            _managers = managers;
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
            return RedirectToAction("Index","Account");
        }
        [HttpPost]
        public async Task<IActionResult> MarkProcessed(int id)
        {
            var order = await _orders.GetById(id);
            if (order == null) return NotFound();

            if (order.Manager != null) return RedirectToAction("Index", "Account");
            else
            {
                order.Manager = await _managers.GetById((await _userManager.GetUserAsync(HttpContext.User)).IdManager);
               
                if (order.Manager == null) 
                    return RedirectToAction("Error", "Home", new { message = "Помилка обробки запиту, не вдалося перевірити доступ менеджера!" });

                await _orders.Update(order);
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> MarkCanceled(int id)
        {
            var order = await _orders.GetById(id);
            if (order == null || order.Canceled==true) return NotFound();
            await _orderService.CancelOrder(id);

            return RedirectToAction("Index", "Account");
           
        }
        public async Task<IActionResult> Checkout()
        {
            if (cart.Lines.Count() > 0)
            {
                var checkedList = await _orderService.UnOrderableMarks(cart.Lines.ToArray());
                if (checkedList.Any())
                {
                    return View("Error", new OrderErrorViewModel()
                    {
                        ErrorMessage = "Не має наступних речей у цій кількості!",
                        MarksThatIsNotInStock = await _orderService.UnOrderableMarks(cart.Lines.ToArray())
                    });
                }
                else
                {
                    var order = new Order();
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);

                    if (currentUser != null && currentUser.IdClient > 0 && !HttpContext.User.HasClaim("access", Role.Manager.ToString()))
                    {
                        order.Client = await _clients.GetById(currentUser.IdClient);
                    }
                    else 
                        order.Client = await _clients.Create(new Client());

                    return View(order);
                }
                    
            }
            else
                return RedirectToAction("Index", "Cart");
        }

        [HttpPost]

        public async  Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _orderService.AddOrder(order, cart.Lines.ToArray());
                }
                catch (Exception ex)
                {
                    return View("Error", new OrderErrorViewModel()
                    {
                        ErrorMessage = "Не має наступних речей у цій кількості!",
                        MarksThatIsNotInStock = await _orderService.UnOrderableMarks(cart.Lines.ToArray())
                    });
                }
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
