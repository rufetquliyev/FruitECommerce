using ECommerce.Business.ViewModels.UserVMs;
using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IdentityResult> Register(RegisterUserVm vm);
        public Task<AppUser> Login(LoginUserVm vm);
        public Task CreateRole();
    }
}
