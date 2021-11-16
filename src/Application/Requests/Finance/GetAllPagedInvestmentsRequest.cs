namespace BlazorHero.CleanArchitecture.Application.Requests.Finance
{
    public class GetAllPagedInvestmentsRequest : PagedRequest
    {
        public string SearchString { get; set; }

        public long? FilterFinanceAccountId { get; set; }
    }
}