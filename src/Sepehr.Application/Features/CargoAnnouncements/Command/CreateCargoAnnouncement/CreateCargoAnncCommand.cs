using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.Style;
using Sepehr.Application.DTOs.CargoAnnounce;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.CargoAnnouncements.Command.CreateCargoAnnouncement
{
    public partial class CreateCargoAnncCommand : IRequest<Response<CargoAnnounce>>
    {
        public Guid OrderId { get; set; }
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public string CarPlaque { get; set; }=string.Empty;
        public string DriverMobile { get; set; }=string.Empty;
        public string ApprovedUserName { get; set; }=string.Empty;
        public decimal FareAmount { get; set; }
        public bool IsComplete { get; set; }
        public int? VehicleTypeId { get; set; }
        public string ShippingName { get; set; }=string.Empty;
        public string DeliveryDate { get; set; }=string.Empty;
        public string Description { get; set; }=string.Empty;

        public required List<CargoAnnounceDetailDto> CargoAnnounceDetails { get; set; }

    }
    public class CreateCargoAnncCommandHandler : IRequestHandler<CreateCargoAnncCommand, Response<CargoAnnounce>>
    {
        private readonly ILogger<CreateCargoAnncCommandHandler> _logger;
        private readonly ICargoAnnouncementRepositoryAsync _cargoAnncRepository;
        private readonly IOrderRepositoryAsync _orderRep;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;
        public CreateCargoAnncCommandHandler(
            ILogger<CreateCargoAnncCommandHandler> logger,
            ICargoAnnouncementRepositoryAsync cargoAnncRepository, 
            IOrderRepositoryAsync orderRepository, 
            IMapper mapper,ISmsService smsService)
        {
            _cargoAnncRepository = cargoAnncRepository;
            _orderRep = orderRepository;
            _mapper = mapper;
            _smsService = smsService;
            _logger= logger;
        }

        public async Task<Response<CargoAnnounce>> Handle(CreateCargoAnncCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRep.GetOrderById(request.OrderId);

                //---محاسیه مقادیر بارگیری شده
                var ladingPermitSummery = order.CargoAnnounces == null ? 0 : 
                    order.CargoAnnounces.Sum(a => a.CargoAnnounceDetails.Sum(l => l.LadingAmount));

                if (order.OrderStatusId== (int)OrderStatusEnum.Sended || 
                    (order.CargoAnnounces!=null && order.Details.Sum(od => od.ProximateAmount) == ladingPermitSummery))
                    throw new ApiException("ارسال سفارش تکمیل شده است !");

                var cargoAnnc = _mapper.Map<CargoAnnounce>(request);

                if (request.IsComplete)
                    order.OrderStatusId = (int)OrderStatusEnum.Sended;

                await _orderRep.UpdateAsync(order);
                await _cargoAnncRepository.AddAsync(cargoAnnc);
                return new Response<CargoAnnounce>(cargoAnnc, $"اعلام بار با موفقیت ثبت شد .");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }
    }
}