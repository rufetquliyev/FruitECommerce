using ECommerce.Core.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECommerce.Business.ViewModels.UserVMs
{
    public record RegisterUserVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RegisterUserValidator : AbstractValidator<RegisterUserVm>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(25).WithMessage("Length cannot be more than 25 characters.");
            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage("Surname cannot be empty.")
                .MaximumLength(45).WithMessage("Length cannot be more than 45 characters.");
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("Username cannot be empty.")
                .MaximumLength(70).WithMessage("Username cannot be more than 70 characters.");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password cannot be empty.")
                .Must(x =>
                {
                    Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
                    bool match = regex.IsMatch(x);
                    return match;
                }).WithMessage("Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character.");
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().WithMessage("Confirm password cannot be empty.")
                .Equal(x => x.Password).WithMessage("Confirm password must be equal to password.");
        }
    }
}
