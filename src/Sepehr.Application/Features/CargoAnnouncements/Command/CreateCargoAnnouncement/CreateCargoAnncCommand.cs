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
    public partial class CreateCargoAnncCommand : IRequest<Response<CargoAnnounce>>
    {
        public Guid OrderId { get; set; }
        public string UnloadingPlaceAddress { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public string CarPlaque { get; set; } = string.Empty;
        public string DriverMobile { get; set; } = string.Empty;
        public string ApprovedUserName { get; set; } = string.Empty;
        public decimal FareAmount { get; set; }
        public bool IsComplete { get; set; }
        public int? VehicleTypeId { get; set; }
        public string ShippingName { get; set; } = string.Empty;
        public string DeliveryDate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public required List<CargoAnnounceDetailDto> CargoAnnounceDetails { get; set; }
        public List<AttachmentDto>? Attachments { get; set; }

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
            IMapper mapper, ISmsService smsService)
        {
            _cargoAnncRepository = cargoAnncRepository;
            _orderRep = orderRepository;
            _mapper = mapper;
            _smsService = smsService;
            _logger = logger;
        }

        public async Task<Response<CargoAnnounce>> Handle(CreateCargoAnncCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRep.GetOrderById(request.OrderId);
                var custAccInfo = await _cargoAnncRepository.GetCustomerAccountInfo(order.CustomerId);
                //if (custAccInfo.CustomerDept > custAccInfo.CustomerCreditor && 
                //    !((decimal)custAccInfo.CustomerCurrentDept <= ((decimal)custAccInfo.CustomerDept * 0.1m)))
                //    throw new ApiException("مشتری دارای بدهی می باشد و امکان صدور بارنامه وجود ندارد !");

                    //---محاسبه مقادیر بارگیری شده
                    //var ladingPermitSummery = order.Details == null ? 0 : 
                    //    order.CargoAnnounces.Sum(a => a.CargoAnnounceDetails.Sum(l => l.LadingAmount));

                    if (order.OrderStatusId == (int)OrderStatusEnum.Sended)
                        throw new ApiException("ارسال سفارش تکمیل شده است !");

                //List<CargoAnnounce> cargoAnnounces = new List<CargoAnnounce>();
                //var orderDetailByWarehouse = order.Details.GroupBy(a => a.WarehouseId).ToList();

                //for (int i = 0; i < orderDetailByWarehouse.Count(); i++)
                //{
                //    var cargoAnnc = _mapper.Map<CargoAnnounce>(request);
                //    List<int> _orderDetails = new List<int>();
                //    foreach (var item in request.CargoAnnounceDetails)
                //    {
                //        var od = await _orderRep.GetOrderDetailInfo(item.OrderDetailId);
                //        if (od == null)
                //            throw new ApiException("خطا در ثبت اعلام بار!");
                //        if (od.CargoAnnounces != null && od.CargoAnnounces.Sum(c => c.LadingAmount) + item.LadingAmount > od.ProximateAmount)
                //            throw new ApiException("مقدار بارگیری نمیتواند بیشتر از مقدار سفارش باشد !");

                //        if (od.WarehouseId == orderDetailByWarehouse[i].Key)
                //            _orderDetails.Add(od.Id);
                //    }

                //    var filteredDetails = cargoAnnc.CargoAnnounceDetails.Where(d => _orderDetails.Contains(d.OrderDetailId));
                //    ICollection<CargoAnnounceDetail> newDetails = filteredDetails.ToList();
                //    cargoAnnc.CargoAnnounceDetails = newDetails;
                //    //if(orderDetail.Warehouse.WarehouseTypeId==(int)EWarehouseType.Vaseteh)
                //    //{
                //    //    cargoAnnc.LadingPermits.Add(new LadingPermit
                //    //    {
                //    //        HasExitPermit = false,
                //    //        IsActive = true,
                //    //        Description = "کالا از نوع واسطه بوده و بصورت خودکار مجوز بارگیری صادر شد"
                //    //    });
                //    //}

                //    cargoAnnounces.Add(cargoAnnc);
                //}

                //var cargoAnnc = _mapper.Map<CargoAnnounce>(request);

                //----اگر کاربر ثبت کننده این مقدار را تکمیل شده بفرستد وضعیت سفارش به بارگیری تکمیل شده یا ارسال کامل تبدیل خواهد شد 
                if (request.IsComplete)
                    order.OrderStatusId = (int)OrderStatusEnum.Sended;

                //await _orderRep.UpdateAsync(order);

                var cargoAnnc = _mapper.Map<CargoAnnounce>(request);

                await _cargoAnncRepository.CreateCargoAnnounce(cargoAnnc,order);
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