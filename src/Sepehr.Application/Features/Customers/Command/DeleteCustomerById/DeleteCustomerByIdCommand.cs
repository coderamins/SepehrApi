using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.Customers.Command.DeleteCustomerById
{
    public class DeleteCustomerByIdCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public class
        DeleteCustomerByIdCommandHandler
        : IRequestHandler<DeleteCustomerByIdCommand, Response<Guid>>
        {
            private readonly ICustomerRepositoryAsync _customerRepository;
            public DeleteCustomerByIdCommandHandler(
                ICustomerRepositoryAsync customerRepository
                
            )
            {
                _customerRepository = customerRepository;
                
            }

            public async Task<Response<Guid>>
            Handle(
                DeleteCustomerByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var customer = await _customerRepository.GetByIdAsync(command.Id);
                if (customer == null)
                    throw new ApiException($"مشتری یافت نشد !");

                await _customerRepository.DeleteAsync(customer);
                return new Response<Guid>(customer.Id);
            }
        }
    }
}
