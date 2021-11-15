using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.AddEdit;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetById;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Commands.Delete;
using BlazorHero.CleanArchitecture.Application.Features.Finance.Transactions.Queries.GetAllPaged;
using BlazorHero.CleanArchitecture.Shared.Constants.Permission;
using BlazorHero.CleanArchitecture.Server.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

namespace BlazorHero.CleanArchitecture.Server.Controllers.v1.Finance
{
    public class TransactionsController : BaseApiController<TransactionsController>
    {
        /// <summary>
        /// Get All Transactions
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchString"></param>
        /// <param name="orderBy"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Transactions.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize, string searchString, string orderBy = null)
        {
            var records = await _mediator.Send(new GetAllTransactionsQuery(pageNumber, pageSize, searchString, orderBy));
            return Ok(records);
        }

        /// <summary>
        /// Get Transaction By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 Ok</returns>
        [Authorize(Policy = Permissions.Transactions.View)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var record = await _mediator.Send(new GetTransactionByIdQuery { Id = id });
            return Ok(record);
        }

        /// <summary>
        /// Add/Edit a Transaction
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Transactions.Create)]
        [HttpPost]
        public async Task<IActionResult> Post(AddEditTransactionCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete a Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK response</returns>
        [Authorize(Policy = Permissions.Transactions.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            return Ok(await _mediator.Send(new DeleteTransactionCommand { Id = id }));
        }
    }
}

