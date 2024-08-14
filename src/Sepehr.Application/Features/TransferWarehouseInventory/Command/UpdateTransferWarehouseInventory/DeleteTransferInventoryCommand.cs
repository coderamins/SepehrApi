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
    public partial class DeleteTransferInventoryCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
    public class DeleteTransferInventoryCommandHandler : IRequestHandler<DeleteTransferInventoryCommand, Response<bool>>
    {
        private readonly ITransferWarehouseInventoryRepositoryAsync _purchaseOrderTransRepo;
        private readonly IMapper _mapper;
        public DeleteTransferInventoryCommandHandler(ITransferWarehouseInventoryRepositoryAsync purchaseOrderRepo, IMapper mapper)
        {
            _purchaseOrderTransRepo = purchaseOrderRepo;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(DeleteTransferInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _purchaseOrderTransRepo.DeleteTransferInventory(request.Id);
                return new Response<bool>(true,"حذف انتقال با موفقیت انجام شد .");
            }
            catch (Exception r)
            {

                throw;
            }
        }

    }
}