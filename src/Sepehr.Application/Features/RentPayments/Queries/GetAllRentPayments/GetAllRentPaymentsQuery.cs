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
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments
{
    public class GetAllRentPaymentsQuery : IRequest<PagedResponse<IEnumerable<RentPaymentViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? RentPaymentCode { get; set; }
        public int? RentPaymentId { get; internal set; }
        public int? ReferenceCode { get; set; }
        public string FromDate { get; set; } = string.Empty;
        public string ToDate { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
    }
    public class GetAllRentPaymentsQueryHandler :
         IRequestHandler<GetAllRentPaymentsQuery, PagedResponse<IEnumerable<RentPaymentViewModel>>>
    {
        private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
        private readonly IMapper _mapper;
        public GetAllRentPaymentsQueryHandler(IRentPaymentRepositoryAsync rentPaymentRepository, IMapper mapper)
        {
            _rentPaymentRepository = rentPaymentRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<RentPaymentViewModel>>> Handle(
            GetAllRentPaymentsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllRentPaymentsParameter>(request);
                var rentPayments = await _rentPaymentRepository.GetAllRentPaymentsAsync(validFilter);

                var rentPaymentViewModel = _mapper.Map<IEnumerable<RentPaymentViewModel>>(
                    rentPayments.Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                    .Take(validFilter.PageSize).ToList());

                return new PagedResponse<IEnumerable<RentPaymentViewModel>>(
                    rentPaymentViewModel.OrderByDescending(p=>p.Id),
                    validFilter.PageNumber,
                    validFilter.PageSize, rentPayments.Count());
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}