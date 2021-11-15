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

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetAllPaged
{
    public class GetAllTransactionsQuery : IRequest<PaginatedResult<GetAllPagedTransactionsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public string[] OrderBy
        { get; set; } // of the form fieldname [ascending|descending],fieldname [ascending|descending]...

        public GetAllTransactionsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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
        GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, PaginatedResult<GetAllPagedTransactionsResponse>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;

        public GetAllTransactionsQueryHandler(IUnitOfWork<long> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedResult<GetAllPagedTransactionsResponse>> Handle(GetAllTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            Expression<Func<Transaction, GetAllPagedTransactionsResponse>> expression = e => new GetAllPagedTransactionsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                DateTime = e.DateTime,
                PostingDate = e.PostingDate,
                DocumentNo = e.DocumentNo,
                CategoryId = e.CategoryId,
                FinanceAccountId = e.FinanceAccountId,
                FinanceAccountName = e.FinanceAccount.Name,
                Receiver = e.Receiver,
                Amount = e.Amount,
                Favorit = e.Favorit,
                IsCash = e.IsCash,
                Color = e.Color,
                UsedFormula = e.UsedFormula,
                TransactionType = e.TransactionType,
                BookingType = e.BookingType,
                CounterAccountId = e.CounterAccountId,
                CounterAccountName = e.CounterAccount.Name,
                InvestmentId = e.InvestmentId,
                InvestmentName = e.Investment.Name

            };
            var recordFilterSpec = new TransactionFilterSpecification(request.SearchString);

            if (request.OrderBy?.Any() != true)
            {
                var data = await _unitOfWork.Repository<Transaction>().Entities
                    .Specify(recordFilterSpec)
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
            else
            {
                var ordering = string.Join(",", request.OrderBy); // of the form fieldname [ascending|descending], ...
                var data = await _unitOfWork.Repository<Transaction>().Entities
                    .Specify(recordFilterSpec)
                    .OrderBy(ordering) // require system.linq.dynamic.core
                    .Select(expression)
                    .ToPaginatedListAsync(request.PageNumber, request.PageSize);
                return data;
            }
        }
    }
}