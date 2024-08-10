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

namespace Sepehr.Application.Features.UnloadingPermits.Command.RevokeUnloadingPermit
{
    public class RevokeUnloadingPermitCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public class RevokeUnloadingPermitCommandHandler : IRequestHandler<RevokeUnloadingPermitCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IUnloadingPermitRepositoryAsync _unloadingPermitRepository;
            public RevokeUnloadingPermitCommandHandler(IUnloadingPermitRepositoryAsync unloadingPermitRepository, IMapper mapper)
            {
                _unloadingPermitRepository = unloadingPermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(RevokeUnloadingPermitCommand command, CancellationToken cancellationToken)
            {
                var unloadingPermit = await _unloadingPermitRepository.GetByIdAsync(command.Id);

                if (unloadingPermit == null || !unloadingPermit.IsActive)
                    throw new ApiException(new ErrorMessageFactory()
                        .MakeError("مجوز تخلیه", ErrorType.NotFound));
                else
                {
                    unloadingPermit.IsActive = false;
                    await _unloadingPermitRepository.UpdateAsync(unloadingPermit);

                    return new Response<string>("مجوز تخلیه با موفقیت ابطال شد !");
                }
            }
        }
    }
}