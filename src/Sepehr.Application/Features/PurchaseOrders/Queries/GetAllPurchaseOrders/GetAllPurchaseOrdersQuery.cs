using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders
{
    public class GetAllPurchaseOrdersQuery : IRequest<PagedResponse<IEnumerable<PurchaseOrderViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<int>? InvoiceTypeId { get; set; }
        public int? PurchaseOrderStatusId { get; set; }
        public bool? IsNotTransferedToWarehouse { get; set; }
        public long? OrderCode { get; set; }
    }
    public class GetAllPurchaseOrdersQueryHandler :
         IRequestHandler<GetAllPurchaseOrdersQuery, PagedResponse<IEnumerable<PurchaseOrderViewModel>>>
    {
        private readonly IPurchaseOrderRepositoryAsync _purchaseOrderRepository;
        private readonly IMapper _mapper;
        public GetAllPurchaseOrdersQueryHandler(IPurchaseOrderRepositoryAsync purchaseOrderRepository, IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<PurchaseOrderViewModel>>> Handle(GetAllPurchaseOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllPurchaseOrdersParameter>(request);
                var purchaseOrder = await _purchaseOrderRepository.GetAllPurchaseOrdersAsync(validFilter);

                var purchaseOrderViewModel = _mapper.Map<IEnumerable<PurchaseOrderViewModel>>(
                    purchaseOrder.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<PurchaseOrderViewModel>>(purchaseOrderViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize, purchaseOrder.Count());
            }
            catch (Exception e)
            {

                throw;
            }         
        }   
    }
}