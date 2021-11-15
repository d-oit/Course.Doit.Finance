using BlazorHero.CleanArchitecture.Application.Specifications.Base;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;

namespace BlazorHero.CleanArchitecture.Application.Specifications.Finance
{
    public class InvestmentFilterSpecification : HeroSpecification<Investment>
    {
        public InvestmentFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.Name.Contains(searchString) || p.Code.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}