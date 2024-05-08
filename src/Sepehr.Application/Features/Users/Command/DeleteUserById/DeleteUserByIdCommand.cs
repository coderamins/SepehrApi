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

namespace Sepehr.Application.Features.ApplicationUsers.Command.DeleteApplicationUserById
{
    public class DeleteApplicationUserByIdCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }

        public class DeleteApplicationUserByIdCommandHandler:IRequestHandler<DeleteApplicationUserByIdCommand, Response<string>>
        {
            private readonly IApplicationUserRepositoryAsync _applicationUserRepository;
            ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteApplicationUserByIdCommandHandler(
                IApplicationUserRepositoryAsync applicationUserRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _applicationUserRepository = applicationUserRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<string>> Handle(
                DeleteApplicationUserByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                try
                {
                    var applicationUser = await _applicationUserRepository.GetByIdAsync(command.Id);
                    if (applicationUser == null)
                        throw new ApiException($"کاربر یافت نشد !");

                    await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = applicationUser.Id.ToString(), TableName = "applicationUser" });

                    await _applicationUserRepository.DeleteAsync(applicationUser);
                    return new Response<string>(applicationUser.Id.ToString(), "حذف کاربر با موفقیت انجام شد .");
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
