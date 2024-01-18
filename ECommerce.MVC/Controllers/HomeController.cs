using ECommerce.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerce.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFruitService _service;

        public HomeController(IFruitService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var fruits = await _service.GetAllAsync();
            return View(fruits);
        }
    }
}
