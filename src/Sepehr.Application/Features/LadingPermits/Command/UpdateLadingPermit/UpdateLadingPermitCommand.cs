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

namespace Sepehr.Application.Features.LadingPermits.Command.UpdateLadingPermit
{
    public class UpdateLadingPermitCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }

        public class UpdateLadingPermitCommandHandler : IRequestHandler<UpdateLadingPermitCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;
            public UpdateLadingPermitCommandHandler(ILadingPermitRepositoryAsync ladingPermitRepository, IMapper mapper)
            {
                _ladingPermitRepository = ladingPermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateLadingPermitCommand command, CancellationToken cancellationToken)
            {
                var ladingPermit = await _ladingPermitRepository.GetByIdAsync(command.Id);
                ladingPermit = _mapper.Map<UpdateLadingPermitCommand, LadingPermit>(command, ladingPermit);

                if (ladingPermit == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("مجوز بارگیری", ErrorType.NotFound));
                else
                {
                    await _ladingPermitRepository.UpdateAsync(ladingPermit);
                    return new Response<string>(ladingPermit.Id.ToString(), new ErrorMessageFactory().MakeError("مجوز بارگیری", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}