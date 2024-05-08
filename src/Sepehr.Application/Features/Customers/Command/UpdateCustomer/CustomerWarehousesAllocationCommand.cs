using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Customers.Command.UpdateCustomer
{
    public class CustomerWarehousesAllocationCommand : IRequest<Response<bool>>
    {
        public Guid CustomerId { get; set; }
        public required List<int> Warehouses { get; set; }

        public class AsignCustomerWarehousesCommandHandler : IRequestHandler<CustomerWarehousesAllocationCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly ICustomerRepositoryAsync _customerRepository;
            public AsignCustomerWarehousesCommandHandler(ICustomerRepositoryAsync customerRepository, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }
            public async Task<Response<bool>> Handle(CustomerWarehousesAllocationCommand command, CancellationToken cancellationToken)
            {
                var result = await _customerRepository.AllocateCustomerWarehouses(command.CustomerId,command.Warehouses);                

                return new Response<bool>(result, "تخصیص انبار به مشتری با موفقیت انجام شد .");
                
            }
        }
    }
}