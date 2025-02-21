using Crayon.Crayon.Licences.Commands;
using Crayon.Crayon.SoftwareServices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Crayon.Crayon.SoftwareServices
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoftwareServicesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SoftwareServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("services")]
        public async Task<IActionResult> GetServices()
        {
            var result = await _mediator.Send(new GetCCPSoftwareServicesQuery());
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);
            return Ok(result.Data);
        }

        [HttpPost("order")]
        public async Task<IActionResult> OrderSoftware(OrderSoftwareLicenseCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.ErrorMessage);
            return StatusCode(result.StatusCode, result.Data);
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
