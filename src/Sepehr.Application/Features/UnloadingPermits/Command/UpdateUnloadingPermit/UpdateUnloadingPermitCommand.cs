using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.UnloadingPermits.Command.UpdateUnloadingPermit
{
    public class UpdateUnloadingPermitCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }

        public class UpdateUnloadingPermitCommandHandler : IRequestHandler<UpdateUnloadingPermitCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IUnloadingPermitRepositoryAsync _unloadingPermitRepository;
            public UpdateUnloadingPermitCommandHandler(IUnloadingPermitRepositoryAsync unloadingPermitRepository, IMapper mapper)
            {
                _unloadingPermitRepository = unloadingPermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateUnloadingPermitCommand command, CancellationToken cancellationToken)
            {
                var unloadingPermit = await _unloadingPermitRepository.GetByIdAsync(command.Id);
                unloadingPermit = _mapper.Map<UpdateUnloadingPermitCommand, UnloadingPermit>(command, unloadingPermit);

                if (unloadingPermit == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("مجوز تخلیه", ErrorType.NotFound));
                else
                {
                    await _unloadingPermitRepository.UpdateAsync(unloadingPermit);
                    return new Response<string>(unloadingPermit.Id.ToString(), new ErrorMessageFactory().MakeError("مجوز تخلیه", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}