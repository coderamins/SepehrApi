using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Sepehr.WebApi.Controller
{
    [ApiController]
    //[EnableCors("CorsPolicy")]
    //[Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        private ILogger<BaseApiController> _logger; 
        //private ILogger<BaseApiController> _logger;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<BaseApiController> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<BaseApiController>>();
        //protected BaseApiController(ILogger<BaseApiController> logger)
        //{
        //    _logger = logger;
        //}
    }
}