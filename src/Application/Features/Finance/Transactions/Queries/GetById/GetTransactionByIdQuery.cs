using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetById
{
    public class GetTransactionByIdQuery : Transaction, IRequest<Result<GetTransactionByIdResponse>>
    {
    }

    internal class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, Result<GetTransactionByIdResponse>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(IUnitOfWork<long> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetTransactionByIdResponse>> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken)
        {
            var transaction = await _unitOfWork.Repository<Transaction>().GetByIdAsync(query.Id);
            var mappedTransaction = _mapper.Map<GetTransactionByIdResponse>(transaction);
            return await Result<GetTransactionByIdResponse>.SuccessAsync(mappedTransaction);
        }
    }
}