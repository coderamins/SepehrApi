using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.RentPayments.Command.CreateRentPayment;
using Sepehr.Application.Features.RentPayments.Command.DeleteRentPaymentById;
using Sepehr.Application.Features.RentPayments.Command.UpdateRentPayment;
using Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments;
using Sepehr.Application.Features.RentPayments.Queries.GetRentPaymentById;
using Sepehr.Application.Helpers;
using Serilog;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class RentPaymentController : BaseApiController
    {

        [HasPermission("GetAllRentPayments")]
        //[AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllRentPaymentsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllRentPaymentsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    DriverName = filter.DriverName,
                    FromDate = filter.FromDate,
                    ToDate = filter.ToDate,
                    ReferenceCode = filter.ReferenceCode,
                    RentPaymentCode = filter.RentPaymentCode
                }));
        }

        [HasPermission("GetAllRents")]
        [HttpGet("GetAllRents")]
        public async Task<IActionResult> GetAllRents([FromQuery] GetAllRentsToPaymentParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllRentsToPaymentQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    DriverMobile = filter.DriverMobile,
                    DriverName = filter.DriverName,
                    OrderType = filter.OrderType,
                    ReferenceCode = filter.ReferenceCode,
                    FromDate = filter.FromDate,
                    ToDate = filter.ToDate,
                    FareAmountStatusId=filter.FareAmountStatusId,
                }));
        }


        [HasPermission("GetRentByReferenceCode")]
        [HttpGet("GetRentByReferenceCode")]
        public async Task<IActionResult> GetRentByReferenceCode(int refCode)
        {
            return Ok(await Mediator
                .Send(new GetFareAmountByRefCodeQuery()
                {
                    ReferenceCode = refCode
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetRentPaymentById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetRentPaymentByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateRentPayment")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateRentPaymentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateRentPayment")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateRentPaymentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteRentPayment")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteRentPaymentByIdCommand { Id = id }));
        }


    }
}
