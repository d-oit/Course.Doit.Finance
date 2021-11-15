using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace BlazorHero.CleanArchitecture.Application.Validators.Features.Finance.Investment.Commands.AddEdit
{
    public class AddEditInvestmentCommandValidator : AbstractValidator<AddEditInvestmentCommand>
    {
        public AddEditInvestmentCommandValidator(IStringLocalizer<AddEditInvestmentCommandValidator> localizer)
        {
            //TODO: Insert Data Validation Here
            /*RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
            RuleFor(request => request.Barcode)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Barcode is required!"]);
            RuleFor(request => request.Description)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Description is required!"]);
            RuleFor(request => request.BrandId)
                .GreaterThan(0).WithMessage(x => localizer["Brand is required!"]);
            RuleFor(request => request.Rate)
                .GreaterThan(0).WithMessage(x => localizer["Rate must be greater than 0"]);*/
        }
    }
}