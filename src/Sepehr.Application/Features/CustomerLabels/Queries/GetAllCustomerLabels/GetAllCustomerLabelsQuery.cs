using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.CustomerLabels.Queries.GetAllCustomerLabels;
using Sepehr.Application.Features.Orders.Queries.GetAllOrders;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.CustomerLabels.Queries.GetAllCustomerLabels
{
    public class GetAllCustomerLabelsQuery : IRequest<Response<IEnumerable<CustomerLabelViewModel>>>
    {
        public Guid CustomerId { get; set; }
    }
    public class GetAllCustomerLabelsQueryHandler :
         IRequestHandler<GetAllCustomerLabelsQuery, Response<IEnumerable<CustomerLabelViewModel>>>
    {
        private readonly ICustomerLabelRepositoryAsync _customerLabelRepository;
        private readonly IMapper _mapper;
        public GetAllCustomerLabelsQueryHandler(ICustomerLabelRepositoryAsync customerLabelRepository, IMapper mapper)
        {
            _customerLabelRepository = customerLabelRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CustomerLabelViewModel>>> Handle(
            GetAllCustomerLabelsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var filter = _mapper.Map<GetAllCustomerLabelsParameter>(request);

                var customerLabels = await _customerLabelRepository.GetAllCustomerLabelsAsync(filter); 
                
                var customerLabelViewModel = _mapper.Map<IEnumerable<CustomerLabelViewModel>>(customerLabels);
                return new Response<IEnumerable<CustomerLabelViewModel>>(customerLabelViewModel);
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}