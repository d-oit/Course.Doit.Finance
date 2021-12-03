using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Domain.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetAllPaged
{
    public class GetAllPagedInvestmentsResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Code { get; set; }
        public long FinanceAccountId { get; set; }

        public string FinanceAccountName { get; set; }

        public string No { get; set; }

        public decimal InvestmentValue { get; set; }

        public bool IsActive { get; set; }

        public string Type { get; set; }

        public DateTime? InterestClaimSinceDate { get; set; }

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
