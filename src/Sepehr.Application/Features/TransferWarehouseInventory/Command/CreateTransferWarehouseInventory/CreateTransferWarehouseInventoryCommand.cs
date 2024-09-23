using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Product;
using Sepehr.Application.DTOs.TransferWarehouseInventory;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.TransferWarehouseInventories.Command.CreateTransferWarehouseInventory
{
    public partial class CreateTransferWarehouseInventoryCommand : IRequest<Response<TransferWarehouseInventory>>
    {
        public int OriginWarehouseId { get; set; }
        public Guid PurchaseOrderId { get; set; }
        public List<TransferWarehouseInventoryDetailDto> Details { get; set; }

    }

    public class CreateTransferWarehouseInventoryCommandHandler : IRequestHandler<CreateTransferWarehouseInventoryCommand, Response<TransferWarehouseInventory>>
    {
        private readonly ITransferWarehouseInventoryRepositoryAsync _transferRemitRepRepository;
        private readonly IMapper _mapper;
        public CreateTransferWarehouseInventoryCommandHandler(ITransferWarehouseInventoryRepositoryAsync transferRemitRepRepository, IMapper mapper)
        {
            _transferRemitRepRepository = transferRemitRepRepository;
            _mapper = mapper;
        }

        public async Task<Response<TransferWarehouseInventory>> Handle(CreateTransferWarehouseInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transferInventory = _mapper.Map<TransferWarehouseInventory>(request);

                var newTransRemittance = await _transferRemitRepRepository
                    .CreateTransferWarehouseInventory(transferInventory);

                return new Response<TransferWarehouseInventory>(newTransRemittance, "ثبت انتقال با موفقیت انجام شد .");
            }
            catch (Exception r)
            {

                throw;
            }
        }

    }
}