using AutoMapper;
using BlazorHero.CleanArchitecture.Application.Interfaces.Repositories;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.AddEdit
{
    public partial class AddEditFinanceAccountCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [Required]
        [StringLength(300)]
        public string Owner { get; set; }

        [Required]
        [StringLength(150)]
        public string Type { get; set; }


        [Required, MinLength(2), MaxLength(2)]
        public string CountryCode { get; set; } = "DE";

        [Range(0, 100)]
        public int SortOrder { get; set; } = 99;

        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        public DateTime? InactiveDate { get; set; }

        public decimal InitAmount { get; set; }

        public bool IsBankAccount { get; set; }

        public bool IsInvestmentAccount { get; set; }
    }

    internal class AddEditFinanceAccountCommandHandler : IRequestHandler<AddEditFinanceAccountCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<AddEditFinanceAccountCommandHandler> _localizer;

        public AddEditFinanceAccountCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper,
            IStringLocalizer<AddEditFinanceAccountCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditFinanceAccountCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<FinanceAccount>().Entities.Where(s => s.Id != command.Id).AnyAsync(cancellationToken: cancellationToken))
            {
                return await Result<int>.FailAsync("Record is already existed");
            }

            if (command.Id == 0)
            {
                var record = _mapper.Map<FinanceAccount>(command);
                await _unitOfWork.Repository<FinanceAccount>().AddAsync(record);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(record.Id, "Create Record Successfully");
            }
            else
            {
                var record = await _unitOfWork.Repository<FinanceAccount>().GetByIdAsync(command.Id);
                if (record != null)
                {
                    record.Name = command.Name ?? record.Name;
                    record.Code = command.Code ?? record.Code;
                    record.Type = command.Type ?? record.Type;
                    record.InitAmount = command.InitAmount;
                    record.IsBankAccount = command.IsBankAccount;
                    record.IsInvestmentAccount = command.IsInvestmentAccount;
                    record.IsActive = command.IsActive;
                    record.CountryCode = command.CountryCode;
                    record.Owner = command.Owner ?? record.Owner;
                    record.SortOrder = command.SortOrder;
                    record.InactiveDate = record.InactiveDate;

                    await _unitOfWork.Repository<FinanceAccount>().UpdateAsync(record);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(record.Id, "Update Record Successfully");
                }
                else
                {
                    return await Result<int>.FailAsync("Record Not Found");
                }
            }
        }
    }
}