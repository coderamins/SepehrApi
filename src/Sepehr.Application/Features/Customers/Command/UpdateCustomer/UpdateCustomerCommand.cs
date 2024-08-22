using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Customers.Command.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string OfficialName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public required string NationalId { get; set; }
        //public string Mobile { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public CustomerType CustomerType { get; set; }
        public required int CustomerValidityId { get; set; }
        //public string? Tel1 { get; set; }
        //public string? Tel2 { get; set; }
        public string? Address2 { get; set; }
        public string? Representative { get; set; }
        public SettlementType SettlementType { get; set; }
        public int SettlementDay { get; set; }
        public string CustomerCharacteristics { get; set; } = string.Empty;
        public bool IsSupplier { get; set; }

        public IEnumerable<CreatePhonebookRequest>? Phonebook { get; set; }

        public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly ICustomerRepositoryAsync _customerRepository;
            public UpdateCustomerCommandHandler(ICustomerRepositoryAsync customerRepository, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
            {
                var customer = await _customerRepository
                    .LoadSingleWithRelatedAsync<Customer>(,);

                _customerRepository.LoadAllWithRelatedAsQueryableAsync<ProductBrand>(request.PageNumber, request.PageSize,
                p => p.Product,
                    p => p.Product.ProductMainUnit,
                    p => p.Product.ProductSubUnit,
                    p => p.Brand);
                if (customer == null)
                {
                    throw new ApiException($"مشتری یافت نشد !");
                }
                else
                {
                    if (customer.Phonebook != null)
                        customer.Phonebook.Clear();

                    customer.Orders.Clear();
                    var updated_customer = _mapper.Map(command, customer);

                    await _customerRepository.UpdateCustomer(updated_customer);
                    return new Response<string>(customer.Id.ToString(), "");
                }
            }
        }
    }
}