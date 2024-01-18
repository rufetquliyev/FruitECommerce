using AutoMapper;
using ECommerce.Business.Services.Interfaces;
using ECommerce.Business.ViewModels.UserVMs;
using ECommerce.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(IAccountService accountService, SignInManager<AppUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVm vm)
        {
            RegisterUserValidator validationRes = new RegisterUserValidator();
            var result = validationRes.Validate(vm);
            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var res = await _accountService.Register(vm);
            if (!res.Succeeded)
            {
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(vm);
            }
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserVm vm)
        {
            LoginUserValidator validationRes = new LoginUserValidator();
            var result = validationRes.Validate(vm);
            if (result != null)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var res = await _accountService.Login(vm);
            if (!res.Success)
            {
                ModelState.AddModelError("", "Username Or Password is wrong!");
                return View(vm);
            }
            await _signInManager.SignInAsync(res.user, false);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRole()
        {
            await _accountService.CreateRole();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
