using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Application.Features.ReceivePays.Command.AccApproveReceivePay;
using Sepehr.Application.Features.ReceivePays.Command.AccRejectReceivePay;
using Sepehr.Application.Features.ReceivePays.Command.CreateReceivePay;
using Sepehr.Application.Features.ReceivePays.Command.DeleteReceivePayById;
using Sepehr.Application.Features.ReceivePays.Command.UpdateReceivePay;
using Sepehr.Application.Features.ReceivePays.Queries.GetAllReceivePays;
using Sepehr.Application.Features.ReceivePays.Queries.GetReceivePayById;
using Sepehr.Infrastructure.Authentication;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ReceivePayController : BaseApiController
    {
        [HasPermission("GetAllReceivePays")]
        [HttpGet]
        [SwaggerOperation("توسط این متد هم میتوان همه رکوردها را دریافت کرد و هم دریافت و پرداخت های تایید حسابداری شده و نشده را(توسط پارامتر IsApproved)")]
        public async Task<IActionResult> Get([FromQuery] GetAllReceivePaysParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllReceivePaysQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    FromDate=filter.FromDate,
                    ToDate=filter.ToDate,
                    StatusId=filter.StatusId,
                    IsApproved=filter.IsApproved,
                    AccountingDocNo=filter.AccountingDocNo,
                    ReceivePayCode=filter.ReceivePayCode
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetReceivePayById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetReceivePayByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateReceivePay")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CreateReceivePayCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateReceivePay")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm]UpdateReceivePayCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteReceivePay")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteReceivePayByIdCommand { Id = id }));
        }

        [HasPermission("ReceivePayApprove")]
        [HttpPut("ReceivePayApprove")]
        public async Task<IActionResult> Put(AccApproveReceivePayCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("ReceivePayAccRegister")]
        [HttpPut("ReceivePayAccRegister")]
        public async Task<IActionResult> ReceivePayAccRegister(AccRegisterReceivePayCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("ReceivePayAccReject")]
        [HttpPut("ReceivePayAccReject")]
        public async Task<IActionResult> ReceivePayAccReject(AccRejectReceivePayCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
