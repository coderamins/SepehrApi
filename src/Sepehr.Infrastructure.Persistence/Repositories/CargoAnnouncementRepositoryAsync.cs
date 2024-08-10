using Microsoft.EntityFrameworkCore;
using Sepehr.Application.DTOs.Email;
using Sepehr.Application.DTOs.Sms;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class CargoAnnouncementRepositoryAsync : GenericRepositoryAsync<CargoAnnounce>, ICargoAnnouncementRepositoryAsync
    {
        private readonly DbSet<CargoAnnounce> _cargoAnnouncements;
        private readonly DbSet<CargoAnnounceDetail> _cargoAnnounceDetail;
        private readonly DbSet<Order> _order;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;
        private readonly ApplicationDbContext _dbContext;

        public CargoAnnouncementRepositoryAsync(ApplicationDbContext dbContext
            , IEmailService emailService
            , ISmsService smsService) : base(dbContext)
        {
            _cargoAnnouncements = dbContext.Set<CargoAnnounce>();
            _cargoAnnounceDetail = dbContext.Set<CargoAnnounceDetail>();
            _order = dbContext.Set<Order>();
            _emailService = emailService;
            _smsService = smsService;
            _dbContext = dbContext;
        }

        public async Task<IQueryable<CargoAnnounce>> GetAllCargoAnnounceAsync(GetAllCargoAnncsParameter validFilter)
        {
            return _cargoAnnouncements
                .Include(c => c.LadingPermits.Where(l => l.IsActive))
                .Include(ca => ca.Order).ThenInclude(o => o.Details).ThenInclude(od => od.Warehouse)
                .Include(ca => ca.Order).ThenInclude(o => o.Customer).ThenInclude(c => c.CustomerOfficialCompanies)
                .Include(ca => ca.Order).ThenInclude(o => o.OrderStatus)
                .Include(ca => ca.Order).ThenInclude(o => o.InvoiceType)
                .Include(ca => ca.Order).ThenInclude(o => o.FarePaymentType)
                .Include(ca => ca.Order).ThenInclude(o => o.OrderServices)
                .Include(ca => ca.Order).ThenInclude(o => o.OrderSendType)
                .Include(ca => ca.CargoAnnounceDetails).ThenInclude(c => c.OrderDetail).ThenInclude(c => c.Product).ThenInclude(p => p.ProductSubUnit)
                .Include(ca => ca.CargoAnnounceDetails).ThenInclude(c => c.OrderDetail).ThenInclude(c => c.Product).ThenInclude(p => p.ProductMainUnit)
                .Include(ca => ca.CargoAnnounceDetails).ThenInclude(c => c.OrderDetail).ThenInclude(c => c.ProductBrand).ThenInclude(b => b.Brand)
                .Include(c => c.ApplicationUser)
                .Where(c =>
                c.IsActive == true &&
                (c.CargoAnnounceNo == validFilter.CargoAnnounceNo || validFilter.CargoAnnounceNo==null) &&
                (c.IsComplete == validFilter.IsCompletlyLading || !validFilter.IsCompletlyLading) &&
                (c.Order.CustomerId == validFilter.CustomerId || validFilter.CustomerId == null) &&
                (c.Order.OrderCode == validFilter.OrderCode || validFilter.OrderCode == null) &&
                (c.OrderId == validFilter.OrderId || validFilter.OrderId == null))
                .OrderBy(c => c.DeliveryDate).AsQueryable();

        }

        public async Task<CargoAnnounce?> GetCargoAnnounceInfo(Guid id)
        {
            return await _cargoAnnouncements
                .Include(t => t.Attachments)
                .Include(c => c.LadingPermits.Where(l => l.IsActive))
                .Include(ca => ca.Order).ThenInclude(o => o.Details).ThenInclude(od => od.ProductBrand).ThenInclude(p => p.Product)
                .Include(ca => ca.Order).ThenInclude(o => o.Details).ThenInclude(od => od.CargoAnnounces)
                .Include(ca => ca.Order).ThenInclude(o => o.Details).ThenInclude(od => od.Warehouse)
                .Include(ca => ca.Order).ThenInclude(o => o.Customer).ThenInclude(c => c.CustomerOfficialCompanies)
                .Include(ca => ca.Order).ThenInclude(o => o.OrderStatus)
                .Include(ca => ca.Order).ThenInclude(o => o.InvoiceType)
                .Include(ca => ca.Order).ThenInclude(o => o.FarePaymentType)
                .Include(ca => ca.Order).ThenInclude(o => o.OrderServices)
                .Include(ca => ca.Order).ThenInclude(o => o.OrderSendType)
                .Include(ca => ca.CargoAnnounceDetails).ThenInclude(c => c.LadingExitPermitDetail).ThenInclude(x=>x.LadingExitPermit)
                .Include(ca => ca.CargoAnnounceDetails).ThenInclude(c => c.OrderDetail).ThenInclude(c => c.Product).ThenInclude(p => p.ProductSubUnit)
                .Include(ca => ca.CargoAnnounceDetails).ThenInclude(c => c.OrderDetail).ThenInclude(c => c.Product).ThenInclude(p => p.ProductMainUnit)
                .Include(ca => ca.CargoAnnounceDetails).ThenInclude(c => c.OrderDetail).ThenInclude(c => c.ProductBrand).ThenInclude(b => b.Brand)
                .Include(c => c.ApplicationUser)
                .Where(c => c.IsActive == true &&
                c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> ValidationForCargoAnnc(Guid orderId)
        {
            return await _order.AnyAsync(a => a.Id == orderId && a.OrderStatusId == (int)OrderStatusEnum.Sending);// && a.IsCompletlySended);
        }

        public async Task<bool> CreateLadingPermit(Guid cargoAnnounceId)
        {
            var cargoAnnc = await _cargoAnnouncements.FirstOrDefaultAsync(c => c.Id == cargoAnnounceId);
            if (cargoAnnc == null)
                throw new ApiException("اعلام بار یافت نشد !");
            if (cargoAnnc.HasLadingPermit)
                throw new ApiException("مجوز بارگیری قبلا برای این اعلام بار ایجاد شده است !");

            cargoAnnc.HasLadingPermit = true;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<CargoAnnounce> UpdateCargoAnncAsync(CargoAnnounce cargoAnnc)
        {
            _dbContext.CargoAnnounceDetails.RemoveRange(_dbContext.CargoAnnounceDetails
                .Where(s => s.CargoAnnounceId == cargoAnnc.Id &&
                !cargoAnnc.CargoAnnounceDetails.Select(d => d.Id).Contains(s.Id)));//.Remove(os);

            cargoAnnc = _cargoAnnouncements.Update(cargoAnnc).Entity;

            await _dbContext.SaveChangesAsync();
            return cargoAnnc;
        }

        public async Task<List<CargoAnnounceDetail>> GetCargoAnnouncesByOrderDetailId(int? orderDetailId)
        {
            return await _cargoAnnounceDetail
                .Include(c => c.OrderDetail).ThenInclude(c => c.ProductBrand).ThenInclude(t => t.Brand)
                .Include(c => c.OrderDetail).ThenInclude(c => c.ProductBrand).ThenInclude(t => t.Product)
                .Include(c => c.OrderDetail).ThenInclude(c => c.Product)
                .Where(cd => cd.OrderDetailId == orderDetailId)
                .ToListAsync();
        }
    }
}