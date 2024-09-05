using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersParameter : RequestParameter
    {
        public int? CustomerCode { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string Keyword { get; set; } = string.Empty;

        public int? CustomerLabelId { get; set; }
        public CustomerReportType ReportType { get; set; }

        public GetAllCustomersParameter()
        {
            //Keyword = Keyword.Replace("ﮎ", "ک")
            //      .Replace("ﮏ", "ک")
            //      .Replace("ﮐ", "ک")
            //      .Replace("ﮑ", "ک")
            //      .Replace("ك", "ک")
            //      .Replace("ي", "ی")
            //      .Replace("ئ", "ی")
            //      .Replace("ى", "ی")
            //      .Replace(" ", " ")
            //      .Replace("‌", " ")
            //      .Replace("ٔ", "")
            //      .Replace("ھ", "ه")
            //      .Replace("دِ", "د")
            //      .Replace("بِ", "ب")
            //      .Replace("زِ", "ز")
            //      .Replace("شِ", "ش")
            //      .Replace("سِ", "س");
        }
    }
}