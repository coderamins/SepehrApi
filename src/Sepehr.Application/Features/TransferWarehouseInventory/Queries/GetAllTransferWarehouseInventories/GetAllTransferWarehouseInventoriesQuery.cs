using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.TransferWarehouseInventories.Queries.GetAllTransferWarehouseInventories
{
    public class GetAllTransferWarehouseInventoriesQuery : IRequest<PagedResponse<IEnumerable<TransferWarehouseInventoryViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? Id { get; set; }
        public string? RegisterDate { get; set; }
        public int? OriginWarehouseId { get; set; }
        public int? DestinationWarehouseId { get; set; }
        public bool? IsEntranced { get; set; }
        public int? TransferEntransePermitNo { get; set; }
        public int? TransferRemittStatusId { get; set; }
        /// <summary>
        /// فروشنده
        /// </summary>
        public Guid? MarketerId { get; set; }
    }
    public class GetAllTransferWarehouseInventoriesQueryHandler :
         IRequestHandler<GetAllTransferWarehouseInventoriesQuery, PagedResponse<IEnumerable<TransferWarehouseInventoryViewModel>>>
    {
        private readonly ITransferWarehouseInventoryRepositoryAsync _purchaseOrderRepository;
        private readonly IMapper _mapper;
        public GetAllTransferWarehouseInventoriesQueryHandler(ITransferWarehouseInventoryRepositoryAsync purchaseOrderRepository, IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<TransferWarehouseInventoryViewModel>>> Handle
            (GetAllTransferWarehouseInventoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllTransferWarehouseInventoriesParameter>(request);
                var transferWarehouseInventorys = await _purchaseOrderRepository.GetAllTransferWarehouseInventoriesAsync(validFilter);

                var transferWarehouseInventoryViewModel = _mapper.Map<IEnumerable<TransferWarehouseInventoryViewModel>>(
                    transferWarehouseInventorys.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<TransferWarehouseInventoryViewModel>>(transferWarehouseInventoryViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize, transferWarehouseInventorys.Count());
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}