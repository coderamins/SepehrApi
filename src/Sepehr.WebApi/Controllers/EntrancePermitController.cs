﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.EntrancePermits.Command.CreateEntrancePermit;
using Sepehr.Application.Features.EntrancePermits.Command.DeleteEntrancePermitById;
using Sepehr.Application.Features.EntrancePermits.Command.UpdateEntrancePermit;
using Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits;
using Sepehr.Application.Features.EntrancePermits.Queries.GetEntrancePermitById;
using Sepehr.Application.Features.Incomes.Queries.GetIncomeById;
using Sepehr.Application.Features.OrganizationBanks.Command.CreateOrganizationBank;
using Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances;
using Sepehr.Infrastructure.Authentication;
using Sepehr.WebApi.Controller;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controllers
{
    [ApiVersion("1")]
    public class EntrancePermitController : BaseApiController
    {

        // POST api/<controller>
        [HasPermission("CreateEntrancePermit")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateEntrancePermitCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        // POST api/<controller>
        [HasPermission("UpdateEntrancePermit")]
        [HttpPut]
        public async Task<IActionResult> Put(UpdateEntrancePermitCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // POST api/<controller>
        [HasPermission("UpdateEntrancePermit")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteEntrancePermitByIdCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [SwaggerOperation("لیست مجوز های ورود")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllTransferRemittancesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllEntrancePermitsQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    Id = filter.Id,
                    RegisterDate = filter.RegisterDate,
                    OriginWarehouseId = filter.OriginWarehouseId,
                    DestinationWarehouseId = filter.DestinationWarehouseId,
                    TransferEntransePermitNo = filter.TransferEntransePermitNo,
                    TransferRemittStatusId = filter.TransferRemittStatusId,
                    MarketerId = filter.MarketerId,
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetEntrancePermitById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetEntrancePermitByIdQuery { Id = id }));
        }

    }
}
