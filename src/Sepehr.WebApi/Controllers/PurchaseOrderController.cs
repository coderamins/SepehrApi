using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.DTOs.TransferRemittanceUnloadingPermit;
using Sepehr.Application.Features.DriverFareAmounts;
using Sepehr.Application.Features.Orders.Command.ApproveInvoiceType;
using Sepehr.Application.Features.Orders.Command.ConfirmPurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Command.CreatePurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Command.DeletePurchaseOrderById;
using Sepehr.Application.Features.PurchaseOrders.Command.TransferPurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Command.UpdatePurchaseOrder;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetPurchaseOrderById;
using Sepehr.Application.Features.TransferRemittances.Command.CreateTransferRemittance;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceEntrancePermission;
using Sepehr.Application.Features.TransferRemittances.Command.UpdateTransferRemittance;
using Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances;
using Sepehr.Application.Features.TransferRemittances.Queries.GetTransferRemittanceById;
using Sepehr.Infrastructure.Authentication;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class PurchaseOrderController : BaseApiController
    {
        [HasPermission("GetAllPurchaseOrders")]
        [SwaggerOperation("IsNotTransferedToWarehouse= با ست کردن این پارامتر به مقدار true میتوانید لیست سفارشات انتقال داده نشده به انبار را ببینید")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllPurchaseOrdersParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllPurchaseOrdersQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    InvoiceTypeId = filter.InvoiceTypeId,
                    PurchaseOrderStatusId = filter.PurchaseOrderStatusId,
                    IsNotTransferedToWarehouse = filter.IsNotTransferedToWarehouse,
                    OrderCode=filter.OrderCode
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetPurchaseOrderById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetPurchaseOrderByIdQuery { Id = id }));
        }

        // GET api/<controller>/5
        [HasPermission("GetPurchaseOrderInfo")]
        [HttpGet("GetPurchaseOrderInfo/{purchaseOrderCode}")]
        public async Task<IActionResult> GetPurchaseOrderInfo(long purchaseOrderCode)
        {
            return Ok(await Mediator.Send(new GetPurchaseOrderByCodeQuery { purchaseOrderCode = purchaseOrderCode }));
        }

        // POST api/<controller>
        [HasPermission("CreatePurchaseOrder")]
        [HttpPost]
        public async Task<IActionResult> Post(CreatePurchaseOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // POST api/<controller>
        [HasPermission("ApprovePurchaseOrder")]
        [HttpPost("ApprovePurchaseOrder")]
        public async Task<IActionResult> ApprovePurchaseOrder(ConfirmPurchaseOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdatePurchaseOrder")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdatePurchaseOrderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        //[HttpPut("ConfirmPurchaseOrder/{id}")]
        //[Authorize]
        //public async Task<IActionResult> ConfirmPurchaseOrder(Guid id, ConfirmPurchaseOrderCommand command)
        //{
        //    if (id != command.PurchaseOrderId)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(await Mediator.Send(command));
        //}

        // DELETE api/<controller>/5

        [HasPermission("DeletePurchaseOrder")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeletePurchaseOrderByIdCommand { Id = id }));
        }

        [HttpPut("ApproveInvoiceType")]
        public async Task<IActionResult> ApproveInvoiceType(ApprovePurchaseOrderInvoiceTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        
        // POST api/<controller>
        [HasPermission("PurchaseOrderTransfer")]
        [HttpPost("PurchaseOrderTransfer")]
        public async Task<IActionResult> PurchaseOrderTransfer(TransferPurchaseOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        // POST api/<controller>
        [SwaggerOperation("ثبت حواله انتقال")]
        [HasPermission("TransferRemittance")]
        [HttpPost("TransferRemittance")]
        public async Task<IActionResult> TransferRemittance(CreateTransferRemittanceCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [SwaggerOperation("ویرایش حواله انتقال")]
        [HttpPut("UpdateTransferRemittance/{id}")]
        public async Task<IActionResult> Put(int id, UpdateTransferRemittanceCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("GetAllTransferRemittances")]
        [SwaggerOperation("لیست حواله های انتقال")]
        [HttpGet("GetAllTransferRemittances")]
        public async Task<IActionResult> GetAllTransferRemittances([FromQuery] GetAllTransferRemittancesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllTransferRemittancesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    Id = filter.Id,
                    RegisterDate = filter.RegisterDate,
                    OriginWarehouseId = filter.OriginWarehouseId,
                    DestinationWarehouseId = filter.DestinationWarehouseId,
                    TransferEntransePermitNo=filter.TransferEntransePermitNo,
                    TransferRemittStatusId=filter.TransferRemittStatusId,
                    IsEntranced =filter.IsEntranced,
                    MarketerId=filter.MarketerId,
                }));
        }

        [HasPermission("GetTransferRemittanceById")]
        [SwaggerOperation("جزئیات حواله انتقال")]
        [HttpGet("GetTransferRemittanceById/{Id}")]
        public async Task<IActionResult> GetTransferRemittanceById([FromRoute] int Id)
        {
            return Ok(await Mediator
                .Send(new GetTransferRemittanceByIdQuery()
                {
                    Id = Id
                }));
        }

        [HasPermission("GetTransferRemittanceById")]
        [SwaggerOperation("جزئیات حواله انتقال براساس شماره مجوز")]
        [HttpGet("GetTransferRemittanceByPermitCode/{Id}")]
        public async Task<IActionResult> GetTransferRemittanceByPermitCode([FromRoute] int Id)
        {
            return Ok(await Mediator
                .Send(new GetTransferRemittanceByIdQuery()
                {
                    Id = Id
                }));
        }


        [SwaggerOperation("ثبت مجوز ورود حواله انتقال")]
        [HttpPut("TransferRemittanceEntrancePermission/{id}")]
        [AllowAnonymous]
        //[HasPermission("TransferRemittanceEntrancePermission")]
        public async Task<IActionResult> TransferRemittanceEntrancePermission(
            int id, TransferRemittanceEntrancePermissionCommand command)
        {
            if (id != command.TransferRemittanceId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [SwaggerOperation("ثبت مجوز تخلیه حواله انتقال")]
        [HttpPost("TransferRemittanceUnloadingPermit/{entranceId}")]
        [HasPermission("TransferRemittanceUnloadingPermit")]
        public async Task<IActionResult> TransferRemittanceUnloadingPermit(
            Guid entranceId, PurOrderTransRemittUnloadingPermitCommand command)
        {
            if (entranceId != command.TransferRemittanceEntrancePermitId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [SwaggerOperation("تایید کرایه مجوز تخلیه")]
        [HttpPost("ApprovePurOrderTransRemittFareAmount")]
        [HasPermission("ApprovePurOrderTransRemittFareAmount")]
        public async Task<IActionResult> ApprovePurOrderTransRemittFareAmount(
            ApprovePurOrderTransRemittFareAmountCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
