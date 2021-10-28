using BlazorHero.CleanArchitecture.Application.Enums;
using System;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetAllPaged
{
    public class GetAllPagedFinanceAccountsResponse
    {
        public int Id { get; set; }
     
        public string Name { get; set; }

        public string Code { get; set; }

        public string Owner { get; set; }

        public string Type { get; set; }

        public string CountryCode { get; set; } = "DE";

        public int SortOrder { get; set; } = 99;

        public bool IsActive { get; set; }

        public DateTime? InactiveDate { get; set; }

        public decimal InitAmount { get; set; }

        public bool IsBankAccount { get; set; }

        public bool IsInvestmentAccount { get; set; }
    }
}
