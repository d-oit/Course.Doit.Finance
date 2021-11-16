using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Application.Requests.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.FinanceAccount
{
    public interface IFinanceAccountManager : IManager
    {
        Task<PaginatedResult<GetAllPagedFinanceAccountsResponse>> GetFinanceAccountsAsync(GetAllPagedFinanceAccountsRequest request);

        Task<IResult<GetFinanceAccountByIdResponse>> GetByIdAsync(GetFinanceAccountByIdQuery request);

        Task<IResult<int>> SaveAsync(AddEditFinanceAccountCommand request);

        Task<IResult<int>> DeleteAsync(int id);
        Task<List<NameValueResponse>> GetFinanceAccountNamesAsync();
    }
}