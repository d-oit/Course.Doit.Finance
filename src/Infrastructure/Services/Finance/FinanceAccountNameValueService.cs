using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Infrastructure.Contexts;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Infrastructure.Services.Finance
{
    public class FinanceAccountValueNameService : IFinanceAccountValueNameService
    {
        private readonly ApplicationDbContext _context;

        public FinanceAccountValueNameService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResult<IEnumerable<NameValueResponse>>> GetAllAsync()
        {
            var list = await _context.FinanceAccounts.Where(x => x.IsActive).Select(x => new NameValueResponse { Id = x.Id, Name = x.Name }).OrderByDescending(a => a.Id).ToListAsync();
            return await Result<IEnumerable<NameValueResponse>>.SuccessAsync(list);
        }
    }
}
