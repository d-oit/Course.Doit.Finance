using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using System;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.Delete
{
    public partial class DeleteTransactionCommand : IRequest<Result<long>>
    {
        public long Id { get; set; }
    }

    internal class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Result<long>>
    {
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IStringLocalizer<DeleteTransactionCommandHandler> _localizer;

        public DeleteTransactionCommandHandler(IUnitOfWork<long> unitOfWork,
            IStringLocalizer<DeleteTransactionCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<long>> Handle(DeleteTransactionCommand command, CancellationToken cancellationToken)
        {
            var record =
                await _unitOfWork.Repository<Transaction>().GetByIdAsync(command.Id);
            if (record != null)
            {
                await _unitOfWork.Repository<Transaction>().DeleteAsync(record);
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