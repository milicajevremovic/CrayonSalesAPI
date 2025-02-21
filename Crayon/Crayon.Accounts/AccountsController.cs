using Crayon.Crayon.Accounts.Queries;
using Crayon.Crayon.Licences.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Crayon.Crayon.Accounts
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await _mediator.Send(new GetAccountsQuery());
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        }

        [HttpGet("{accountId}/licenses")]
        public async Task<IActionResult> GetLicenses(Guid accountId)
        {
            var result = await _mediator.Send(new GetPurchasedLicensesQuery(accountId));
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        }
    }
}
