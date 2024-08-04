using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Application.Features.Customers.Command.DeleteCustomerById;
using Sepehr.Application.Features.Customers.Command.UpdateCustomer;
using Sepehr.Application.Features.Customers.Queries.GetAllCustomers;
using Sepehr.Application.Features.Customers.Queries.GetCustomerById;
using Sepehr.Application.Features.Products.Command.CreateProduct;
using Sepehr.Application.Features.Products.Command.DeleteProductById;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Application.Features.Products.Queries.GetProductById;
using Sepehr.Infrastructure.Authentication;
using Serilog;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class CustomerController : BaseApiController
    {
        [HasPermission("GetAllCustomers")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCustomersParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllCustomersQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    CustomerCode=filter.CustomerCode,
                    CustomerName =filter.CustomerName,
                    PhoneNumber=filter.PhoneNumber,
                    NationalCode=filter.NationalCode
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetCustomerById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCustomerByIdQuery { Id = id }));
        }

        // POST api/<controller>
        //[HasPermission("CreateCustomer")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateCustomer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateCustomerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteCustomer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteCustomerByIdCommand { Id = id }));
        }

        [HasPermission("AllocateCustomerWarehouses")]
        [HttpPost("AllocateCustomerWarehouses")]
        public async Task<IActionResult> AllocateCustomerWarehouses(CustomerWarehousesAllocationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
