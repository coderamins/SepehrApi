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

namespace Sepehr.Application.Features.Personnels.Command.DeletePersonnelById
{
    public class DeletePersonnelByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class
        DeletePersonnelByIdCommandHandler
        : IRequestHandler<DeletePersonnelByIdCommand, Response<Guid>>
        {
            private readonly IPersonnelRepositoryAsync _personnelRepository;
            

            public DeletePersonnelByIdCommandHandler(
                IPersonnelRepositoryAsync personnelRepository                
            )
            {
                _personnelRepository = personnelRepository;                
            }

            public async Task<Response<Guid>>
            Handle(
                DeletePersonnelByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var personnel = await _personnelRepository.GetByIdAsync(command.Id);
                if (personnel == null)
                    throw new ApiException($"مشتری یافت نشد !");

                await _personnelRepository.DeleteAsync(personnel);
                return new Response<Guid>(personnel.Id);
            }
        }
    }
}
