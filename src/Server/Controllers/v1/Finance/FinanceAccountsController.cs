using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Commands.Delete;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Application.Features.Finance.FinanceAccounts.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Interfaces.Services;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.Finance
{
    public class FinanceAccountsController : BaseApiController<FinanceAccountsController>
    {
        private IFinanceAccountValueNameService _financeAccountValueNameServiceInstance;

        protected IFinanceAccountValueNameService _financeAccountValueNameService => _financeAccountValueNameServiceInstance ??= HttpContext.RequestServices.GetService<IFinanceAccountValueNameService>();


        /// <summary>
        /// Get All FinanceAccounts
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.FinanceAccounts.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var records = await _mediator.Send(new GetAllFinanceAccountsQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(records);
        }

        [Authorize(Policy = Permissions.FinanceAccounts.View)]
        [HttpGet("names")]
        public async Task<IActionResult> GetNamesAsync()
        {
            var list = await _financeAccountValueNameService.GetAllAsync();
            return Ok(list);
        }

        /// <summary>
        /// Get FinanceAccount By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.FinanceAccounts.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var record = await _mediator.Send(new GetFinanceAccountByIdQuery { Id = id });
            return Ok(record);
        }

        /// <summary>
        /// Add/Edit a FinanceAccount
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.FinanceAccounts.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditFinanceAccountCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a FinanceAccount
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK response</returns>
        [Authorize(Policy = Permissions.FinanceAccounts.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteFinanceAccountCommand { Id = id }));
        }
    }
}

