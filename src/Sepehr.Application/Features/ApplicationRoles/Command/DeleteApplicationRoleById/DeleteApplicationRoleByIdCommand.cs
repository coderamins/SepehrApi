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

namespace Sepehr.Application.Features.ApplicationRoles.Command.DeleteApplicationRoleById
{
    public class DeleteApplicationRoleByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class
        DeleteApplicationRoleByIdCommandHandler
        : IRequestHandler<DeleteApplicationRoleByIdCommand, Response<bool>>
        {
            private readonly IApplicationRoleRepositoryAsync _applicationRoleRepository;

            public DeleteApplicationRoleByIdCommandHandler(
                IApplicationRoleRepositoryAsync applicationRoleRepository
            )
            {
                _applicationRoleRepository = applicationRoleRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteApplicationRoleByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                try
                {
                    var applicationRole = await _applicationRoleRepository.GetByIdAsync(command.Id);
                    if (applicationRole == null)
                        throw new ApiException($"نقش یافت نشد !");

                    await _applicationRoleRepository.DeleteAsync(applicationRole);
                    return new Response<bool>(true, "نقش با موفقیت حذف شد .");
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
