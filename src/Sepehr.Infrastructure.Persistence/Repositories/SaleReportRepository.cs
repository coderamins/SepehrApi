using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.Reports.SaleReport;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;
using Sepehr.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class SaleReportRepository : ISaleReportRepository
    {
        private readonly IOrderRepositoryAsync _orders;
        private readonly DbSet<OrderDetail> _orderDetail;
        private readonly IPurchaseOrderRepositoryAsync _purchaseOrder;
        private readonly IMapper _mapper;
        public SaleReportRepository(
            ApplicationDbContext dbcontext,
            IOrderRepositoryAsync order,
            IPurchaseOrderRepositoryAsync purchaseOrder,
            IMapper mapper)
        {
            _orders = order;
            _purchaseOrder = purchaseOrder;
            _orderDetail = dbcontext.Set<OrderDetail>(); ;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SaleRepByProductTypeViewModel>> GetSaleReportByProductType(SaleReportByProductTypeParameter filter)
        {
            var _saleRep = await
                _orderDetail.Include(x => x.ProductBrand).ThenInclude(x => x.Product).ThenInclude(x => x.ProductType)
                .Where(o =>
                    (o.Product.ProductTypeId == filter.ProductTypeId || filter.ProductTypeId == null) &&
                    (o.Order.Created.Date >= filter.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.FromDate) &&
                    (o.Order.Created.Date <= filter.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.ToDate))
                 ))
                .GroupBy(x => new
                {
                    x.ProductBrand.Product.ProductType.Desc
                    /*,SaleAmount=x.*/
                })
                .ToListAsync();

            List<SaleRepByProductTypeViewModel> _saleReport = new List<SaleRepByProductTypeViewModel>();
            foreach (var item in _saleRep)
            {
                _saleReport.Add(new SaleRepByProductTypeViewModel
                {
                    ProductTypeDesc = item.Key.Desc,
                    SaleAmount = item.Sum(x => x.ProximateAmount),
                });
            }

            return _saleReport;
        }

        public async Task<IEnumerable<SaleStatusDiagramViewModel>> GetSaleStatusDiagram(SaleReportByProductTypeParameter filter)
        {
            var dates = Enumerable.Range(0, (filter.ToDate.ToDateTime("00:00") - filter.FromDate.ToDateTime("00:00")).Days + 1)
            .Select(i => filter.FromDate.ToDateTime("00:00").AddDays(i));

            var salesByDay = _orderDetail
                           .Include(x => x.ProductBrand)
                           .ThenInclude(x => x.Product)
                           .ThenInclude(x => x.ProductType)
                           .Where(o =>
                                 (o.Product.ProductTypeId==filter.ProductTypeId || filter.ProductTypeId==null) &&
                                 (o.Order.Created.Date >= filter.FromDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.FromDate) &&
                                 (o.Order.Created.Date <= filter.ToDate.ToDateTime("00:00") || string.IsNullOrEmpty(filter.ToDate))
                            ))
                           .GroupBy(x => new { x.ProductBrand.Product.ProductType.Desc, SaleDate = x.Order.Created.Date })
                           .Select(g => new
                           {
                               SaleDate = g.Key.SaleDate,
                               OrderAmount = g.Sum(o => o.ProximateAmount),
                               ProductTypeDesc = g.Key.Desc,
                               Price = g.Sum(o => o.Price * o.ProximateAmount)
                           });

            // ترکیب نتایج با دنباله تاریخ ها و پر کردن روزهای بدون سفارش
            return dates
                .GroupJoin(salesByDay,
                    date => date,
                    sale => sale.SaleDate,
                    (date, sales) => new SaleStatusDiagramViewModel
                    {
                        SaleDate = date.ToShamsiDate(),
                        OrderAmount = sales.Sum(o => o.OrderAmount),
                        ProductTypeDesc = sales.FirstOrDefault()?.ProductTypeDesc,
                        Price = sales.Sum(o => o.Price * o.OrderAmount)
                    });
        }
    }
}
