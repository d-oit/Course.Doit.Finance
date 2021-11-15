using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using System;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class InvestmentProfile : Profile
    {
        public InvestmentProfile()
        {
            CreateMap<AddEditInvestmentCommand, Investment>().ReverseMap();
        }
    }
}