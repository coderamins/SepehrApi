using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.ApprovePersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.CreatePersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.DeletePersonnelPaymentRequestById;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.ProceedPersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.RejectPersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Command.UpdatePersonnelPaymentRequest;
using Sepehr.Application.Features.PersonnelPaymentRequests.Queries.GetAllPersonnelPaymentRequests;
using Sepehr.Application.Features.PersonnelPaymentRequests.Queries.GetPersonnelPaymentRequestById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class PersonnelPaymentRequestController : BaseApiController
    {

        [HasPermission("GetAllPersonnelPaymentRequests")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPersonnelPaymentRequestsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllPersonnelPaymentRequestsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    PaymentRequestCoode = filter.PaymentRequestCoode,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetPersonnelPaymentRequestById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetPersonnelPaymentRequestByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreatePersonnelPaymentRequest")]
        [HttpPost]
        public async Task<IActionResult> Post(CreatePersonnelPaymentRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdatePersonnelPaymentRequest")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePersonnelPaymentRequestCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("ApprovePersonnelPaymentRequest")]
        [HttpPut("ApprovePersonnelPaymentRequest")]
        public async Task<IActionResult> ApprovePersonnelPaymentRequest(ApprovePersonnelPaymentRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("ProceedToPersonnelPaymentRequest")]
        [HttpPut("ProceedToPersonnelPaymentRequest")]
        public async Task<IActionResult> ProceedToPersonnelPaymentRequest(ProceedToPersonnelPaymentRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("RejectPersonnelPaymentRequest")]
        [HttpPut("RejectPersonnelPaymentRequest")]
        public async Task<IActionResult> RejectPersonnelPaymentRequest(RejectPersonnelPaymentRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeletePersonnelPaymentRequest")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeletePersonnelPaymentRequestByIdCommand { Id = id }));
        }


    }
}
