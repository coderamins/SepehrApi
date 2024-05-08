using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments
{
    public class GetAllRentsToPaymentQuery : IRequest<PagedResponse<IEnumerable<RentsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? ReferenceCode { get; set; }
        public string DriverName { get; set; } = string.Empty;
        public string DriverMobile { get; set; } = string.Empty;
        public OrderClassType? OrderType { get; set; }
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
    }
    public class GetAllRentsToPaymentQueryHandler :
         IRequestHandler<GetAllRentsToPaymentQuery, PagedResponse<IEnumerable<RentsViewModel>>>
    {
        private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
        private readonly IMapper _mapper;
        public GetAllRentsToPaymentQueryHandler(IRentPaymentRepositoryAsync rentPaymentRepository, IMapper mapper)
        {
            _rentPaymentRepository = rentPaymentRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<RentsViewModel>>> Handle(GetAllRentsToPaymentQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllRentsToPaymentParameter>(request);
                var rents = await _rentPaymentRepository.GetAllRentsAsync(validFilter);

                var t1 = _mapper.Map<List<RentsViewModel>>(rents.Item1);
                var t2 = _mapper.Map<List<RentsViewModel>>(rents.Item2);

                var result = t1.Union(t2);

                var rentsToPaymentViewModel =
                    result.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList();

                return new PagedResponse<IEnumerable<RentsViewModel>>(
                    rentsToPaymentViewModel.OrderBy(p => p.ReferenceDate),
                    validFilter.PageNumber,
                    validFilter.PageSize, result.Count());

                //return new Response<IEnumerable<RentsViewModel>>(t1.Union(t2));

            }
            catch (Exception e) 
            {
                throw;
            }
        }

    }
}