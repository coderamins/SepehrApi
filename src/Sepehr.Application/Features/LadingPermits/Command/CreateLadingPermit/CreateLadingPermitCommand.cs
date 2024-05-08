using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.LadingPermit;
using Sepehr.Application.DTOs.LadingPermits;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.LadingPermits.Command.CreateLadingPermit
{
    public partial class CreateLadingPermitCommand : IRequest<Response<LadingPermit>>
    {
        public required Guid CargoAnnounceId { get; set; }
        //public string? Description { get; set; }

       // public List<LadingPermitDetailDto> LadingPermitDetails { get; set; }
    }
    public class CreateLadingPermitCommandHandler : IRequestHandler<CreateLadingPermitCommand, Response<LadingPermit>>
    {
        private readonly ICargoAnnouncementRepositoryAsync _cargoAnncRepo;
        private readonly ILadingPermitRepositoryAsync _ladingPermitRepository;
        private readonly IProductInventoryRepositoryAsync _productInventory;
        private readonly IMapper _mapper;
        public CreateLadingPermitCommandHandler(ILadingPermitRepositoryAsync ladingPermitRepository,
            IProductInventoryRepositoryAsync productInventory,
            ICargoAnnouncementRepositoryAsync cargoAnncRepo,
            IMapper mapper)
        {
            _ladingPermitRepository = ladingPermitRepository;
            _productInventory = productInventory;
            _cargoAnncRepo = cargoAnncRepo;
            _mapper = mapper;
        }

        public async Task<Response<LadingPermit>> Handle(CreateLadingPermitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cargoAnnc = await _cargoAnncRepo.GetByIdAsync(request.CargoAnnounceId);
                if(cargoAnnc == null) { throw new ApiException("بارنامه یافت نشد !"); }

                var checkExists = await _cargoAnncRepo.GetCargoAnnounceInfo(request.CargoAnnounceId);
                if (checkExists.LadingPermits.Count()!=0)
                    throw new ApiException("مجوز بارگیری برای این بارنامه قبلا ثبت شده است !");

                var ladingPermit = _mapper.Map<LadingPermit>(request);
                await _ladingPermitRepository.AddAsync(ladingPermit);
                cargoAnnc.HasLadingPermit = true;
                await _cargoAnncRepo.UpdateAsync(cargoAnnc);

                return new Response<LadingPermit>(ladingPermit, 
                    new ErrorMessageFactory()
                    .MakeError("مجوز بارگیری", ErrorType.CreatedSuccess));
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}