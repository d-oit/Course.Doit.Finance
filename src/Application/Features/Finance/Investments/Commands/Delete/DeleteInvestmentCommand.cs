using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using System;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.Delete
{
    public partial class DeleteInvestmentCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }
    }

    internal class DeleteInvestmentCommandHandler : IRequestHandler<DeleteInvestmentCommand, Result<long>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IStringLocalizer<DeleteInvestmentCommandHandler> _localizer;

        public DeleteInvestmentCommandHandler(IUnitOfWork<long> unitOfWork,
            IStringLocalizer<DeleteInvestmentCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<long>> Handle(DeleteInvestmentCommand command, CancellationToken cancellationToken)
        {
            var record =
                await _unitOfWork.Repository<Investment>().GetByIdAsync(command.Id);
            if (record != null)
            {
                await _unitOfWork.Repository<Investment>().DeleteAsync(record);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<long>.SuccessAsync(record.Id, "Delete Record Successfully");
            }
            else
            {
                return await Result<long>.FailAsync("Record Not Found");
            }
        }
    }
}