namespace BlazorHero.CleanArchitecture.Application.Requests.Finance
{
    public class GetAllPagedFinanceAccountsRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}