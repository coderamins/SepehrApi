using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Brands.Command.CreateBrand;
using Sepehr.Application.Features.Brands.Command.DeleteBrandById;
using Sepehr.Application.Features.Brands.Command.UpdateBrand;
using Sepehr.Application.Features.Brands.Queries.GetAllBrands;
using Sepehr.Application.Features.Brands.Queries.GetBrandById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class AttachmentController : BaseApiController
    {

        // GET api/<controller>/5
        [HasPermission("GetAttachmentById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetBrandByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateBrand")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateBrandCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateBrand")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateBrandCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteBrand")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteBrandByIdCommand { Id = id }));
        }


    }
}
