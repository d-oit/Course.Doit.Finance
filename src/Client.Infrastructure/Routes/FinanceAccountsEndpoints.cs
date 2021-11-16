using System.Linq;

namespace BlazorHero.CleanArchitecture.Client.Infrastructure.Routes
{
    public static class FinanceAccountsEndpoints
    {
        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/v1/financeaccounts?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
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

        public static string GetById(int financeaccountId)
        {
            return $"api/v1/financeaccounts/{financeaccountId}";
        }

        public static string GetFinanceAccountNames()
        {
            return $"api/v1/financeaccountnames/";
        }

        public static string GetCount = "api/v1/financeaccounts/count";

        public static string Save = "api/v1/financeaccounts";
        public static string Delete = "api/v1/financeaccounts";
    }
}