using AutoMapper;
using ECommerce.Business.Exceptions.Common;
using ECommerce.Business.Exceptions.Fruit;
using ECommerce.Business.Services.Interfaces;
using ECommerce.Business.ViewModels.FruitVMs;
using ECommerce.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class FruitController : Controller
    {
        private readonly IFruitService _service;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public FruitController(IFruitService service, IMapper mapper, IWebHostEnvironment env)
        {
            _service = service;
            _mapper = mapper;
            _env = env;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var fruits = await _service.GetAllAsync();
            return View(fruits);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateFruitVm vm)
        {
            CreateFruitValidator validator = new CreateFruitValidator();
            var res = await validator.ValidateAsync(vm);
            if (res != null)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    return View(vm);
                }
            }
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            await _service.CreateAsync(vm, _env.WebRootPath);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var fruit = await _service.GetByIdAsync(id);
                UpdateFruitVm vm = _mapper.Map<UpdateFruitVm>(fruit);
                return View(vm);
            }
            catch (NegativeIdException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
            }
            catch (FruitNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateFruitVm vm)
        {
            UpdateFruitValidator validator = new UpdateFruitValidator();
            var res = await validator.ValidateAsync(vm);
            if (res != null)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    return View(vm);
                }
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            await _service.UpdateAsync(vm, _env.WebRootPath);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id, _env.WebRootPath);
            }
            catch (NegativeIdException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
            }
            catch (FruitNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
