using BlazorHero.CleanArchitecture.Domain.Contracts;
using BlazorHero.CleanArchitecture.Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Finance
{
    public class Investment : AuditableEntity<long>
    {
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
        public Guid FinanceAccountId { get; set; }

        [ForeignKey(nameof(FinanceAccountId))]
        public FinanceAccount FinanceAccount { get; set; }

        [StringLength(100)]
        public string No { get; set; }

        [StringLength(100)]
        public string ISIN { get; set; }

        [StringLength(100)]
        public string WKN { get; set; }

        [DataType(DataType.Date)]
        public DateTime? InvestmentEndDate { get; set; }

        [StringLength(emmisionNoLength)]
        public string EmmissionNo { get; set; }

        [StringLength(100)]
        public string TACode { get; set; }

        public decimal InvestmentValue { get; set; }

        public decimal NominalValueOfAnInvestment { get; set; }

        public double Quantity { get; set; } = 1;

        public double? DividendReturnPercentage { get; set; }

        public double? TotalReturnPercentage { get; set; }

        public bool IsActive { get; set; }

        [StringLength(_typeLength)]
        public string Type { get; set; } = "Finanzierung"; // Bestand, Aktien, Crowdfunding...

        [DataType(DataType.Date)]
        public DateTime InterestClaimSinceDate { get; set; }

        public int? DaysLate { get; set; }
    }

}