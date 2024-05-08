using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Helpers;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;
using Stimulsoft.Report;

namespace Sepehr.Application.Features.Orders.Queries.GetExitPermissionReport
{
    public class GetExitPermissionReportQuery : IRequest<Response<byte[]>>
    {
        public Guid Id { get; set; }

        public class GetExitPermissionReportQueryHandler : IRequestHandler<GetExitPermissionReportQuery, Response<byte[]>>
        {
            private readonly IOrderRepositoryAsync _OrderRepository;
            private readonly IMapper _mapper;
            public GetExitPermissionReportQueryHandler(
                IOrderRepositoryAsync OrderRepository,
                IMapper mapper
            )
            {
                _OrderRepository = OrderRepository;
                _mapper = mapper;
            }

            public async Task<Response<byte[]>>
            Handle(
                GetExitPermissionReportQuery query,
                CancellationToken cancellationToken
            )
            {
                var order = await _OrderRepository.GetOrderById(query.Id);
                if (order == null)
                    throw new ApiException($"سفارش یافت نشد !");

                StiReport report = new StiReport();
                report.Load(@"StiReports\Report.mrt");
                report.Render();

                var orderVM = _mapper.Map<OrderViewModel>(order);
                var stream = new MemoryStream();

                report.RegBusinessObject("Orders", order);
                //report.Variables.Add("OrderAmount", order.TotalAmount);
                report["OrderAmount"] = order.TotalAmount;
                report.ExportDocument(StiExportFormat.Pdf, stream);

                report.Dispose();
                var result = StreamConvertor.UseStreamReader(stream);
                return new Response<byte[]>(result);


            }
        }
    }
}
