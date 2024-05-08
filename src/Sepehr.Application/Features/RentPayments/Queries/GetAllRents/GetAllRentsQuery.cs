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
    public class GetAllRentsQuery : IRequest<PagedResponse<IEnumerable<RentsViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? LadingExitPermitId { get; set; }
        public Guid? PurchaseOrderTransferRemittanceUnloadingPermitId { get; set; }
    }
    public class GetAllRentsQueryHandler :
         IRequestHandler<GetAllRentsQuery, Response<IEnumerable<RentsViewModel>>>
    {
        private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
        private readonly IMapper _mapper;
        public GetAllRentsQueryHandler(IRentPaymentRepositoryAsync rentPaymentRepository, IMapper mapper)
        {
            _rentPaymentRepository = rentPaymentRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<RentsViewModel>>> Handle(
            GetAllRentsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllRentsToPaymentParameter>(request);
                var rents = await _rentPaymentRepository.GetAllRentsAsync(validFilter);

                var t1 = _mapper.Map<List<RentsViewModel>>(rents.Item1);
                var t2 = _mapper.Map<List<RentsViewModel>>(rents.Item2);

                return new Response<IEnumerable<RentsViewModel>>(t1.Union(t2));

            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}