using AutoMapper;
using MediatR;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs
{
    public class GetAllCargoAnncsQuery : IRequest<PagedResponse<IEnumerable<CargoAnncViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? OrderId { get; set; }
        public long? OrderCode { get; set; }
        public Guid? CustomerId { get; set; }
        public bool IsCompletlyLading { get; set; }
    }
    public class GetAllCargoAnncsQueryHandler :
         IRequestHandler<GetAllCargoAnncsQuery, PagedResponse<IEnumerable<CargoAnncViewModel>>>
    {
        private readonly ICargoAnnouncementRepositoryAsync _cargoAnncRepository;
        private readonly IMapper _mapper;
        public GetAllCargoAnncsQueryHandler(ICargoAnnouncementRepositoryAsync cargoAnncRepository, IMapper mapper)
        {
            _cargoAnncRepository = cargoAnncRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<CargoAnncViewModel>>> Handle(GetAllCargoAnncsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllCargoAnncsParameter>(request);
            var cargoAnnouncements = await _cargoAnncRepository.GetAllCargoAnnounceAsync(validFilter);

            var cargoAnncViewModel = _mapper.Map<IEnumerable<CargoAnncViewModel>>(
                    cargoAnnouncements.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());

            return new PagedResponse<IEnumerable<CargoAnncViewModel>>(
                cargoAnncViewModel,
                validFilter.PageNumber,
                validFilter.PageSize,
                cargoAnnouncements.Count());           
        }
    }
}