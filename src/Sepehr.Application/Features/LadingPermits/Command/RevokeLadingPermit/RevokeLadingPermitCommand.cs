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

namespace Sepehr.Application.Features.LadingPermits.Command.RevokeLadingPermit
{
    public class RevokeLadingPermitCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public class RevokeLadingPermitCommandHandler : IRequestHandler<RevokeLadingPermitCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;
            public RevokeLadingPermitCommandHandler(ILadingPermitRepositoryAsync ladingPermitRepository, IMapper mapper)
            {
                _ladingPermitRepository = ladingPermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(RevokeLadingPermitCommand command, CancellationToken cancellationToken)
            {
                var ladingPermit = await _ladingPermitRepository.GetByIdAsync(command.Id);

                if (ladingPermit == null || !ladingPermit.IsActive)
                    throw new ApiException(new ErrorMessageFactory()
                        .MakeError("مجوز بارگیری", ErrorType.NotFound));
                else
                {
                    ladingPermit.IsActive = false;
                    await _ladingPermitRepository.UpdateAsync(ladingPermit);

                    return new Response<string>("مجوز بارگیری با موفقیت ابطال شد !");
                }
            }
        }
    }
}