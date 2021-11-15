using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetById
{
    public class GetInvestmentByIdQuery : Investment, IRequest<Result<GetInvestmentByIdResponse>>
    {
    }

    internal class GetInvestmentByIdQueryHandler : IRequestHandler<GetInvestmentByIdQuery, Result<GetInvestmentByIdResponse>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IMapper _mapper;

        public GetInvestmentByIdQueryHandler(IUnitOfWork<long> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetInvestmentByIdResponse>> Handle(GetInvestmentByIdQuery query, CancellationToken cancellationToken)
        {
            var investment = await _unitOfWork.Repository<Investment>().GetByIdAsync(query.Id);
            var mappedInvestment = _mapper.Map<GetInvestmentByIdResponse>(investment);
            return await Result<GetInvestmentByIdResponse>.SuccessAsync(mappedInvestment);
        }
    }
}