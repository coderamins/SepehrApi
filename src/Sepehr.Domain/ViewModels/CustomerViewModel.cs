using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public long CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; } = string.Empty;
        public string? OfficialName { get; set; }
        public string NickName { get; set; }
        public string NationalId { get; set; }
        /// <summary>
        /// شناسه ملی
        /// </summary>
        public string NationalCode { get; set; } = string.Empty;

        public string Mobile { get; set; }
        public string Address1 { get; set; }
        public CustomerType CustomerType { get; set; }
        public int CustomerValidityId { get; set; }
        public string CustomerValidityDesc { get; set; }
        public string CustomerValidityColorCode { get; set; }
        public string? Tel1 { get; set; }
        public string? Tel2 { get; set; }
        public string? Address2 { get; set; }
        public string? Representative { get; set; }
        public bool IsSupplier { get; set; }
        public string CustomerCharacteristics { get; set; }= string.Empty;

        public SettlementType SettlementType { get; set; }
        public int SettlementDay { get; set; }
        public string SettlementTypeDesc { get; set; } = string.Empty;

        public decimal CustomerCurrentDept { get; set; }
        public decimal CustomerDept { get; set; }

        public List<PhonebookViewModel> Phonebook { get; set; }
        public List<CustomerOfficialCompanyViewModel>? CustomerOfficialCompanies { get; set; }
        public List<WarehouseViewModel>? Warehouses { get; set; }
        public List<CustomerAssignedLabelViewModel>? CustomerLabels { get; set; }
    }
}
