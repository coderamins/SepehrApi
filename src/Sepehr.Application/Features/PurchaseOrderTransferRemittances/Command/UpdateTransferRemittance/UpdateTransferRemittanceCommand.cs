using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Product;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.TransferRemittances.Command.UpdateTransferRemittance
{
    public partial class UpdateTransferRemittanceCommand : IRequest<Response<PurchaseOrderTransferRemittance>>
    {
        public int Id { get; set; }
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public int TransferRemittanceTypeId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string ShippingName { get; set; } = string.Empty;
        public string CarPlaque { get; set; } = string.Empty;
        public int? VehicleTypeId { get; set; }
        public string DriverMobile { get; set; } = string.Empty;
        public string DeliverDate { get; set; } = string.Empty;
        public decimal FareAmount { get; set; }
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public decimal? OtherCosts { get; set; }
        public string? Description { get; set; }

        public required IEnumerable<TransferRemittanceDetailDto> Details { get; set; }

    }
    public class UpdateTransferRemittanceCommandHandler : IRequestHandler<UpdateTransferRemittanceCommand, Response<PurchaseOrderTransferRemittance>>
    {
        private readonly IPurchaseOrderTransferRemittanceRepositoryAsync _purchaseOrderTransRepo;
        private readonly IMapper _mapper;
        public UpdateTransferRemittanceCommandHandler(IPurchaseOrderTransferRemittanceRepositoryAsync purchaseOrderRepo, IMapper mapper)
        {
            _purchaseOrderTransRepo = purchaseOrderRepo;
            _mapper = mapper;
        }

        public async Task<Response<PurchaseOrderTransferRemittance>> Handle(UpdateTransferRemittanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transRemitt = await _purchaseOrderTransRepo.GetByIdAsync(request.Id);
                if (transRemitt == null)
                    throw new ApiException("حواله انتقال یافت نشد !");

                if (transRemitt.TransferRemittanceStatusId > 1)
                    throw new ApiException("حواله انتقال امکان ویرایش ندارد !");

                var transRemittance = _mapper.Map(request, transRemitt);

                var updatedTransRemittance = await _purchaseOrderTransRepo.UpdateTransferRemittance(transRemittance);
                return new Response<PurchaseOrderTransferRemittance>(updatedTransRemittance, "ویرایش حواله با موفقیت انجام شد .");
            }
            catch (Exception r)
            {

                throw;
            }
        }

    }
}