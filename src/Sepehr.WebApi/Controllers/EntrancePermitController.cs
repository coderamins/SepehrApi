using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits;
using Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances;
using Sepehr.Infrastructure.Authentication;
using Sepehr.WebApi.Controller;
using Swashbuckle.AspNetCore.Annotations;

namespace Sepehr.WebApi.Controllers
{
    [ApiVersion("1")]
    public class EntrancePermitController : BaseApiController
    {
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

    }
}
