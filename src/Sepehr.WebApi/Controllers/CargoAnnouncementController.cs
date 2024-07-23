using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.CargoAnnouncements.Command.CreateCargoAnnouncement;
using Sepehr.Application.Features.CargoAnnouncements.Command.DeleteCargoAnnouncementById;
using Sepehr.Application.Features.CargoAnnouncements.Command.UpdateCargoAnnouncement;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllNotSendedOrders;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetCargoAnncById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class CargoAnnouncement : BaseApiController
    {
        [HasPermission("GetAllCargoAnnouncements")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCargoAnncsParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllCargoAnncsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    OrderId=filter.OrderId,
                    CustomerId=filter.CustomerId,
                    OrderCode=filter.OrderCode,
                    IsCompletlyLading=filter.IsCompletlyLading,
                    CargoAnnounceNo=filter.CargoAnnounceNo
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetCargoAnnouncementById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCargoAnncByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateCargoAnnouncement")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCargoAnncCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateCargoAnnouncement")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateCargoAnncCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteCargoAnnouncement")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteCargoAnncByIdCommand { Id = id }));
        }

        /// <summary>
        /// لیست سفارشاتی که اعلام بار نداشته اند
        /// </summary>
        /// <returns></returns>
        // DELETE api/<controller>/5
        [HasPermission("GetNotAnnouncedOrders")]
        [HttpGet("GetNotSendedOrders")]
        public async Task<IActionResult> GetNotSendedOrders([FromQuery] GetNotAnnouncedOrdersParameter param)
        {
            return Ok(await Mediator.Send(new GetAllNotSendedOrdersQuery
            {
                OrderCode=param.OrderCode,
            }));
        }

        // PUT api/<controller>/5
        [HasPermission("RevokeCargoAnnouncement")]
        [HttpPut("RevokeCargoAnnouncement/{id}")]
        public async Task<IActionResult> RevokeCargoAnnouncement(Guid id, RevokeCargoAnncCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }


    }
}
