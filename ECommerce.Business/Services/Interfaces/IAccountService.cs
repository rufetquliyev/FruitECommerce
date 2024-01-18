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
        public Task<LoginResult> Login(LoginUserVm vm);
        public Task CreateRole();
    }
    public class LoginResult
    {
        public bool Success { get; set; }
        public AppUser? user { get; set; }
    }
}
