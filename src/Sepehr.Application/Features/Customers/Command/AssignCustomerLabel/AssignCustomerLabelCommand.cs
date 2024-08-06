using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Customer;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Customers.Command.AssignCustomerLabel
{
    public partial class AssignCustomerLabelCommand : IRequest<Response<bool>>
    {
        public Guid CustomerId { get; set; }

        public required IEnumerable<int> AssignedLabels { get; set; }
    }

    public class AssignCustomerLabelCommandHandler : IRequestHandler<AssignCustomerLabelCommand, Response<bool>>
    {
        private readonly ICustomerRepositoryAsync _customerRepository;
        private readonly IMapper _mapper;
        public AssignCustomerLabelCommandHandler(ICustomerRepositoryAsync customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(AssignCustomerLabelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var checkDuplicate = await _customerRepository.GetCustomerInfo(request.CustomerId);
                if (checkDuplicate == null) { throw new ApiException("مشتری یافت نشد !"); }

                var customerAssignedLabelDtos = _mapper.Map<IEnumerable<CustomerAssignedLabelDto>>(request);
                var customerAssignedLabels = _mapper.Map<List<CustomerAssignedLabel>>(customerAssignedLabelDtos);

                await _customerRepository.AssignCustomerLabels(customerAssignedLabels);
                return new Response<bool>(true, "برچسب های جدید با موفقیت ایجاد شدند .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}