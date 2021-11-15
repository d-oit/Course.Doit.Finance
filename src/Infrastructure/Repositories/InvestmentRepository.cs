using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using System;

namespace BlazorHero.CleanArchitecture.Infrastructure.Repositories
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private readonly IRepositoryAsync<Investment, long> _repository;

        public InvestmentRepository(IRepositoryAsync<Investment, long> repository)
        {
            _repository = repository;
        }
    }
}