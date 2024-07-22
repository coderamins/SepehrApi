using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.LadingPermit;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.LadingExitPermits.Command.UpdateLadingExitPermit
{
    public class CreateLadingExitPermitAttachment : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public List<AttachmentDto>? Attachments { get; set; }

        public class CreateLadingExitPermitAttachmentHandler : IRequestHandler<CreateLadingExitPermitAttachment, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly ILadingExitPermitRepositoryAsync _ladingExitPermitRepository;
            private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;
            public CreateLadingExitPermitAttachmentHandler(
                ILadingExitPermitRepositoryAsync ladingExitPermitRepository,
                ILadingPermitRepositoryAsync ladingPermitRepository,
                IMapper mapper)
            {
                _ladingExitPermitRepository = ladingExitPermitRepository;
                _ladingPermitRepository = ladingPermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<bool>> Handle(CreateLadingExitPermitAttachment command, CancellationToken cancellationToken)
            {
                var ladingExitPermit = await _ladingExitPermitRepository.GetLadingExitPermitInfo(command.Id);
                if (ladingExitPermit == null)
                    throw new ApiException("مجوز خروج یافت نشد !");

                ladingExitPermit = _mapper.Map(command, ladingExitPermit);

                if (ladingExitPermit == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("مجوز خروج", ErrorType.NotFound));
                else
                {
                    await _ladingExitPermitRepository.UpdateLadingExitPermit(ladingExitPermit);
                    return new Response<bool>(true,
                        new ErrorMessageFactory().MakeError("مجوز خروج", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}