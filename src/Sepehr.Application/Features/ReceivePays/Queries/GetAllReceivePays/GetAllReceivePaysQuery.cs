using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.Products.Queries.GetAllProducts;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ReceivePays.Queries.GetAllReceivePays
{
    public class GetAllReceivePaysQuery : IRequest<PagedResponse<IEnumerable<ReceivePayViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FromDate { get; set; }=string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public int? StatusId { get; set; }
        public int? AccountingDocNo { get; set; }
        public long? ReceivePayCode { get; set; }
        public IsApprovalReceivePay? IsApproved { get; set; }
    }
    public class GetAllReceivePayQueryHandler :
         IRequestHandler<GetAllReceivePaysQuery, PagedResponse<IEnumerable<ReceivePayViewModel>>>
    {
        private readonly IReceivePayRepositoryAsync _receivePayRepository;
        private readonly IMapper _mapper;
        public GetAllReceivePayQueryHandler(IReceivePayRepositoryAsync receivePayRepository, IMapper mapper)
        {
            _receivePayRepository = receivePayRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ReceivePayViewModel>>> Handle(
            GetAllReceivePaysQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllReceivePaysParameter>(request);
            var receivePay = await _receivePayRepository.GetAllReceivePays(validFilter);

            var receivePayViewModel = _mapper.Map<IEnumerable<ReceivePayViewModel>>(receivePay);
            return new PagedResponse<IEnumerable<ReceivePayViewModel>>(
                receivePayViewModel, 
                validFilter.PageNumber, 
                validFilter.PageSize);
        }
    }
}