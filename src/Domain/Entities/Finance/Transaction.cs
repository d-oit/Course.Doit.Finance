using BlazorHero.CleanArchitecture.Domain.Contracts;
using BlazorHero.CleanArchitecture.Domain.Enums;
using BlazorHero.CleanArchitecture.Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorHero.CleanArchitecture.Domain.Entities.Finance
{

    public class Transaction : AuditableEntity<long>
    {
        private decimal amount;
        private string _name;
        private string _code = "";

        public const int _codeLength = 20;
        public const int _nameLength = 500;

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

        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostingDate { get; set; }

        [StringLength(100)]
        public string DocumentNo { get; set; } = "";

        [StringLength(200)]
        public string CategoryName { get; set; } = "";

        [StringLength(700)]
        public string Description { get; set; } // TODO Buchungstext -> Rules for Category and Co !

        [Required]
        public int FinanceAccountId { get; set; }

        [ForeignKey(nameof(FinanceAccountId))]
        public FinanceAccount FinanceAccount { get; set; }



        [StringLength(700)]
        public string Receiver { get; set; }

        public bool IsInitAmount { get; set; } = false;

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount
        {
            get => amount; set
            {
                if ((TransactionType == TransactionType.Expense))
                {
                    amount = -value;
                }
                else
                {
                    amount = value;
                }
            }
        }

        [NotMapped]
        public decimal IncomeAmount
        {
            get
            {
                if (TransactionType == TransactionType.Income)
                    return Amount;

                return 0;
            }
        }

        [NotMapped]
        public decimal ExpenseAmount
        {
            get
            {
                if (TransactionType == TransactionType.Expense)
                    return Amount;

                return 0;
            }
        }

        [NotMapped]
        public decimal InvestmentAmount
        {
            get
            {
                if (TransactionType == TransactionType.Investment)
                    return Amount;

                return 0;
            }
        }

        public bool Favorit { get; set; }

        public string Notes { get; set; }

        public bool IsCash { get; set; } = false;


        [StringLength(50)]
        public string Color { get; set; } = "#FFFFFF";

        public string UsedFormula { get; set; }

        public TransactionType TransactionType { get; set; }

        public BookingType BookingType { get; set; }


        public int? CounterAccountId { get; set; }

        [ForeignKey(nameof(CounterAccountId))]
        public FinanceAccount CounterAccount { get; set; }

        public long? InvestmentId { get; set; }

        [ForeignKey(nameof(InvestmentId))]
        public Investment Investment { get; set; }
    }
}