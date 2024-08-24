using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits
{
    public class GetAllEntrancePermitsQuery : IRequest<PagedResponse<IEnumerable<EntrancePermitViewModel>>>
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
        /// ›—Ê‘‰œÂ
        /// </summary>
        public Guid? MarketerId { get; set; }
    }
    public class GetAllEntrancePermitsQueryHandler :
         IRequestHandler<GetAllEntrancePermitsQuery, PagedResponse<IEnumerable<EntrancePermitViewModel>>>
    {
        private readonly IEntrancePermitRepositoryAsync _entrancePermitRepository;
        private readonly IMapper _mapper;
        public GetAllEntrancePermitsQueryHandler(IEntrancePermitRepositoryAsync entrancePermitRepository, IMapper mapper)
        {
            _entrancePermitRepository = entrancePermitRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<EntrancePermitViewModel>>> Handle(GetAllEntrancePermitsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllEntrancePermitsParameter>(request);
                var _entrancePermit = await _entrancePermitRepository.GetAllEntrancePermitsAsync(validFilter);

                var _entrancePermitViewModel = _mapper.Map<IEnumerable<EntrancePermitViewModel>>(
                    _entrancePermit.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<EntrancePermitViewModel>>(_entrancePermitViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize, _entrancePermit.Count());
            }
            catch (Exception e)
            {

                throw;
            }         
        }   
    }
}