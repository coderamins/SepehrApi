using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.PurchaseOrderTransferRemittances.Command.TransferRemittanceUnloadingPermit;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.DTOs.TransferRemittanceUnloadingPermit
{
    public class PurchaseOrderTransferRemittanceUnloadingPermitCommand : IRequest<Response<PurchaseOrderTransferRemittanceUnloadingPermit>>
    {
        public Guid PurchaseOrderTransferRemittanceEntrancePermitId { get; set; }
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

        public virtual required List<PurchaseOrderTransferRemittanceUnloadingPermitDetailDto> PurchaseOrderTransferRemittanceUnloadingPermitDetails { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }

        public class CreateLadingExitPermitCommandHandler : IRequestHandler<PurchaseOrderTransferRemittanceUnloadingPermitCommand,
            Response<PurchaseOrderTransferRemittanceUnloadingPermit>>
        {
            private readonly IMapper _mapper;
            private readonly IPurchaseOrderTransferRemittanceRepositoryAsync _purchaseOrderTransferRemittanceRepo;
            private readonly IAttachmentRepositoryAsync _attachmentRepository;
            public CreateLadingExitPermitCommandHandler(
                IPurchaseOrderTransferRemittanceRepositoryAsync purchaseOrderTransferRemittanceRepo,
                IAttachmentRepositoryAsync attachmentRepository,
                IMapper mapper)
            {
                _purchaseOrderTransferRemittanceRepo = purchaseOrderTransferRemittanceRepo;
                _attachmentRepository = attachmentRepository;
                _mapper = mapper;
            }
            public async Task<Response<PurchaseOrderTransferRemittanceUnloadingPermit>> Handle(
                PurchaseOrderTransferRemittanceUnloadingPermitCommand command,
                CancellationToken cancellationToken)
            {
                var purchTransferEntraPermit = await _purchaseOrderTransferRemittanceRepo
                    .PurchaseOrderTransRemittEntrancePermitById(command.PurchaseOrderTransferRemittanceEntrancePermitId);

                if (purchTransferEntraPermit == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("مجوز ورود", ErrorType.NotFound));
                else
                {
                    var purchaseOrderTransferRemittanceUnloadingPermit =
                        _mapper.Map<PurchaseOrderTransferRemittanceUnloadingPermit>(command);

                    PurchaseOrderTransferRemittanceUnloadingPermit purchaseOrderTransferRemittanceUnloading = await _purchaseOrderTransferRemittanceRepo
                        .CreatePurchaseOrderTransferRemittanceUnloadingPermit(purchaseOrderTransferRemittanceUnloadingPermit);

                    return new Response<PurchaseOrderTransferRemittanceUnloadingPermit>(purchaseOrderTransferRemittanceUnloading,
                        new ErrorMessageFactory().MakeError("مجوز تخلیه", ErrorType.UpdatedSuccess));
                }
            }

        }
    }
}
