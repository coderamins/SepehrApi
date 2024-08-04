using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Product;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.TransferRemittances.Command.CreateTransferRemittance
{
    public partial class CreateTransferRemittanceCommand : IRequest<Response<TransferRemittance>>
    {
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public int TransferRemittanceTypeId { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string ShippingName { get; set; } = string.Empty;
        public string CarPlaque { get; set; } = string.Empty;
        public int? VehicleTypeId { get; set; }
        public string DriverMobile { get; set; } = string.Empty;
        public string DeliverDate { get; set; } = string.Empty;
        public decimal? FareAmount { get; set; }
        public decimal? OtherCosts { get; set; }
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public string? Description { get; set; }

        public required IEnumerable<TransferRemittanceDetailDto> Details { get; set; }

    }

    public class CreateTransferRemittanceCommandHandler : IRequestHandler<CreateTransferRemittanceCommand, Response<TransferRemittance>>
    {
        private readonly ITransferRemittanceRepositoryAsync _transferRemitRepRepository;
        private readonly IMapper _mapper;
        public CreateTransferRemittanceCommandHandler(ITransferRemittanceRepositoryAsync transferRemitRepRepository, IMapper mapper)
        {
            _transferRemitRepRepository = transferRemitRepRepository;
            _mapper = mapper;
        }

        public async Task<Response<TransferRemittance>> Handle(CreateTransferRemittanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var transRemittance = _mapper.Map<TransferRemittance>(request);
                var newTransRemittance = await _transferRemitRepRepository.CreateTransferRemittance(transRemittance);
                return new Response<TransferRemittance>(newTransRemittance, "ثبت حواله با موفقیت انجام شد .");
            }
            catch (Exception r)
            {

                throw;
            }
        }

    }
}