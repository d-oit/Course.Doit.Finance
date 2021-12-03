using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Requests.Finance;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Extensions;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.Investment
{
    public class InvestmentManager : IInvestmentManager
    {
        private readonly HttpClient _httpClient;

        public InvestmentManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<long>> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.InvestmentsEndpoints.Delete}/{id}");
            return await response.ToResult<long>();
        }

        public async Task<PaginatedResult<GetAllPagedInvestmentsResponse>> GetInvestmentsAsync(GetAllPagedInvestmentsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.InvestmentsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedInvestmentsResponse>();
        }

        public async Task<IResult<GetInvestmentByIdResponse>> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync(Routes.InvestmentsEndpoints.GetById(id));
            var result = await response.ToResult<GetInvestmentByIdResponse>();
            return result;
        }

        public async Task<IResult<long>> SaveAsync(AddEditInvestmentCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.InvestmentsEndpoints.Save, request);
            return await response.ToResult<long>();
        }
    }
}