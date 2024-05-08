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

namespace Sepehr.Application.Features.RolePermissions.Command.DeleteRolePermissionById
{
    public class DeleteRolePermissionByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class
        DeleteRolePermissionByIdCommandHandler
        : IRequestHandler<DeleteRolePermissionByIdCommand, Response<Guid>>
        {
            private readonly IRolePermissionRepositoryAsync _rolePermissionRepository;
            ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteRolePermissionByIdCommandHandler(
                IRolePermissionRepositoryAsync rolePermissionRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _rolePermissionRepository = rolePermissionRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<Guid>>
            Handle(
                DeleteRolePermissionByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var rolePermission = await _rolePermissionRepository.GetByIdAsync(command.Id);
                if (rolePermission == null)
                    throw new ApiException($"نقش دسترسی یافت نشد !");

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = rolePermission.Id.ToString(), TableName = "rolePermission" });

                await _rolePermissionRepository.DeleteAsync(rolePermission);
                return new Response<Guid>(rolePermission.Id);
            }
        }
    }
}
