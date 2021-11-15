using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Commands.Delete;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Investments.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using BlazorHero.CleanArchitecture.Server.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.Finance
{
    public class InvestmentsController : BaseApiController<InvestmentsController>
    {
        /// <summary>
        /// Get All Investments
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Investments.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var records = await _mediator.Send(new GetAllInvestmentsQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(records);
        }

        /// <summary>
        /// Get Investment By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Investments.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var record = await _mediator.Send(new GetInvestmentByIdQuery { Id = id });
            return Ok(record);
        }

        /// <summary>
        /// Add/Edit a Investment
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Investments.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditInvestmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Investment
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK response</returns>
        [Authorize(Policy = Permissions.Investments.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await _mediator.Send(new DeleteInvestmentCommand { Id = id }));
        }
    }
}

