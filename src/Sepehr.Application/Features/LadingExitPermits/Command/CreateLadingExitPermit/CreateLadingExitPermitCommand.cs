using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.CargoExitPermit;
using Sepehr.Application.DTOs.LadingPermit;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.CargoAnnouncements.Command.CreateCargoAnnouncement;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.LadingExitPermits.Command.CreateLadingExitPermit
{
    public partial class CreateLadingExitPermitCommand : IRequest<Response<LadingExitPermit>>
    {
        public int LadingPermitId { get; set; }
        public string BankAccountNo { get; set; } = string.Empty;
        public string CreditCardNo { get; set; } = string.Empty;
        public string BankAccountOwnerName { get; set; } = string.Empty;
        public decimal? OtherAmount { get; set; }
        public decimal FareAmount { get; set; }
        public bool? HasExitPermit { get; set; }
        public string? ExitPermitDescription { get; set; }

        public required List<CreateLadingExitPermitDetailRequest> LadingExitPermitDetails { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }

    }
    public class CreateLadingExitPermitCommandHandler : IRequestHandler<CreateLadingExitPermitCommand, Response<LadingExitPermit>>
    {
        private readonly ILadingPermitRepositoryAsync _ladingPermitRep;
        private readonly ILadingExitPermitRepositoryAsync _ladingExitPermitRepository;
        private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;
        private readonly IMapper _mapper;
        public CreateLadingExitPermitCommandHandler(
            ILadingExitPermitRepositoryAsync ladingExitPermitRepository,
            IMapper mapper,
            ILadingPermitRepositoryAsync ladingPermitRepository,
            ILadingPermitRepositoryAsync ladingPermitRep)
        {
            _ladingExitPermitRepository = ladingExitPermitRepository;
            _ladingPermitRepository = ladingPermitRepository;
            _mapper = mapper;
            _ladingPermitRep = ladingPermitRep;
        }

        public async Task<Response<LadingExitPermit>> Handle(CreateLadingExitPermitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var ladingPermit = await _ladingPermitRepository.GetByIdAsync(request.LadingPermitId);

                if (ladingPermit == null) { throw new ApiException("مجوز بارگیری یافت نشد !"); }
                if (ladingPermit.HasExitPermit == true)
                    throw new ApiException("مجوز خروج بارنامه قبلا ثبت شده است !");

                if (await _ladingPermitRep.GetByIdAsync(request.LadingPermitId) == null)
                    throw new ApiException("مجوز بارگیری یافت نشد !");

                ladingPermit.HasExitPermit = true;

                var ladingExitPermit = _mapper.Map<LadingExitPermit>(request);

                await _ladingExitPermitRepository.CreateLadingExitPermit(ladingExitPermit);

                return new Response<LadingExitPermit>(ladingExitPermit, new ErrorMessageFactory().MakeError("مجوز خروج", ErrorType.CreatedSuccess));
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

    }
}