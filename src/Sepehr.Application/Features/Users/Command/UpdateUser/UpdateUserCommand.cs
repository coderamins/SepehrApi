using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Users;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.ApplicationUsers.Command.UpdateApplicationUser
{
    public class UpdateApplicationUserCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public string Mobile { get; set; } = string.Empty;

        public List<ApplicationUserRoleDto> UserRoles { get; set; } = new List<ApplicationUserRoleDto>();

        public class UpdateApplicationUserCommandHandler : IRequestHandler<UpdateApplicationUserCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationUserRepositoryAsync _applicationUserRepository;
            public UpdateApplicationUserCommandHandler(IApplicationUserRepositoryAsync applicationUserRepository, IMapper mapper)
            {
                _applicationUserRepository = applicationUserRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateApplicationUserCommand command, CancellationToken cancellationToken)
            {
                var applicationUser = await _applicationUserRepository.GetByIdAsync(command.Id);
                applicationUser = _mapper.Map(command, applicationUser);

                if (applicationUser == null)
                {
                    throw new ApiException($"کاربر یافت نشد !");
                }
                else
                {
                    await _applicationUserRepository.UpdateUserAsync(applicationUser);
                    return new Response<string>(applicationUser.Id.ToString(), "");
                }
            }
        }
    }
}