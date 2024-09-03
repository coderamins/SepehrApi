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

namespace Sepehr.Application.Features.Permissions.Command.DeletePermissionById
{
    public class DeletePermissionByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class
        DeletePermissionByIdCommandHandler
        : IRequestHandler<DeletePermissionByIdCommand, Response<Guid>>
        {
            private readonly IPermissionRepositoryAsync _permissionRepository;

            public DeletePermissionByIdCommandHandler(
                IPermissionRepositoryAsync permissionRepository
            )
            {
                _permissionRepository = permissionRepository;
            }

            public async Task<Response<Guid>>
            Handle(
                DeletePermissionByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var permission = await _permissionRepository.GetByIdAsync(command.Id);
                if (permission == null)
                    throw new ApiException($"دسترسی یافت نشد !");

                await _permissionRepository.DeleteAsync(permission);
                return new Response<Guid>(permission.Id);
            }
        }
    }
}
