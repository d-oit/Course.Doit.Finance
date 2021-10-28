using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using MediatR;
using BlazorHero.CleanArchitecture.Application.Extensions;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Application.Specifications.Finance;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetAllPaged
{
    public class GetAllFinanceAccountsQuery : IRequest<PaginatedResult<GetAllPagedFinanceAccountsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public string[]
            OrderBy { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllFinanceAccountsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
        GetAllFinanceAccountsQueryHandler : IRequestHandler<GetAllFinanceAccountsQuery, PaginatedResult<GetAllPagedFinanceAccountsResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllFinanceAccountsQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedFinanceAccountsResponse>> Handle(GetAllFinanceAccountsQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<FinanceAccount, GetAllPagedFinanceAccountsResponse>> expression = financeaccount => new GetAllPagedFinanceAccountsResponse
            {
                Id = financeaccount.Id,
                Name = financeaccount.Name,
                Code = financeaccount.Code,
                Owner = financeaccount.Owner,
                Type = financeaccount.Type,
                CountryCode = financeaccount.CountryCode,
                SortOrder = financeaccount.SortOrder,
                IsActive = financeaccount.IsActive,
                InactiveDate = financeaccount.InactiveDate,
                InitAmount = financeaccount.InitAmount,
                IsBankAccount = financeaccount.IsBankAccount,
                IsInvestmentAccount = financeaccount.IsInvestmentAccount
            };
            var recordFilterSpec = new FinanceAccountFilterSpecification(request.SearchString);
            
            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<FinanceAccount>().Entities
                    .Specify(recordFilterSpec)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<FinanceAccount>().Entities
                    .Specify(recordFilterSpec)
                    .OrderBy(ordering) // require system.linq.dynamic.core
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
        }
    }
}