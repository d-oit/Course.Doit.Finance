using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using System;

namespace BlazorHero.CleanArchitecture.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IRepositoryAsync<Transaction, long> _repository;

        public TransactionRepository(IRepositoryAsync<Transaction, long> repository)
        {
            _repository = repository;
        }
    }
}