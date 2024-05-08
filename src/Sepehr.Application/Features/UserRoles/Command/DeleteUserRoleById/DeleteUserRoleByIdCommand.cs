using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.UserRoles.Command.DeleteUserRoleById
{
    public class DeleteUserRoleByIdCommand : IRequest<Response<bool>>
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public class
        DeleteUserRoleByIdCommandHandler
        : IRequestHandler<DeleteUserRoleByIdCommand, Response<bool>>
        {
            private readonly IUserRoleRepositoryAsync _userRoleRepository;
            ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteUserRoleByIdCommandHandler(
                IUserRoleRepositoryAsync userRoleRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _userRoleRepository = userRoleRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteUserRoleByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var userRole = await _userRoleRepository.GetUserRoleInfo(command.UserId,command.RoleId);
                if (userRole == null)
                    throw new ApiException($"دسترسی یافت نشد !");

                await _userRoleRepository.DeleteAsync(userRole);
                return new Response<bool>(true);
            }
        }
    }
}
