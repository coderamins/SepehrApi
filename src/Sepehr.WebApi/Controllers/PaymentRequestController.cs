using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.PaymentRequests.Command.ApprovePaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Command.CreatePaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Command.DeletePaymentRequestById;
using Sepehr.Application.Features.PaymentRequests.Command.ProceedPaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Command.RejectPaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Command.UpdatePaymentRequest;
using Sepehr.Application.Features.PaymentRequests.Queries.GetAllPaymentRequests;
using Sepehr.Application.Features.PaymentRequests.Queries.GetPaymentRequestById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class PaymentRequestController : BaseApiController
    {

        [HasPermission("GetAllPaymentRequests")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPaymentRequestsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllPaymentRequestsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    PaymentRequestCoode = filter.PaymentRequestCoode,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetPaymentRequestById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetPaymentRequestByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreatePaymentRequest")]
        [HttpPost]
        public async Task<IActionResult> Post(CreatePaymentRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdatePaymentRequest")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePaymentRequestCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("ApprovePaymentRequest")]
        [HttpPut("ApprovePaymentRequest")]
        public async Task<IActionResult> ApprovePaymentRequest(ApprovePaymentRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("ProceedToPaymentRequest")]
        [HttpPut("ProceedToPaymentRequest")]
        public async Task<IActionResult> ProceedToPaymentRequest(ProceedToPaymentRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("RejectPaymentRequest")]
        [HttpPut("RejectPaymentRequest")]
        public async Task<IActionResult> RejectPaymentRequest(RejectPaymentRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeletePaymentRequest")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeletePaymentRequestByIdCommand { Id = id }));
        }


    }
}
