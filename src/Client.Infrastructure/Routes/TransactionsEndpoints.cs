using System.Linq;
using System;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Routes
{
    public static class TransactionsEndpoints
    {
        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/v1/transactions?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // loose training ,
            }
            return url;
        }

        public static string GetById(long transactionId)
        {
            return $"api/v1/transactions/{transactionId}";
        }

        public static string GetCount = "api/v1/transactions/count";

        public static string Save = "api/v1/transactions";
        public static string Delete = "api/v1/transactions";
    }
}