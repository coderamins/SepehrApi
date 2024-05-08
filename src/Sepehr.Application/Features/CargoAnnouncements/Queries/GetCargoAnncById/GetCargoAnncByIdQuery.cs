using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.CargoAnnouncements.Queries.GetCargoAnncById
{
    public class GetCargoAnncByIdQuery : IRequest<Response<CargoAnncViewModel>>
    {
        public Guid Id { get; set; }

        public class GetCargoAnncByIdQueryHandler : IRequestHandler<GetCargoAnncByIdQuery, Response<CargoAnncViewModel>>
        {
            private readonly ICargoAnnouncementRepositoryAsync _cargoAnncRepository;
            private readonly IMapper _mapper;
            public GetCargoAnncByIdQueryHandler(
                ICargoAnnouncementRepositoryAsync cargoAnncRepository,
                IMapper mapper
            )
            {
                _cargoAnncRepository = cargoAnncRepository;
                _mapper = mapper;
            }

            public async Task<Response<CargoAnncViewModel>>
            Handle(
                GetCargoAnncByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                try
                {
                    var cargoAnnc = await _cargoAnncRepository.GetCargoAnnounceInfo(query.Id);

                    if (cargoAnnc == null)
                        throw new ApiException($"اعلام بار یافت نشد !");

                    var cargoAnncVM = _mapper.Map<CargoAnncViewModel>(cargoAnnc);

                    return new Response<CargoAnncViewModel>(cargoAnncVM);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}
