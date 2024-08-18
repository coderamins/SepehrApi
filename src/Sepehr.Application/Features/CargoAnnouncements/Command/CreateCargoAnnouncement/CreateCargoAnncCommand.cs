using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.Style;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.CargoAnnounce;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.CargoAnnouncements.Command.CreateCargoAnnouncement
{
    public partial class CreateCargoAnncCommand : IRequest<Response<List<CargoAnnounce>>>
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
        public List<AttachmentDto>? Attachments { get; set; }

    }
    public class CreateCargoAnncCommandHandler : IRequestHandler<CreateCargoAnncCommand, Response<List<CargoAnnounce>>>
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

        public async Task<Response<List<CargoAnnounce>>> Handle(CreateCargoAnncCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRep.GetOrderById(request.OrderId);

                //---محاسبه مقادیر بارگیری شده
                //var ladingPermitSummery = order.Details == null ? 0 : 
                //    order.CargoAnnounces.Sum(a => a.CargoAnnounceDetails.Sum(l => l.LadingAmount));

                if (order.OrderStatusId== (int)OrderStatusEnum.Sended)
                    throw new ApiException("ارسال سفارش تکمیل شده است !");

                List<CargoAnnounce> cargoAnnounces = new List<CargoAnnounce>();
                var orderDetailByWarehouse = order.Details.GroupBy(a => a.WarehouseId).Select(x => x.Key);
                foreach (var whouse in orderDetailByWarehouse)
                {
                    foreach (var item in request.CargoAnnounceDetails)
                    {
                        var orderDetail =await _orderRep.GetOrderDetailInfo(item.OrderDetailId);
                        if (orderDetail == null)
                            throw new ApiException("سفارش یافت نشد !");

                        if (orderDetail.CargoAnnounces!=null && 
                            orderDetail.CargoAnnounces.Sum(c => c.LadingAmount) + item.LadingAmount > orderDetail.ProximateAmount)
                            throw new ApiException("مقدار بارگیری نمیتواند بیشتر از مقدار سفارش باشد !");

                        if(orderDetail!=null && orderDetail.WarehouseId==whouse)
                        {
                            var cargoAnnc = _mapper.Map<CargoAnnounce>(request);
                            
                            var filteredDetails = cargoAnnc.CargoAnnounceDetails.Where(d => d.OrderDetailId == orderDetail.Id);
                            ICollection<CargoAnnounceDetail> newDetails = filteredDetails.ToList();
                            cargoAnnc.CargoAnnounceDetails = newDetails;
                            //if(orderDetail.Warehouse.WarehouseTypeId==(int)EWarehouseType.Vaseteh)
                            //{
                            //    cargoAnnc.LadingPermits.Add(new LadingPermit
                            //    {
                            //        HasExitPermit = false,
                            //        IsActive = true,
                            //        Description = "کالا از نوع واسطه بوده و بصورت خودکار مجوز بارگیری صادر شد"
                            //    });
                            //}

                            cargoAnnounces.Add(cargoAnnc);
                        }                       
                    }
                }

                //var cargoAnnc = _mapper.Map<CargoAnnounce>(request);

                //----اگر کاربر ثبت کننده این مقدار را تکمیل شده بفرستد وضعیت سفارش به بارگیری تکمیل شده یا ارسال کامل تبدیل خواهد شد 
                if (request.IsComplete)
                    order.OrderStatusId = (int)OrderStatusEnum.Sended;

                await _orderRep.UpdateAsync(order);                

                await _cargoAnncRepository.AddAsync(cargoAnnounces);
                return new Response<List<CargoAnnounce>>(cargoAnnounces, $"اعلام بار با موفقیت ثبت شد .");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }
    }
}