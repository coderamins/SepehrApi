using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Login;

namespace Sepehr.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator= mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(LoginCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}
