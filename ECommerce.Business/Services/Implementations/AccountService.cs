using AutoMapper;
using ECommerce.Business.Enums;
using ECommerce.Business.Exceptions.User;
using ECommerce.Business.Services.Interfaces;
using ECommerce.Business.ViewModels.UserVMs;
using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
        }

        public async Task<IdentityResult> Register(RegisterUserVm vm)
        {
            AppUser user = _mapper.Map<AppUser>(vm);
            var res = await _userManager.CreateAsync(user, vm.Password);
            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }
            return res;
        }

        public async Task<AppUser> Login(LoginUserVm vm)
        {
            AppUser user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail) ?? await _userManager.FindByNameAsync(vm.UsernameOrEmail);
            if (user == null) throw new UserNotFoundException("Username / Email is wrong.", nameof(vm.UsernameOrEmail));
            return user;
        }
    }
}
