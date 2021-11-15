using BlazorHero.CleanArchitecture.Application.Specifications.Base;
using BlazorHero.CleanArchitecture.Domain.Entities.Finance;
using System;

namespace BlazorHero.CleanArchitecture.Application.Specifications.Finance
{
    public class TransactionFilterSpecification :  HeroSpecification<Transaction>
    {
        public TransactionFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                //TODO: Insert Criteria Here
                //Criteria = p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString);
                Criteria = p => true;
            } else {
                Criteria = p => true;
            }
        }
    }
}