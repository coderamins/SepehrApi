﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Orders.Command.ApproveInvoiceType;
using Sepehr.Application.Features.Orders.Command.ConfirmOrder;
using Sepehr.Application.Features.Orders.Command.CreateOrder;
using Sepehr.Application.Features.Orders.Command.DeleteOrderById;
using Sepehr.Application.Features.Orders.Command.ReturnOrder;
using Sepehr.Application.Features.Orders.Command.UpdateOrder;
using Sepehr.Application.Features.Orders.Queries.GetAllOrders;
using Sepehr.Application.Features.Orders.Queries.GetExitPermissionReport;
using Sepehr.Application.Features.Orders.Queries.GetOrderById;
using Sepehr.Domain.Entities;
using Sepehr.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        [HasPermission("GetAllOrders")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllOrdersParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllOrdersQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    InvoiceTypeId=filter.InvoiceTypeId,
                    OrderStatusId=filter.OrderStatusId,
                    OrderCode=filter.OrderCode,
                    OrderType=filter.OrderType,
                    IsTemporary=filter.IsTemporary,
                    SaleManagerId=filter.SaleManagerId,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetOrderById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery { Id = id }));
        }

        // GET api/<controller>/5
        [HasPermission("GetOrderByOrderCode")]
        [HttpGet("GetOrderInfo/{orderCode}")]
        public async Task<IActionResult> GetOrderInfo(long orderCode)
        {
            return Ok(await Mediator.Send(new GetOrderByCodeQuery { orderCode = orderCode }));
        }

        // POST api/<controller>
        [HasPermission("CreateOrder")]
        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post(CreateOrderCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateOrder")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateOrderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("ConfirmOrder")]
        [HttpPut("ConfirmOrder/{id}")]
        public async Task<IActionResult> ConfirmOrder(Guid id, ConfirmOrderCommand command)
        {
            if (id != command.OrderId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteOrder")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteOrderByIdCommand { Id = id }));
        }

        [HasPermission("ApproveOrderInvoiceType")]
        [HttpPut("ApproveInvoiceType")]
        public async Task<IActionResult> ApproveInvoiceType(ApproveInvoiceTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [SwaggerOperation("عدم تایید حسابداری")]
        [HasPermission("RevertOrderInvoiceType")]
        [HttpPut("RevertOrderInvoiceType")]
        public async Task<IActionResult> RevertOrderInvoiceType(RevertInvoiceTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("ReturnOrder")]
        [SwaggerOperation("برگشت سفارش")]
        [HttpPut("ReturnOrder/{id}")]
        public async Task<IActionResult> Put(Guid id, ReturnOrderCommand command)
        {
            if (id != command.OrderId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("ConvertPreSaleOrder")]
        [SwaggerOperation("تبدیل سفارش پیش فروش")]
        [HttpPut("ConvertPreSaleOrder/{id}")]
        public async Task<IActionResult> ConvertPreSaleOrder(Guid id, ConvertPresaleToOrderCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HasPermission("CompleteOrderAnnouncement")]
        [SwaggerOperation("تکمیل اعلام بار")]
        [HttpPut("CompleteAnnouncement/{id}")]
        public async Task<IActionResult> CompleteAnnouncement(Guid id, CompleteAnnouncementCommand command)
        {
            if (id != command.OrderId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [AllowAnonymous]
        //[HasPermission("CompleteOrderAnnouncement")]
        [SwaggerOperation("پرینت مجوز خروج")]
        [HttpGet("GetExitPermissionReport/{id}")]
        public async Task<IActionResult> GetExitPermissionReport(Guid id)
        {
            return Ok(await Mediator.Send(new GetExitPermissionReportQuery { Id=id}));
        }

    }
}
