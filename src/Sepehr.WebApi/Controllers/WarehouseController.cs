using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Warehouses.Command.CreateWarehouse;
using Sepehr.Application.Features.Warehouses.Command.DeleteWarehouseById;
using Sepehr.Application.Features.Warehouses.Command.UpdateWarehouse;
using Sepehr.Application.Features.Warehouses.Queries.GetAllWarehouses;
using Sepehr.Application.Features.Warehouses.Queries.GetWarehouseById;
using Sepehr.Application.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class WarehouseController : BaseApiController
    {
        //[AllowAnonymous]
        [SwaggerOperation("دریافت لیست انبارها","میتوانید با مشخص کردن نوع انبار فقط انبارهای همان نوع را دریافت کنید")]
        [HasPermission("GetAllWarehouses")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllWarehousesParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllWarehousesQuery{
                    WarehouseTypeId=filter.WarehouseTypeId,
                    CustomerId=filter.CustomerId
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetWarehouseById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetWarehouseByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateProductBran")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateWarehouseCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateWarehouse")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateWarehouseCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteWarehouse")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteWarehouseByIdCommand { Id = id }));
        }


    }
}
