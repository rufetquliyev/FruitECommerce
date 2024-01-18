using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECommerce.Business.ViewModels.UserVMs
{
    public record LoginUserVm
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
    public class LoginUserValidator : AbstractValidator<LoginUserVm>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotNull().NotEmpty().WithMessage("Username/Email cannot be empty.")
                .MaximumLength(70).WithMessage("Length cannot be more than 70 characters.");
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password cannot be empty.")
                .Must(x =>
                {
                    Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
                    bool match = regex.IsMatch(x);
                    return match;
                }).WithMessage("Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.");

        }
    }
}
