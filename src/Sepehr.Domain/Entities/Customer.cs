using Sepehr.Domain.Common;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class Customer : AuditableBaseEntity<Guid>
    {
        public Customer()
        {
            ReceivePaymentSourceFrom = new HashSet<ReceivePay>();
            ReceivePaymentSourceTo = new HashSet<ReceivePay>();
        }

        public long CustomerCode { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FatherName { get; set; } = string.Empty;
        public string? OfficialName { get; set; }
        public required string NationalId { get; set; }
        public string NickName { get; set; }
        /// <summary>
        /// شناسه ملی
        /// </summary>
        public string NationalCode { get; set; } = string.Empty;
        //public required string Mobile { get; set; }
        /// <summary>
        /// آیا تامین کننده می باشد؟
        /// </summary>
        public bool IsSupplier { get; set; }
        public required string Address1 { get; set; }
        public string? Email { get; set; }
        /// <summary>
        /// نوع مشتری
        /// </summary>
        public CustomerType CustomerType { get; set; }
        public required int CustomerValidityId { get; set; }

        public string? Tel1 { get; set; }
        public string? Tel2 { get; set; }
        public string? Address2 { get; set; }
        /// <summary>
        /// معرف
        /// </summary>
        public string? Representative { get; set; }

        public SettlementType SettlementType { get; set; }
        public int SettlementDay { get; set; }
        public string CustomerCharacteristics { get; set; }


        public required virtual CustomerValidity CustomerValidity { get; set; }
        public virtual ICollection<Phonebook>? Phonebook { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        [NotMapped]
        public virtual ICollection<ReceivePay> ReceivePaymentSourceFrom { get; set; } =new List<ReceivePay>();
        [NotMapped]
        public virtual ICollection<ReceivePay> ReceivePaymentSourceTo { get; set; }=new List<ReceivePay>();
        public virtual ICollection<CustomerOfficialCompany> CustomerOfficialCompanies { get; set; } = new List<CustomerOfficialCompany>();
        public virtual ICollection<CustomerWarehouse> CustomerWarehouses { get; set; }
        public virtual ICollection<CustomerAssignedLabel> CustomerLabels { get; set; }

    }
}
