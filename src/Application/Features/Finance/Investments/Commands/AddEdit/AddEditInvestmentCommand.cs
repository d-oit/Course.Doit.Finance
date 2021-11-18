using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit
{
    public partial class AddEditInvestmentCommand : Investment, IRequest<Result<long>>
    {
    }

    internal class AddEditInvestmentCommandHandler : IRequestHandler<AddEditInvestmentCommand, Result<long>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<long> _unitOfWork;
        private readonly IStringLocalizer<AddEditInvestmentCommandHandler> _localizer;

        public AddEditInvestmentCommandHandler(IUnitOfWork<long> unitOfWork, IMapper mapper,
            IStringLocalizer<AddEditInvestmentCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<long>> Handle(AddEditInvestmentCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Investment>().Entities.Where(s => s.Id != command.Id).AnyAsync(cancellationToken: cancellationToken))
            {
                return await Result<long>.FailAsync("Record is already existed");
            }

            if (command.Id == 0)
            {
                var record = _mapper.Map<Investment>(command);
                if (string.IsNullOrWhiteSpace(record.No))
                {
                    record.No = Guid.NewGuid().ToString();
                }
                await _unitOfWork.Repository<Investment>().AddAsync(record);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<long>.SuccessAsync(record.Id, "Create Record Successfully");
            }
            else
            {
                var record = await _unitOfWork.Repository<Investment>()
                    .GetByIdAsync(command.Id);
                record = _mapper.Map<Investment>(command);
                if (record != null)
                {
                    await _unitOfWork.Repository<Investment>().UpdateAsync(record);
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