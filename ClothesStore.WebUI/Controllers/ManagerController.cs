using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ClothesStore.WebUI.Controllers
{
    [Authorize(Policy = "Manager")]
    public class ManagerController : Controller
    {
        public ManagerController()
        { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FinishOrder(int id)
        {
            throw new NotImplementedException();
        }
        public IActionResult OrderDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}
