using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Reports.SaleReport
{
    public class SaleReportByProductTypeQuery : IRequest<Response<IEnumerable<SaleRepByProductTypeViewModel>>>
    {
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public int? ProductTypeId { get; set; }
    }
    public class SaleReportByProductTypeQueryHandler :
         IRequestHandler<SaleReportByProductTypeQuery, Response<IEnumerable<SaleRepByProductTypeViewModel>>>
    {
        private readonly ISaleReportRepository _saleReport;
        private readonly IMapper _mapper;
        public SaleReportByProductTypeQueryHandler(ISaleReportRepository saleReport, IMapper mapper)
        {
            _saleReport = saleReport;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SaleRepByProductTypeViewModel>>> Handle(SaleReportByProductTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<SaleReportByProductTypeParameter>(request);
                var sale_report = await _saleReport.GetSaleReportByProductType(validFilter);

                var saleRepByProductTypeViewModel = _mapper.Map<IEnumerable<SaleRepByProductTypeViewModel>>(
                    sale_report);

                return new Response<IEnumerable<SaleRepByProductTypeViewModel>>(saleRepByProductTypeViewModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
