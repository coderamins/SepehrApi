using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.CustomerLabels.Queries.GetCustomerLabelById;
using Sepehr.Application.Features.Incomes.Command.CreateIncome;
using Sepehr.Application.Features.Incomes.Command.DeleteIncomeById;
using Sepehr.Application.Features.Incomes.Command.UpdateIncome;
using Sepehr.Application.Features.Incomes.Queries.GetAllIncomes;
using Sepehr.Application.Features.Incomes.Queries.GetIncomeById;
using Sepehr.Infrastructure.Authentication;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class IncomeController : BaseApiController
    {

        [HasPermission("GetAllIncomes")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator
                .Send(new GetAllIncomesQuery()));
        }

        // GET api/<controller>/5
        [HasPermission("GetIncomeById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetCustomerLabelByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HasPermission("CreateIncome")]
        [HttpPost]
        public async Task<IActionResult> Post(CreateIncomeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HasPermission("UpdateIncome")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateIncomeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HasPermission("DeleteIncome")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator
                .Send(new DeleteIncomeByIdCommand { Id = id }));
        }


    }
}
