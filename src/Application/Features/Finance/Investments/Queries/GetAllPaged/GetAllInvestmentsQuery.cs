using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Specifications.Finance;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetAllPaged
{
    public class GetAllInvestmentsQuery : IRequest<PaginatedResult<GetAllPagedInvestmentsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public string[]
            OrderBy
        { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllInvestmentsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }

    internal class
        GetAllInvestmentsQueryHandler : IRequestHandler<GetAllInvestmentsQuery, PaginatedResult<GetAllPagedInvestmentsResponse>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;

        public GetAllInvestmentsQueryHandler(IUnitOfWork<long> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedInvestmentsResponse>> Handle(GetAllInvestmentsQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Investment, GetAllPagedInvestmentsResponse>> expression = entity => new GetAllPagedInvestmentsResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                FinanceAccountId = entity.FinanceAccountId,
                FinanceAccountName = entity.FinanceAccount.Name,
                No = entity.No,
                InvestmentValue = entity.InvestmentValue,
                IsActive = entity.IsActive,
                Type = entity.Type,
                InterestClaimSinceDate = entity.InterestClaimSinceDate
            };
            var recordFilterSpec = new InvestmentFilterSpecification(request.SearchString);

            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Investment>().Entities
                    .Specify(recordFilterSpec)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Investment>().Entities
                    .Specify(recordFilterSpec)
                    .OrderBy(ordering) // require system.linq.dynamic.core
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
        }
    }
}