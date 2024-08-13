using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Product;
using Sepehr.Application.DTOs.TransferWarehouseInventory;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.TransferWarehouseInventories.Command.UpdateTransferWarehouseInventory
{
    public partial class UpdateTransferWarehouseInventoryCommand : IRequest<Response<TransferWarehouseInventory>>
    {
        public int Id { get; set; }
        public int OriginWarehouseId { get; set; }
        public List<TransferWarehouseInventoryDetailDto> Details { get; set; }

    }
    public class UpdateTransferWarehouseInventoryCommandHandler : IRequestHandler<UpdateTransferWarehouseInventoryCommand, Response<TransferWarehouseInventory>>
    {
        private readonly ITransferWarehouseInventoryRepositoryAsync _purchaseOrderTransRepo;
        private readonly IMapper _mapper;
        public UpdateTransferWarehouseInventoryCommandHandler(ITransferWarehouseInventoryRepositoryAsync purchaseOrderRepo, IMapper mapper)
        {
            _purchaseOrderTransRepo = purchaseOrderRepo;
            _mapper = mapper;
        }

        public async Task<Response<TransferWarehouseInventory>> Handle(UpdateTransferWarehouseInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transRemitt = await _purchaseOrderTransRepo.GetByIdAsync(request.Id);
                if (transRemitt == null)
                    throw new ApiException("رکورد یافت نشد !");


                var transRemittance = _mapper.Map(request, transRemitt);

                var updatedTransRemittance = await _purchaseOrderTransRepo.UpdateTransferWarehouseInventory(transRemittance);
                return new Response<TransferWarehouseInventory>(updatedTransRemittance, "ویرایش انتقال با موفقیت انجام شد .");
            }
            catch (Exception r)
            {

                throw;
            }
        }

    }
}