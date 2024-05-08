using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace Sepehr.Application.Features.CustomerOfficialCompanys.Command.UpdateCustomerOfficialCompany
{
    public class UpdateCustomerOfficialCompanyCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public required string CompanyName { get; set; }
        public required Guid CustomerId { get; set; }
        public string? EconomicId { get; set; }
        public string? PostalCode { get; set; }
        [StringLength(11)]
        public string? NationalId { get; set; }
        public CustomerType? CustomerType { get; set; }
        [StringLength(11)]
        public string? Tel1 { get; set; }
        [StringLength(11)]
        public string? Tel2 { get; set; }
        [StringLength(500)]
        public string? Address { get; set; }
        public bool IsActive { get; set; }

        public class UpdateCustomerOfficialCompanyCommandHandler : IRequestHandler<UpdateCustomerOfficialCompanyCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly ICustomerOfficialCompanyRepositoryAsync _customerOfficialCompanyRepository;
            public UpdateCustomerOfficialCompanyCommandHandler(ICustomerOfficialCompanyRepositoryAsync customerOfficialCompanyRepository, IMapper mapper)
            {
                _customerOfficialCompanyRepository = customerOfficialCompanyRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateCustomerOfficialCompanyCommand command, CancellationToken cancellationToken)
            {
                var customerOfficialCompany = await _customerOfficialCompanyRepository.GetByIdAsync(command.Id);
                customerOfficialCompany = _mapper.Map(command, customerOfficialCompany);

                if (customerOfficialCompany == null)
                {
                    throw new ApiException($"شرکت رسمی مشتری یافت نشد !");
                }
                else
                {
                    await _customerOfficialCompanyRepository.UpdateAsync(customerOfficialCompany);
                    return new Response<string>(customerOfficialCompany.Id.ToString(), "");
                }
            }
        }
    }
}