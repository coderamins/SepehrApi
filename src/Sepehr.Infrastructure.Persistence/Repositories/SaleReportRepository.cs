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
            IOrderRepositoryAsync order,
            IPurchaseOrderRepositoryAsync purchaseOrder,
            DbSet<OrderDetail> orderDetail,
            IMapper mapper)
        {
            _orders = order;
            _purchaseOrder = purchaseOrder;
            _orderDetail = orderDetail;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SaleRepByProductTypeViewModel>> GetSaleReportByProductType(SaleReportByProductTypeParameter filter)
        {
            var _saleRep =await
                _orderDetail.Include(x => x.ProductBrand).ThenInclude(x => x.Product).ThenInclude(x=>x.ProductType)
                .GroupBy(x => new { x.ProductBrand.Product.ProductType.Desc })
                .ToListAsync();

            IEnumerable
            foreach(var item in _saleRep)
            {

            }
        }
    }
}
