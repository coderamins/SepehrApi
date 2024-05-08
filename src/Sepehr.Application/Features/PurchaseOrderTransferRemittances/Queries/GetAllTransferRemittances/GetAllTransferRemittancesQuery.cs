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

namespace Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances
{
    public class GetAllTransferRemittancesQuery : IRequest<PagedResponse<IEnumerable<TransferRemittanceViewModel>>>
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
    }
    public class GetAllTransferRemittancesQueryHandler :
         IRequestHandler<GetAllTransferRemittancesQuery, PagedResponse<IEnumerable<TransferRemittanceViewModel>>>
    {
        private readonly IPurchaseOrderTransferRemittanceRepositoryAsync _purchaseOrderRepository;
        private readonly IMapper _mapper;
        public GetAllTransferRemittancesQueryHandler(IPurchaseOrderTransferRemittanceRepositoryAsync purchaseOrderRepository, IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<TransferRemittanceViewModel>>> Handle
            (GetAllTransferRemittancesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllTransferRemittancesParameter>(request);
                var transferRemittances = await _purchaseOrderRepository.GetAllTransferRemittancesAsync(validFilter);

                var transferRemittanceViewModel = _mapper.Map<IEnumerable<TransferRemittanceViewModel>>(
                    transferRemittances.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<TransferRemittanceViewModel>>(transferRemittanceViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize, transferRemittances.Count());
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}