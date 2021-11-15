namespace BlazorHero.CleanArchitecture.Application.Requests.Finance
{
    public class GetAllPagedTransactionsRequest : PagedRequest
    {
        public string SearchString { get; set; }
    }
}