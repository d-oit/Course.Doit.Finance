using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Requests.Finance;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Extensions;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.Transaction
{
    public class TransactionManager : ITransactionManager
    {
        private readonly HttpClient _httpClient;

        public TransactionManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<long>> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.TransactionsEndpoints.Delete}/{id}");
            return await response.ToResult<long>();
        }

        public async Task<PaginatedResult<GetAllPagedTransactionsResponse>> GetTransactionsAsync(GetAllPagedTransactionsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.TransactionsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedTransactionsResponse>();
        }

        public async Task<IResult<GetTransactionByIdResponse>> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync(Routes.TransactionsEndpoints.GetById(id));
            return await response.ToResult<GetTransactionByIdResponse>();
        }

        public async Task<IResult<long>> SaveAsync(AddEditTransactionCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.TransactionsEndpoints.Save, request);
            return await response.ToResult<long>();
        }
    }
}