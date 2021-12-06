using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Requests.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.Transaction
{
    public interface ITransactionManager : IManager
    {
        Task<PaginatedResult<GetAllPagedTransactionsResponse>> GetTransactionsAsync(GetAllPagedTransactionsRequest request);

        Task<IResult<GetTransactionByIdResponse>> GetByIdAsync(long id);

        Task<IResult<long>> SaveAsync(AddEditTransactionCommand request);

        Task<IResult<long>> DeleteAsync(long id);
        Task<IResult<AddEditInvestmentCommand>> GetEditByIdAsync(long id);
    }
}