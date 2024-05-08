using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Application.Features.ProductSuppliers.Command.CreateProductSupplier;
using Sepehr.Application.Features.ProductSuppliers.Command.DeleteProductSupplierById;
using Sepehr.Application.Features.ProductSuppliers.Command.UpdateProductSupplier;
using Sepehr.Application.Features.ProductSuppliers.Queries.GetAllProducts;
using Sepehr.Application.Features.ProductSuppliers.Queries.GetAllProductSuppliers;
using Sepehr.Application.Features.ProductSuppliers.Queries.GetProductSupplierById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ProductSupplierController : BaseApiController
    {
        [HasPermission("GetAllProductSuppliers")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductSuppliersParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllProductSuppliersQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetProductSupplierById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductSupplierByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateProductSupplier")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductSupplierCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateProductSupplier")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateProductSupplierCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteProductSupplier")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteProductSupplierByIdCommand { Id = id }));
        }
    }
}
