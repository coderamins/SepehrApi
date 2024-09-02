using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.ShareHolders.Command.CreateShareHolder;
using Sepehr.Application.Features.ShareHolders.Command.DeleteShareHolderById;
using Sepehr.Application.Features.ShareHolders.Command.UpdateShareHolder;
using Sepehr.Application.Features.ShareHolders.Queries.GetAllShareHolders;
using Sepehr.Application.Features.ShareHolders.Queries.GetShareHolderById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ShareHolderController : BaseApiController
    {
        [HasPermission("GetAllShareHolders")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllShareHoldersParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllShareHoldersQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    ShareHolderCode=filter.ShareHolderCode
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetShareHolderById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetShareHolderByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateShareHolder")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateShareHolderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateShareHolder")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateShareHolderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteShareHolder")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteShareHolderByIdCommand { Id = id }));
        }


    }
}
