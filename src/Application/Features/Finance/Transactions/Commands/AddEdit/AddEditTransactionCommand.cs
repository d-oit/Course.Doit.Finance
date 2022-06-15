using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.AddEdit
{
    public partial class AddEditTransactionCommand : Transaction, IRequest<Result<long>>
    {
    }

    internal class AddEditTransactionCommandHandler : IRequestHandler<AddEditTransactionCommand, Result<long>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IStringLocalizer<AddEditTransactionCommandHandler> _localizer;

        public AddEditTransactionCommandHandler(IUnitOfWork<long> unitOfWork, IMapper mapper,
            IStringLocalizer<AddEditTransactionCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<long>> Handle(AddEditTransactionCommand command, CancellationToken cancellationToken)
        {

            if (command.Id == 0)
            {
                var record = _mapper.Map<Transaction>(command);
                await _unitOfWork.Repository<Transaction>().AddAsync(record);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<long>.SuccessAsync(record.Id, "Create Record Successfully");
            }
            else
            {
                var record = await _unitOfWork.Repository<Transaction>()
                    .GetByIdAsync(command.Id);

                if (record != null)
                {
                    record = _mapper.Map<Transaction>(command);

                    await _unitOfWork.Repository<Transaction>().UpdateAsync(record);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<long>.SuccessAsync(record.Id, "Update Record Successfully");
                }
                else
                {
                    return await Result<long>.FailAsync("Record Not Found");
                }
            }
        }
    }
}