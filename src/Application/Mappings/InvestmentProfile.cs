using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetById;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class InvestmentProfile : Profile
    {
        public InvestmentProfile()
        {
            CreateMap<AddEditInvestmentCommand, Investment>().ReverseMap();
            CreateMap<GetInvestmentByIdResponse, Investment>().ReverseMap();
            CreateMap<GetInvestmentByIdResponse, AddEditInvestmentCommand>().ReverseMap();
        }
    }
}