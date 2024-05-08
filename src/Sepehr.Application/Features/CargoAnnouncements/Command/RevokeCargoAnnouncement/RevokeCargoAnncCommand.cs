using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.CargoAnnounce;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.CargoAnnouncements.Command.UpdateCargoAnnouncement
{
    public class RevokeCargoAnncCommand : IRequest<Response<CargoAnnounce>>
    {
        public Guid Id { get; set; }

        public class RevokeCargoAnncCommandHandler : IRequestHandler<RevokeCargoAnncCommand, Response<CargoAnnounce>>
        {
            private readonly ICargoAnnouncementRepositoryAsync _cargoAnncRepository;
            private readonly IMapper _mapper;

            public RevokeCargoAnncCommandHandler(ICargoAnnouncementRepositoryAsync cargoAnncRepository, IMapper mapper)
            {
                _cargoAnncRepository = cargoAnncRepository;
                _mapper = mapper;
            }
            public async Task<Response<CargoAnnounce>> Handle(RevokeCargoAnncCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var cargoAnnc = await _cargoAnncRepository.GetCargoAnnounceInfo(command.Id);

                    if (cargoAnnc == null)
                    {
                        throw new ApiException($"اعلام بار یاقت نشد !");
                    }
                    else if (cargoAnnc.LadingPermits.Count() != 0)
                        throw new ApiException("مجوز بارگیری برای بارنامه صادر شده و امکان ابطال وجود ندارد !");
                    else
                    {
                        cargoAnnc = _mapper.Map(command, cargoAnnc);
                        cargoAnnc.IsActive = false;

                        await _cargoAnncRepository.UpdateAsync(cargoAnnc);
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