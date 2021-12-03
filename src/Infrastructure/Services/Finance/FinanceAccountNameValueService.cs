using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using BlazorHero.CleanArchitecture.Shared.Constants.Application;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using LazyCache;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Infrastructure.Services.Finance
{
    public class FinanceAccountValueNameService : IFinanceAccountValueNameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppCache _cache;

        private static TimeSpan cacheExpiry = new TimeSpan(48, 0, 0); //12 hours


        public FinanceAccountValueNameService(ApplicationDbContext context, IAppCache cache)
        {
            _context = context;
            this._cache = cache;
        }

        public async Task<IResult<IEnumerable<NameIntValueResponse>>> GetAllAsync()
        {
            var model = await _cache.GetOrAddAsync(ApplicationConstants.Cache.FinanceAccountValueNamesCacheKey, async () =>
            {
                var list = await _context.FinanceAccounts.Where(x => x.IsActive)
                    .Select(x => new NameIntValueResponse { Id = x.Id, Name = x.Name })
                    .OrderByDescending(a => a.Id).ToListAsync();
                return await Result<IEnumerable<NameIntValueResponse>>.SuccessAsync(list);
            }, cacheExpiry);

            return model;
        }

        public void ClearNamesCache() => _cache.Remove(ApplicationConstants.Cache.FinanceAccountValueNamesCacheKey);
    }
}
