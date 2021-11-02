using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.AddEdit;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BlazorHero.CleanArchitecture.Application.Validators.Features.Finance.FinanceAccount.Commands.AddEdit
{
    public class AddEditFinanceAccountCommandValidator : AbstractValidator<AddEditFinanceAccountCommand>
    {
        public AddEditFinanceAccountCommandValidator(IStringLocalizer<AddEditFinanceAccountCommandValidator> localizer)
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => localizer["Name is required!"]);
        }
    }
}