using Sepehr.Application.Features.Reports.SaleReport;
using Sepehr.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ISaleReportRepository
    {
        Task<IEnumerable<SaleRepByProductTypeViewModel>> GetSaleReportByProductType(SaleReportByProductTypeParameter filter);
        Task<IEnumerable<SaleStatusDiagramViewModel>> GetSaleStatusDiagram(SaleReportByProductTypeParameter validFilter);
    }
}
