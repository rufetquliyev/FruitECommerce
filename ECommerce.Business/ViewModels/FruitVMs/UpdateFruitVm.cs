﻿using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.ViewModels.FruitVMs
{
    public record UpdateFruitVm
    {
        public int Id { get; set; }
        public string FruitName { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
        public IFormFile? Image { get; set; }
    }
    public class UpdateFruitValidator : AbstractValidator<UpdateFruitVm>
    {
        public UpdateFruitValidator()
        {
            RuleFor(x => x.FruitName).NotNull().NotEmpty().WithMessage("Fruit name cannot be empty.")
                .MaximumLength(30).WithMessage("Length cannot be more than 30 characters.");
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("Category name cannot be empty.")
                .MaximumLength(40).WithMessage("Length cannot be more than 40 characters.");
        }
    }
}
