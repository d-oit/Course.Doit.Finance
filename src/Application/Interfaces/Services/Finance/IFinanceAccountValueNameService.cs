﻿using BlazorHero.CleanArchitecture.Application.Requests;
using BlazorHero.CleanArchitecture.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Interfaces.Services
{
    public interface IFinanceAccountValueNameService
    {
        Task<IResult<IEnumerable<NameValueResponse>>> GetAllAsync();
    }
    
}