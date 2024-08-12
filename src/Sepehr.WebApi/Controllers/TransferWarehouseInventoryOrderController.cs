using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.DTOs.TransferWarehouseInventoryUnloadingPermit;
using Sepehr.Application.Features.DriverFareAmounts;
using Sepehr.Application.Features.Orders.Command.ApproveInvoiceType;
using Sepehr.Application.Features.Orders.Command.ConfirmTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventorys.Command.CreateTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventorys.Command.DeleteTransferWarehouseInventoryById;
using Sepehr.Application.Features.TransferWarehouseInventorys.Command.TransferTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventorys.Command.UpdateTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventorys.Queries.GetAllTransferWarehouseInventorys;
using Sepehr.Application.Features.TransferWarehouseInventorys.Queries.GetTransferWarehouseInventoryById;
using Sepehr.Application.Features.TransferWarehouseInventories.Command.CreateTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventories.Command.TransferWarehouseInventoryEntrancePermission;
using Sepehr.Application.Features.TransferWarehouseInventories.Command.UpdateTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetTransferWarehouseInventoryById;
using Sepehr.Infrastructure.Authentication;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class TransferWarehouseInventoryController : BaseApiController
    {
        [HasPermission("GetAllTransferWarehouseInventorys")]
        [SwaggerOperation("IsNotTransferedToWarehouse= با ست کردن این پارامتر به مقدار true میتوانید لیست سفارشات انتقال داده نشده به انبار را ببینید")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllTransferWarehouseInventoriesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllTransferWarehouseInventoriesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetTransferWarehouseInventoryById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetTransferWarehouseInventoryByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateTransferWarehouseInventory")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateTransferWarehouseInventoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateTransferWarehouseInventory")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateTransferWarehouseInventoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        //[HttpPut("ConfirmTransferWarehouseInventory/{id}")]
        //[Authorize]
        //public async Task<IActionResult> ConfirmTransferWarehouseInventory(Guid id, ConfirmTransferWarehouseInventoryCommand command)
        //{
        //    if (id != command.TransferWarehouseInventoryId)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(await Mediator.Send(command));
        //}

        // DELETE api/<controller>/5

        [HasPermission("DeleteTransferWarehouseInventory")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteTransferWarehouseInventoryByIdCommand { Id = id }));
        }

        [HttpPut("ApproveInvoiceType")]
        public async Task<IActionResult> ApproveInvoiceType(ApproveTransferWarehouseInventoryInvoiceTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        
        // POST api/<controller>
        [HasPermission("TransferWarehouseInventoryTransfer")]
        [HttpPost("TransferWarehouseInventoryTransfer")]
        public async Task<IActionResult> TransferWarehouseInventoryTransfer(TransferTransferWarehouseInventoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        // POST api/<controller>
        [SwaggerOperation("ثبت حواله انتقال")]
        [HasPermission("TransferWarehouseInventory")]
        [HttpPost("TransferWarehouseInventory")]
        public async Task<IActionResult> TransferWarehouseInventory(CreateTransferWarehouseInventoryCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [SwaggerOperation("ویرایش حواله انتقال")]
        [HttpPut("UpdateTransferWarehouseInventory/{id}")]
        public async Task<IActionResult> Put(int id, UpdateTransferWarehouseInventoryCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("GetAllTransferWarehouseInventories")]
        [SwaggerOperation("لیست حواله های انتقال")]
        [HttpGet("GetAllTransferWarehouseInventories")]
        public async Task<IActionResult> GetAllTransferWarehouseInventories([FromQuery] GetAllTransferWarehouseInventoriesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllTransferWarehouseInventoriesQuery()
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

        [HasPermission("GetTransferWarehouseInventoryById")]
        [SwaggerOperation("جزئیات حواله انتقال")]
        [HttpGet("GetTransferWarehouseInventoryById/{Id}")]
        public async Task<IActionResult> GetTransferWarehouseInventoryById([FromRoute] int Id)
        {
            return Ok(await Mediator
                .Send(new GetTransferWarehouseInventoryByIdQuery()
                {
                    Id = Id
                }));
        }

        [HasPermission("GetTransferWarehouseInventoryById")]
        [SwaggerOperation("جزئیات حواله انتقال براساس شماره مجوز")]
        [HttpGet("GetTransferWarehouseInventoryByPermitCode/{Id}")]
        public async Task<IActionResult> GetTransferWarehouseInventoryByPermitCode([FromRoute] int Id)
        {
            return Ok(await Mediator
                .Send(new GetTransferWarehouseInventoryByIdQuery()
                {
                    Id = Id
                }));
        }


        [SwaggerOperation("ثبت مجوز ورود حواله انتقال")]
        [HttpPut("TransferWarehouseInventoryEntrancePermission/{id}")]
        [AllowAnonymous]
        //[HasPermission("TransferWarehouseInventoryEntrancePermission")]
        public async Task<IActionResult> TransferWarehouseInventoryEntrancePermission(
            int id, TransferWarehouseInventoryEntrancePermissionCommand command)
        {
            if (id != command.TransferWarehouseInventoryId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [SwaggerOperation("ثبت مجوز تخلیه حواله انتقال")]
        [HttpPost("TransferWarehouseInventoryUnloadingPermit/{entranceId}")]
        [HasPermission("TransferWarehouseInventoryUnloadingPermit")]
        public async Task<IActionResult> TransferWarehouseInventoryUnloadingPermit(
            Guid entranceId, CreateUnloadingPermitCommand command)
        {
            if (entranceId != command.TransferWarehouseInventoryEntrancePermitId)
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
