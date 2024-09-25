using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Customers.Command.AssignCustomerLabel;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Application.Features.Customers.Command.DeleteCustomerById;
using Sepehr.Application.Features.Customers.Command.UpdateCustomer;
using Sepehr.Application.Features.Customers.Queries.GetAllCustomers;
using Sepehr.Application.Features.Customers.Queries.GetCustomerBilling;
using Sepehr.Application.Features.Customers.Queries.GetCustomerById;
using Sepehr.Application.Helpers;
using Serilog;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class CustomerController : BaseApiController
    {
        [HasPermission("GetAllCustomers")]
        [SwaggerOperation("ReportType= ReportByPurchaseHistory=1,\r\nByLabelId=2,\r\nBothOfThem")]
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
                    NationalCode=filter.NationalCode,
                    CustomerLabelId=filter.CustomerLabelId,
                    ReportType=filter.ReportType,
                    Keyword=filter.Keyword
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetCustomerById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCustomerByIdQuery { Id = id }));
        }

        // GET api/<controller>/5
        [HasPermission("GetCustomerBillingReport")]
        [HttpGet("GetCustomerBillingReport")]
        public async Task<IActionResult> GetCustomerBillingReport([FromQuery] GetCustomerBillingParameter filter)
        {
            return Ok(await Mediator.Send(new GetCustomerBillingQuery 
            { 
                DateFilter=filter.DateFilter,
                CustomerId = filter.CustomerId,
                FromDate = filter.FromDate,
                ToDate = filter.ToDate, 
                BillingReportType=filter.BillingReportType,
            }));
        }

        // POST api/<controller>
        [HasPermission("CreateCustomer")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("ImportCustomers")]
        [HttpPost("ImportCustomers")]
        public async Task<IActionResult> ImportCustomers(ImportCustomersCommand command)
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

        [HasPermission("AssignCustomerLabels")]
        [HttpPost("AssignCustomerLabels")]
        public async Task<IActionResult> AssignCustomerLabels(AssignCustomerLabelCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
