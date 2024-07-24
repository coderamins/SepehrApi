using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceEntrancePermission
{
    public class TransferRemittanceEntrancePermissionCommand : IRequest<Response<EntrancePermit>>
    {
        public int PurchaseOrderTransferRemittanceId { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }

        public class TransferRemittanceEntrancePermissionHandler : IRequestHandler<TransferRemittanceEntrancePermissionCommand,
            Response<EntrancePermit>>
        {
            private readonly IPurchaseOrderTransferRemittanceRepositoryAsync _purchaseOrderTransferRemittance;
            private readonly IMapper _mapper;
            public TransferRemittanceEntrancePermissionHandler(
                IPurchaseOrderTransferRemittanceRepositoryAsync purchaseOrderTransferRemittance,
                IMapper mapper
                )
            {
                _purchaseOrderTransferRemittance = purchaseOrderTransferRemittance;
                _mapper = mapper;
            }
            public async Task<Response<EntrancePermit>> Handle(TransferRemittanceEntrancePermissionCommand command, CancellationToken cancellationToken)
            {
                var purchaseOrderTransferRemittance = await _purchaseOrderTransferRemittance
                    .TransferRemittanceEntrancePermission(command);

                return new Response<EntrancePermit>(
                    purchaseOrderTransferRemittance, "مجوز ورود با موفقیت صادر گردید .");
            }
        }

    }

}
