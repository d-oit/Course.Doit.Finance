using BlazorHero.CleanArchitecture.Domain.Contracts;
using BlazorHero.CleanArchitecture.Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Finance
{
    public class FinanceAccount : AuditableEntity<int>
    {
        private bool isActive;
        private string _code;
        private string _name;
        private bool isBankAccount;
    

        [Required]
        [StringLength(300)]
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

        [StringLength(50)]
        public string Code { get => _code; set => _code = StringExt.Truncate(value?.ToUpperInvariant(), 50); }

        [Required]
        [StringLength(300)]
        public string Owner { get; set; } = "";

        [Required]
        [StringLength(150)]
        public string Type { get; set; } = "";


        [Required, MinLength(2), MaxLength(2)]
        public string CountryCode { get; set; } = "DE";

        [Range(0, 100)]
        public int SortOrder { get; set; } = 99;

        public bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                if (!value)
                {
                    InactiveDate = DateTime.Now.Date;
                }
            }
        }

        [DataType(DataType.Date)]
        public DateTime? InactiveDate { get; set; }

        public decimal InitAmount
        {
            get; set;
        }

        public bool IsBankAccount
        {
            get => isBankAccount; set
            {
                isBankAccount = value;
            }
        }

        public bool IsInvestmentAccount
        {
            get; set;
        }
    }
}
