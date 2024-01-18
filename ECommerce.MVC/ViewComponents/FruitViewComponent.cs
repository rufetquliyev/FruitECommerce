using ECommerce.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.ViewComponents
{
    public class FruitViewComponent : ViewComponent
    {
        private readonly IFruitService _service;

        public FruitViewComponent(IFruitService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var fruits = await _service.GetAllAsync();
            return View(fruits);
        }
    }
}
