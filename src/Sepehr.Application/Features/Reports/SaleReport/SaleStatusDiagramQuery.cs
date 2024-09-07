using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Reports.SaleReport
{
    public class SaleStatusDiagramQuery : IRequest<Response<IEnumerable<SaleStatusDiagramViewModel>>>
    {
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public int? ProductTypeId { get; set; }
    }
    public class SaleStatusDiagramQueryHandler :
         IRequestHandler<SaleStatusDiagramQuery, Response<IEnumerable<SaleStatusDiagramViewModel>>>
    {
        private readonly ISaleReportRepository _saleReport;
        private readonly IMapper _mapper;
        public SaleStatusDiagramQueryHandler(ISaleReportRepository saleReport, IMapper mapper)
        {
            _saleReport = saleReport;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<SaleStatusDiagramViewModel>>> Handle(SaleStatusDiagramQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<SaleReportByProductTypeParameter>(request);
                var sale_report = await _saleReport.GetSaleStatusDiagram(validFilter);

                var saleRepByProductTypeViewModel = _mapper.Map<IEnumerable<SaleStatusDiagramViewModel>>(
                    sale_report);

                return new Response<IEnumerable<SaleStatusDiagramViewModel>>(saleRepByProductTypeViewModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
