using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.CustomerLabels.Command.CreateCustomerLabel;
using Sepehr.Application.Features.CustomerLabels.Command.DeleteCustomerLabelById;
using Sepehr.Application.Features.CustomerLabels.Command.UpdateCustomerLabel;
using Sepehr.Application.Features.CustomerLabels.Queries.GetAllCustomerLabels;
using Sepehr.Application.Features.CustomerLabels.Queries.GetCustomerLabelById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class CustomerLabelController : BaseApiController
    {

        [HasPermission("GetAllCustomerLabels")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator
                .Send(new GetAllCustomerLabelsQuery()));
        }

        // GET api/<controller>/5
        [HasPermission("GetCustomerLabelById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCustomerLabelByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateCustomerLabel")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerLabelCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateCustomerLabel")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCustomerLabelCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteCustomerLabel")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteCustomerLabelByIdCommand { Id = id }));
        }


    }
}
