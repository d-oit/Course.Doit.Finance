using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Application.Requests.Finance;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Cache;
using BlazorHero.CleanArchitecture.Client.Infrastructure.Extensions;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.FinanceAccount
{
    public class FinanceAccountManager : IFinanceAccountManager
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;

        public FinanceAccountManager(HttpClient httpClient, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.FinanceAccountsEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<PaginatedResult<GetAllPagedFinanceAccountsResponse>> GetFinanceAccountsAsync(GetAllPagedFinanceAccountsRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.FinanceAccountsEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPagedFinanceAccountsResponse>();
        }

        public async Task<IResult<GetFinanceAccountByIdResponse>> GetByIdAsync(GetFinanceAccountByIdQuery request)
        {
            var response = await _httpClient.GetAsync(Routes.FinanceAccountsEndpoints.GetById(request.Id));
            return await response.ToResult<GetFinanceAccountByIdResponse>();
        }

        public async Task<IResult<int>> SaveAsync(AddEditFinanceAccountCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.FinanceAccountsEndpoints.Save, request);
            return await response.ToResult<int>();
        }

        public async Task<List<NameValueResponse>> GetFinanceAccountNamesAsync()
        {
            List<NameValueResponse> cacheList;
            // If found in cache, return cached data
            if (_memoryCache.TryGetValue(CacheKeys.FinanceAccountValueNames, out cacheList))
            {
                return await Task.Run(() => cacheList);
            }

            // If not found, then calculate response
            var response = await _httpClient.GetAsync(Routes.FinanceAccountsEndpoints.GetFinanceAccountNames());
            IResult<List<NameValueResponse>> list = await response.ToResult<List<NameValueResponse>>();
            // Set cache options
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(30));

            // Set object in cache
            _memoryCache.Set(CacheKeys.FinanceAccountValueNames, list, cacheOptions);
            return list.Data;


        }
    }
}