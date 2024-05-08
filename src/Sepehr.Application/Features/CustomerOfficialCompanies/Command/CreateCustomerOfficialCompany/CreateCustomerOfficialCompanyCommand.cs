using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.CustomerOfficialCompanys.Command.CreateCustomerOfficialCompany
{
    public partial class CreateCustomerOfficialCompanyCommand : IRequest<Response<CustomerOfficialCompany>>
    {
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
    }
    public class CreateCustomerOfficialCompanyCommandHandler : IRequestHandler<CreateCustomerOfficialCompanyCommand, Response<CustomerOfficialCompany>>
    {
        private readonly ICustomerOfficialCompanyRepositoryAsync _customerOfficialCompanyRepository;
        private readonly IMapper _mapper;
        public CreateCustomerOfficialCompanyCommandHandler(ICustomerOfficialCompanyRepositoryAsync customerOfficialCompanyRepository, IMapper mapper)
        {
            _customerOfficialCompanyRepository = customerOfficialCompanyRepository;
            _mapper = mapper;
        }

        public async Task<Response<CustomerOfficialCompany>> Handle(CreateCustomerOfficialCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customerOfficialCompany = _mapper.Map<CustomerOfficialCompany>(request);
                await _customerOfficialCompanyRepository.AddAsync(customerOfficialCompany);
                return new Response<CustomerOfficialCompany>(customerOfficialCompany, "شرکت رسمی مشتری با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}