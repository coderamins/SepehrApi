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
    public class GetFareAmountByRefCodeQuery : IRequest<Response<RentsViewModel>>
    {
        public int? ReferenceCode { get; set; }
    }
    public class GetFareAmountByRefCodeQueryHandler :
         IRequestHandler<GetFareAmountByRefCodeQuery, Response<RentsViewModel>>
    {
        private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
        private readonly IMapper _mapper;
        public GetFareAmountByRefCodeQueryHandler(IRentPaymentRepositoryAsync rentPaymentRepository, IMapper mapper)
        {
            _rentPaymentRepository = rentPaymentRepository;
            _mapper = mapper;
        }

        public async Task<Response<RentsViewModel>> Handle(GetFareAmountByRefCodeQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllRentsToPaymentParameter>(request);
                var rents = await _rentPaymentRepository.GetAllRentsAsync(validFilter);

                var t1 = _mapper.Map<List<RentsViewModel>>(rents.Item1);
                var t2 = _mapper.Map<List<RentsViewModel>>(rents.Item2);

                var result = t1.Union(t2);

                var rentsToPaymentViewModel = result.First();

                return new Response<RentsViewModel>(rentsToPaymentViewModel);

                //return new Response<IEnumerable<RentsViewModel>>(t1.Union(t2));

            }
            catch (Exception e) 
            {
                throw;
            }
        }

    }
}