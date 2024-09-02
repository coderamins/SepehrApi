using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.CustomerOfficialCompanys.Command.CreateCustomerOfficialCompany;
using Sepehr.Application.Features.CustomerOfficialCompanys.Command.DeleteCustomerOfficialCompanyById;
using Sepehr.Application.Features.CustomerOfficialCompanys.Command.UpdateCustomerOfficialCompany;
using Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetAllCustomerOfficialCompanys;
using Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetCustomerOfficialCompanyById;
using Sepehr.Application.Features.ProductPrices.Command.UpdateProductPrice;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Application.Helpers;
using Sepehr.Infrastructure.Persistence.Repositories;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class CustomerOfficialCompanyController : BaseApiController
    {

        [HasPermission("GetAllCustomerOfficialCompanies")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCustomerOfficialCompanysParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllCustomerOfficialCompanysQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    IsActive=filter.IsActive,
                    CustomerId=filter.CustomerId,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetCustomerOfficialCompanyById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCustomerOfficialCompanyByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateCustomerOfficialCompany")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerOfficialCompanyCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateCustomerOfficialCompany")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCustomerOfficialCompanyCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }


        // DELETE api/<controller>/5
        [HasPermission("DeleteCustomerOfficialCompany")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteCustomerOfficialCompanyByIdCommand { Id = id }));
        }


    }
}
