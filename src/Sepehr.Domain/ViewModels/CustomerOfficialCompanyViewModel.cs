using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CustomerOfficialCompanyViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public Guid CustomerId { get; set; }
        public string CreatedDate { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? EconomicId { get; set; }
        public string? PostalCode { get; set; }
        public string? NationalId { get; set; }
        public CustomerType? CustomerType { get; set; }
        public string? Tel1 { get; set; }
        public string? Tel2 { get; set; }
        public string? Address { get; set; }

        public CustomerViewModel Customer { get; set; }
    }
}
