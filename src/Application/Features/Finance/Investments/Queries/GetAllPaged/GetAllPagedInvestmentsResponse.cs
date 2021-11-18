using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetAllPaged
{
    public class GetAllPagedInvestmentsResponse
    {
        public long Id { get; set; }
        private string _code = "";
        private string _name;

        public const int _codeLength = 50;
        public const int _nameLength = 400;
        public const int _typeLength = 400;
        public const int emmisionNoLength = 100;

        [StringLength(_nameLength)]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                if (!string.IsNullOrEmpty(value) && string.IsNullOrEmpty(Code))
                {
                    Code = _name.ToUpperInvariant().Trim();
                };
            }
        }

        [StringLength(_codeLength)]
        public string Code { get => _code; set => _code = StringExt.Truncate(value?.ToUpperInvariant(), _codeLength); }

        [Required]
        public long FinanceAccountId { get; set; }

        public string FinanceAccountName { get; set; }

        [StringLength(100)]
        public string No { get; set; }


        public decimal InvestmentValue { get; set; }



        public bool IsActive { get; set; }

        [StringLength(_typeLength)]
        public string Type { get; set; } = "Finanzierung"; // Bestand, Aktien, Crowdfunding...


        public DateTime InterestClaimSinceDate { get; set; } = DateTime.UtcNow.Date;



        public static GetAllPagedInvestmentsResponse Map(Investment entity)
        {
            return new GetAllPagedInvestmentsResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code,
                FinanceAccountId = entity.FinanceAccountId,
                FinanceAccountName = entity.FinanceAccount.Name,
                No = entity.No,
                InvestmentValue = entity.InvestmentValue,
                IsActive = entity.IsActive,
                Type = entity.Type,
                InterestClaimSinceDate = entity.InterestClaimSinceDate
            };
        }
    }
}
