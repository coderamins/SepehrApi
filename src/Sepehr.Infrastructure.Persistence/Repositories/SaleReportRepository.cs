using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Features.Reports.SaleReport;
using Sepehr.Application.Interfaces.Repositories;
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
                .GroupBy(x => new { x.ProductBrand.Product.ProductType.Desc/*,SaleAmount=x.*/ })
                .ToListAsync();

            List<SaleRepByProductTypeViewModel> _saleReport = new List<SaleRepByProductTypeViewModel>();
            foreach (var item in _saleRep)
            {
                _saleReport.Add(new SaleRepByProductTypeViewModel
                {
                    ProductTypeDesc= item.Key.Desc,
                    SaleAmount= item.Sum(x=>x.ProximateAmount),
                });
            }

            return _saleReport;
        }
    }
}
