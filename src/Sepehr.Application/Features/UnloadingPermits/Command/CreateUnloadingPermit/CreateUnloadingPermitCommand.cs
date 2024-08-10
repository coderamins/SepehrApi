using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceUnloadingPermit;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.DTOs.TransferRemittanceUnloadingPermit
{
    public class CreateUnloadingPermitCommand : IRequest<Response<UnloadingPermit>>
    {
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


        public string Description { get; set; } = string.Empty;

        public virtual required List<UnloadingPermitDetailDto> UnloadingPermitDetails { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }

        public class CreateLadingExitPermitCommandHandler : IRequestHandler<CreateUnloadingPermitCommand,
            Response<UnloadingPermit>>
        {
            private readonly IMapper _mapper;
            private readonly ITransferRemittanceRepositoryAsync _purchaseOrderTransferRemittanceRepo;
            private readonly IAttachmentRepositoryAsync _attachmentRepository;
            public CreateLadingExitPermitCommandHandler(
                ITransferRemittanceRepositoryAsync purchaseOrderTransferRemittanceRepo,
                IAttachmentRepositoryAsync attachmentRepository,
                IMapper mapper)
            {
                _purchaseOrderTransferRemittanceRepo = purchaseOrderTransferRemittanceRepo;
                _attachmentRepository = attachmentRepository;
                _mapper = mapper;
            }
            public async Task<Response<UnloadingPermit>> Handle(
                CreateUnloadingPermitCommand command,
                CancellationToken cancellationToken)
            {
                var purchTransferEntraPermit = await _purchaseOrderTransferRemittanceRepo
                    .PurchaseOrderTransRemittEntrancePermitById(command.TransferRemittanceEntrancePermitId);

                if (purchTransferEntraPermit == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("مجوز ورود", ErrorType.NotFound));
                else
                {
                    var purchaseOrderTransferRemittanceUnloadingPermit =
                        _mapper.Map<UnloadingPermit>(command);

                    UnloadingPermit purchaseOrderTransferRemittanceUnloading = await _purchaseOrderTransferRemittanceRepo
                        .CreatePOrderUnloadingPermit(purchaseOrderTransferRemittanceUnloadingPermit);

                    return new Response<UnloadingPermit>(purchaseOrderTransferRemittanceUnloading,
                        new ErrorMessageFactory().MakeError("مجوز تخلیه", ErrorType.UpdatedSuccess));
                }
            }

        }
    }
}
