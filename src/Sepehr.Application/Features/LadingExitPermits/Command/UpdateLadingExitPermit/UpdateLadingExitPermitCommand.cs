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
    public class UpdateLadingExitPermitCommand : IRequest<Response<LadingExitPermit>>
    {
        public Guid Id { get; set; }
        public int LadingPermitId { get; set; }
        public string BankAccountNo { get; set; } = string.Empty;
        public string CreditCardNo { get; set; } = string.Empty;
        public string BankAccountOwnerName { get; set; } = string.Empty;
        public decimal? OtherAmount { get; set; }
        public decimal FareAmount { get; set; }
        public bool? HasExitPermit { get; set; }
        public int? ProductSubUnitId { get; set; }
        public string? ExitPermitDescription { get; set; }

        public required List<LadingExitPermitDetailDto> LadingExitPermitDetails { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }

        public class UpdateLadingExitPermitCommandHandler : IRequestHandler<UpdateLadingExitPermitCommand, Response<LadingExitPermit>>
        {
            private readonly IMapper _mapper;
            private readonly ILadingExitPermitRepositoryAsync _ladingExitPermitRepository;
            private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;
            public UpdateLadingExitPermitCommandHandler(
                ILadingExitPermitRepositoryAsync ladingExitPermitRepository,
                ILadingPermitRepositoryAsync ladingPermitRepository,
                IMapper mapper)
            {
                _ladingExitPermitRepository = ladingExitPermitRepository;
                _ladingPermitRepository = ladingPermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<LadingExitPermit>> Handle(UpdateLadingExitPermitCommand command, CancellationToken cancellationToken)
            {
                var ladingExitPermit = await _ladingExitPermitRepository.GetByIdAsync(command.Id);
                if (ladingExitPermit == null)
                    throw new ApiException("مجوز خروج یافت نشد !");

                ladingExitPermit = _mapper.Map(command, ladingExitPermit);

                if (ladingExitPermit == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("مجوز خروج", ErrorType.NotFound));
                else
                {
                    var result = await _ladingExitPermitRepository.UpdateLadingExitPermit(ladingExitPermit);
                    return new Response<LadingExitPermit>(result,
                        new ErrorMessageFactory().MakeError("مجوز خروج", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}