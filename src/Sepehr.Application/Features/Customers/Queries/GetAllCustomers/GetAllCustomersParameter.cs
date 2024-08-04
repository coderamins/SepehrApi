using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersParameter : RequestParameter
    {
        public int? CustomerCode { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }=string.Empty;
        public string NationalCode { get; set; } = string.Empty;
    }
}