using Crayon.Crayon.Licences.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Crayon.Crayon.Licences
{
    [ApiController]
    [Route("api/[controller]")]
    public class LicenseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LicenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("license/{id}/quantity")]
        public async Task<IActionResult> ChangeQuantity(Guid id, [FromBody] int newQuantity)
        {
            var command = new ChangeLicenseQuantityCommand(id, newQuantity);
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        }

        [HttpDelete("license/{id}")]
        public async Task<IActionResult> CancelLicense(Guid id)
        {
            var result = await _mediator.Send(new CancelSoftwareLicenseCommand(id));
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok();
        }

        [HttpPut("license/{id}/extend")]
        public async Task<IActionResult> ExtendLicense(Guid id, [FromBody] DateTime newValidTo)
        {
            var command = new ExtendSoftwareLicenseCommand(id, newValidTo);
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        }
    }
}
