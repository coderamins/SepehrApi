using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Personnels.Command.CreatePersonnel;
using Sepehr.Application.Features.Personnels.Command.DeletePersonnelById;
using Sepehr.Application.Features.Personnels.Command.UpdatePersonnel;
using Sepehr.Application.Features.Personnels.Queries.GetAllPersonnels;
using Sepehr.Application.Features.Personnels.Queries.GetPersonnelById;
using Sepehr.Application.Helpers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class PersonnelController : BaseApiController
    {
        [HasPermission("GetAllPersonnels")]
        [SwaggerOperation("ReportType= ReportByPurchaseHistory=1,\r\nByLabelId=2,\r\nBothOfThem")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPersonnelsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllPersonnelsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    PersonnelCode=filter.PersonnelCode,
                    PersonnelName =filter.PersonnelName,
                    PhoneNumber=filter.PhoneNumber,
                    NationalCode=filter.NationalCode,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetPersonnelById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetPersonnelByIdQuery { Id = id }));
        }

        // POST api/<controller>
        //[HasPermission("CreatePersonnel")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreatePersonnelCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdatePersonnel")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePersonnelCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeletePersonnel")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeletePersonnelByIdCommand { Id = id }));
        }

    }
}
