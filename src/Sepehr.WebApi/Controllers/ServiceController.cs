using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Services.Command.CreateService;
using Sepehr.Application.Features.Services.Command.DeleteServiceById;
using Sepehr.Application.Features.Services.Command.UpdateService;
using Sepehr.Application.Features.Services.Queries.GetAllServices;
using Sepehr.Application.Features.Services.Queries.GetServiceById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ServiceController : BaseApiController
    {

        [HasPermission("GetAllServices")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllServicesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllServicesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetServiceById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetServiceByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateService")]
        [HttpPost]        
        public async Task<IActionResult> Post(CreateServiceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateService")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateServiceCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteService")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteServiceByIdCommand { Id = id }));
        }


    }
}
