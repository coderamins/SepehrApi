using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.CargoAnnounce;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.CargoAnnouncements.Command.UpdateCargoAnnouncement
{
    public class UpdateCargoAnncCommand : IRequest<Response<CargoAnnounce>>
    {
        public Guid Id { get; set; }
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public string CarPlaque { get; set; } = string.Empty;
        public string DriverMobile { get; set; } = string.Empty;
        public decimal FareAmount { get; set; }
        public bool IsComplete { get; set; }
        public int VehicleTypeId { get; set; }
        public string ShippingName { get; set; } = string.Empty;
        public string DeliveryDate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public required List<CargoAnnounceDetailDto> CargoAnnounceDetails { get; set; }

        public class UpdateCargoAnncCommandHandler : IRequestHandler<UpdateCargoAnncCommand, Response<CargoAnnounce>>
        {
            private readonly ICargoAnnouncementRepositoryAsync _cargoAnncRepository;
            private readonly IMapper _mapper;

            public UpdateCargoAnncCommandHandler(ICargoAnnouncementRepositoryAsync cargoAnncRepository, IMapper mapper)
            {
                _cargoAnncRepository = cargoAnncRepository;
                _mapper = mapper;
            }
            public async Task<Response<CargoAnnounce>> Handle(UpdateCargoAnncCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var cargoAnnc = await _cargoAnncRepository.GetByIdAsync(command.Id);

                    if (cargoAnnc == null)
                    {
                        throw new ApiException($"اعلام بار یاقت نشد !");
                    }
                    if(cargoAnnc.HasLadingPermit)
                        throw new ApiException("مجوز بارگیری برای بارنامه صادر شده و امکان ابطال وجود ندارد !");
                    else
                    {
                        cargoAnnc = _mapper.Map(command, cargoAnnc);

                        await _cargoAnncRepository.UpdateCargoAnncAsync(cargoAnnc);
                        return new Response<CargoAnnounce>(cargoAnnc, "");
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}