using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Requests.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Managers.Finance.Investment
{
    public interface IInvestmentManager : IManager
    {
        Task<PaginatedResult<GetAllPagedInvestmentsResponse>> GetInvestmentsAsync(GetAllPagedInvestmentsRequest request);

        Task<IResult<GetInvestmentByIdResponse>> GetByIdAsync(long id);

        Task<IResult<long>> SaveAsync(AddEditInvestmentCommand request);

        Task<IResult<long>> DeleteAsync(long id);
    }
}