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
        public Guid TransferRemittanceEntrancePermitId { get; set; }
        public string DriverAccountNo { get; set; } = string.Empty;
        public string DriverCreditCardNo { get; set; } = string.Empty;
        public decimal? OtherCosts { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public decimal? FareAmount { get; set; }
        public string ShippingName { get; set; } = string.Empty;
        public string Plaque { get; set; } = string.Empty;
        public int? VehicleTypeId { get; set; }
        public string DriverMobile { get; set; } = string.Empty;
        public string DeliverDate { get; set; } = string.Empty;
        public string UnloadingPlaceAddress { get; set; } = string.Empty;

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