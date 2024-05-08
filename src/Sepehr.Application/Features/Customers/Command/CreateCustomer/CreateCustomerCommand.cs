using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Customers.Command.CreateCustomer
{
    public partial class CreateCustomerCommand : IRequest<Response<Customer>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string OfficialName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public required string NationalId { get; set; }
        public required string Mobile { get; set; }
        public required string Address1 { get; set; }
        public CustomerType CustomerType { get; set; }
        public required int CustomerValidityId { get; set; }
        public string? Tel1 { get; set; }
        public string? Tel2 { get; set; }
        public string? Address2 { get; set; }
        public string? Representative { get; set; }
        public int SettlementDay { get; set; }
        public SettlementType SettlementType { get; set; } = SettlementType.BeforeExit;
        /// <summary>
        /// آیا تامین کننده می باشد؟
        /// </summary>
        public bool IsSupplier { get; set; }
    }
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Response<Customer>>
    {
        private readonly ICustomerRepositoryAsync _customerRepository;
        private readonly IMapper _mapper;
        public CreateCustomerCommandHandler(ICustomerRepositoryAsync customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }        

        public async Task<Response<Customer>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _mapper.Map<Customer>(request);
                var checkDuplicate = await _customerRepository.GetCustomerInfo(request.NationalId);
                if (checkDuplicate != null) { throw new ApiException("مشتری با این کد ملی قبلا ثبت نام کرده است !"); }

                await _customerRepository.AddAsync(customer);
                return new Response<Customer>(customer, "اطلاعات مشتری جدید با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}