using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using System;

namespace BlazorHero.CleanArchitecture.Application.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<AddEditTransactionCommand, Transaction>().ReverseMap();
        }
    }
}