﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sepehr.Application.Features.Captcha.Command;
using Sepehr.Application.Features.ProductSuppliers.Queries.GetProductSupplierById;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class CaptchaController : BaseApiController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GenerateCaptchaCommand()));
        }

    }
}
