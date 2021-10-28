using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Localization;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.Delete
{
    public partial class DeleteFinanceAccountCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteFinanceAccountCommandHandler : IRequestHandler<DeleteFinanceAccountCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteFinanceAccountCommandHandler> _localizer;

        public DeleteFinanceAccountCommandHandler(IUnitOfWork<int> unitOfWork,
            IStringLocalizer<DeleteFinanceAccountCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteFinanceAccountCommand command, CancellationToken cancellationToken)
        {
            var record =
                await _unitOfWork.Repository<FinanceAccount>().GetByIdAsync(command.Id);
            if (record != null)
            {
                await _unitOfWork.Repository<FinanceAccount>().DeleteAsync(record);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(record.Id, "Delete Record Successfully");
            }
            else
            {
                return await Result<int>.FailAsync("Record Not Found");
            }
        }
    }
}