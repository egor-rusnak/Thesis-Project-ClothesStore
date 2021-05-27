using ClothesStore.WebUI.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using ClothesStore.WebUI.Extensions;
using System.Threading.Tasks;
using ClothesStore.WebUI.Models.ViewModels;
using ClothesStore.Domain.Interfaces;

namespace ClothesStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly Services.IdentityService _service;
        private readonly IOrderService _orderSerivce;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, Services.IdentityService service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.CheckManagerPrivilegies())
            {
                var manager = await _userManager.GetUserAsync(HttpContext.User);
                if (manager == null || manager.IdForExternalDb == 0) return NotFound();
                return View("ManagerIndex", new ManagerIndexViewModel()
                {
                    Orders = await _orderSerivce.GetOrdersWithManagerIdOrWithoutManager(manager.IdForExternalDb)
                });
            }
            else if (HttpContext.CheckFullPrivilegies())
                return RedirectToAction("Index", "Account");
            else
                return View("UserIndex", new UserViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.RegisterUser(model, false, this.HttpContext);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.LogIn(model);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
