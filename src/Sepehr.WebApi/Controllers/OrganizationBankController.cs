using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.OrganizationBanks.Command.CreateOrganizationBank;
using Sepehr.Application.Features.OrganizationBanks.Command.DeleteOrganizationBankById;
using Sepehr.Application.Features.OrganizationBanks.Command.UpdateOrganizationBank;
using Sepehr.Application.Features.OrganizationBanks.Queries.GetAllOrganizationBanks;
using Sepehr.Application.Features.OrganizationBanks.Queries.GetOrganizationBankById;
using Sepehr.Application.Helpers;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class OrganizationBankController : BaseApiController
    {

        //[HasPermission("GetAllOrganizationBanks")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator
                .Send(new GetAllOrganizationBanksQuery()));
        }

        // GET api/<controller>/5
        [HasPermission("GetOrganizationBankById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetOrganizationBankByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateOrganizationBank")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrganizationBankCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateOrganizationBank")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateOrganizationBankCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteOrganizationBank")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteOrganizationBankByIdCommand { Id = id }));
        }


    }
}
