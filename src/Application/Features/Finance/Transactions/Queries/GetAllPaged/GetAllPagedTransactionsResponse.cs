using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using BlazorHero.CleanArchitecture.Domain.Enums;
using System;

namespace BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetAllPaged
{
    public class GetAllPagedTransactionsResponse
    {
        public long Id { get; set; }

        public string Name
        {
            get; set;
        }

        public string Code { get; set; }

        public DateTime? DateTime { get; set; }

        public DateTime? PostingDate { get; set; }

        public string DocumentNo { get; set; }

        public string CategoryName { get; set; }

        public long FinanceAccountId { get; set; }

        public string FinanceAccountName { get; set; }


        public string Receiver { get; set; }

        public decimal Amount
        {
            get; set;
        }


        public bool Favorit { get; set; }

        public bool IsCash { get; set; }

        public string Color { get; set; }

        public string UsedFormula { get; set; }

        public TransactionType TransactionType { get; set; }

        public BookingType BookingType { get; set; }

        public long? CounterAccountId { get; set; }

        public string CounterAccountName { get; set; }

        public long? InvestmentId { get; set; }

        public string InvestmentName { get; set; }


        public static GetAllPagedTransactionsResponse Mappper(Transaction e)
        {
            return new GetAllPagedTransactionsResponse
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                DateTime = e.DateTime,
                PostingDate = e.PostingDate,
                DocumentNo = e.DocumentNo,
                CategoryName = e.CategoryName,
                FinanceAccountId = e.FinanceAccountId,
                FinanceAccountName = e.FinanceAccount.Name,
                Receiver = e.Receiver,
                Amount = e.Amount,
                Favorit = e.Favorit,
                IsCash = e.IsCash,
                Color = e.Color,
                UsedFormula = e.UsedFormula,
                TransactionType = e.TransactionType,
                BookingType = e.BookingType,
                CounterAccountId = e.CounterAccountId,
                CounterAccountName = e.CounterAccount.Name,
                InvestmentId = e.InvestmentId,
                InvestmentName = e.Investment.Name
            };
        }
    }
}
