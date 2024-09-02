using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.TransferWarehouseInventories.Command.CreateTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventories.Command.UpdateTransferWarehouseInventory;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories;
using Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetTransferWarehouseInventoryById;
using Sepehr.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class TransferWarehouseInventoryController : BaseApiController
    {
        [HasPermission("GetAllTransferWarehouseInventories")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllTransferWarehouseInventoriesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllTransferWarehouseInventoriesQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    Id=filter.Id,
                    OriginWarehouseId=filter.OriginWarehouseId,
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


        [HasPermission("DeleteTransferInventory")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteTransferInventoryCommand { Id = id }));
        }

    }
}
