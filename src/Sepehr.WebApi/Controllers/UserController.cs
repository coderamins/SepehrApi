﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Sepehr.Application.Features.ApplicationUsers.Command.CreateApplicationUser;
using Sepehr.Application.Features.ApplicationUsers.Command.DeleteApplicationUserById;
using Sepehr.Application.Features.ApplicationUsers.Command.UpdateApplicationUser;
using Sepehr.Application.Features.ApplicationUsers.Queries.GetAllApplicationUsers;
using Sepehr.Application.Features.ApplicationUsers.Queries.GetApplicationUserById;
using Sepehr.Application.Features.ApplicationUsers.Queries.GetLoginedUserInfo;
using Sepehr.Application.Features.Users.Command.ChangePassword;
using Sepehr.Application.Features.Users.Command.ForgetPassword;
using Sepehr.Application.Helpers;
using Serilog;

namespace Sepehr.WebApi.Controller
{
    [ApiVersion("1.0")]
    public class ApplicationUserController : BaseApiController
    {
        [HasPermission("GetAllUsers")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllApplicationUsersParameter filter)
        {
            return Ok(await Mediator
                .Send(new GetAllApplicationUsersQuery()
                {
                    PageSize = filter.PageSize,
                    PageNumber = filter.PageNumber,
                    UserRoles=filter.UserRoles
                }));
        }

        // GET api/<controller>/5
        [HasPermission("GetUserById")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetApplicationUserByIdQuery { Id = id }));
        }

        // GET api/<controller>/5
        //[HasPermission("GetLoginedUserInfo")]
        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            return Ok(await Mediator.Send(new GetLoginedUserInfoQuery()));
        }

        // POST api/<controller>
        [HttpPost]
        [HasPermission("CreateUser")]
        public async Task<IActionResult> Post(CreateApplicationUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [HasPermission("UpdateUser")]
        public async Task<IActionResult> Put(Guid id, UpdateApplicationUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [HasPermission("DeleteUser")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator
                .Send(new DeleteApplicationUserByIdCommand { Id = id }));
        }

        // DELETE api/<controller>/5
        [HttpPost("ForgetPasswordRequest")]
        [EnableRateLimiting("ForgetPassReqLimit")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPasswordRequest(ForgetPasswordRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpPost("ChangePasswordRequest")]
        [EnableRateLimiting("ChangePasswordLimiter")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePasswordRequest(ChangePasswordRequestCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
