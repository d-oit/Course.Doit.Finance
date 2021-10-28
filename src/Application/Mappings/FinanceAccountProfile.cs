using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class FinanceAccountProfile : Profile
    {
        public FinanceAccountProfile()
        {
            CreateMap<AddEditFinanceAccountCommand, FinanceAccount>().ReverseMap();
        }
    }
}