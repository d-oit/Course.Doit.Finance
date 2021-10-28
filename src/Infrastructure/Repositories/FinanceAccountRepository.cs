using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;

namespace BlazorHero.CleanArchitecture.Infrastructure.Repositories
{
    public class FinanceAccountRepository : IFinanceAccountRepository
    {
        private readonly IRepositoryAsync<FinanceAccount, int> _repository;

        public FinanceAccountRepository(IRepositoryAsync<FinanceAccount, int> repository)
        {
            _repository = repository;
        }
    }
}