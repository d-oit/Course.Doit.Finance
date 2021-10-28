using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetById
{
    public class GetFinanceAccountByIdQuery : IRequest<Result<GetFinanceAccountByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetFinanceAccountByIdQueryHandler : IRequestHandler<GetFinanceAccountByIdQuery, Result<GetFinanceAccountByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetFinanceAccountByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetFinanceAccountByIdResponse>> Handle(GetFinanceAccountByIdQuery query, CancellationToken cancellationToken)
        {
            var financeaccount = await _unitOfWork.Repository<FinanceAccount>().GetByIdAsync(query.Id);
            var mappedFinanceAccount = _mapper.Map<GetFinanceAccountByIdResponse>(financeaccount);
            return await Result<GetFinanceAccountByIdResponse>.SuccessAsync(mappedFinanceAccount);
        }
    }
}