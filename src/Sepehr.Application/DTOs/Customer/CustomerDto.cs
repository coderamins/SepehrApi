using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.DTOs.Customer
{
    public class CustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OfficialName { get; set; } = string.Empty;
        public string FatherName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string NationalId { get; set; } = "1111111111";
        public string Address1 { get; set; }
        public CustomerType CustomerType { get; set; } = CustomerType.Real;
        public int CustomerValidityId { get; set; } = (int)ECustomerValidity.Usual;
        public string? Address2 { get; set; }
        public string? Representative { get; set; }
        public int SettlementDay { get; set; }
        public string CustomerCharacteristics { get; set; } = string.Empty;
        public SettlementType SettlementType { get; set; } = SettlementType.AfterExit;

        public bool IsSupplier { get; set; }
        public IEnumerable<CreatePhonebookRequest>? Phonebook { get; set; }
        //public IEnumerable<CustomerAssignedLabelDto> CustomerAssignedLabels { get; set; }

    }
}
