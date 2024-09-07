using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class GetCustomerBillingViewModel
    {
        public DateTime Created { get; set; }
        public string Created_Shamsi { get; set; }=string.Empty;
        public DateTime? WeightingDate { get; set; }
        public string WeightingDate_Shamsi { get; set; } = string.Empty;
        public string DocType { get; set; } = string.Empty;
        public string DocDesc { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal BalanceAmoount { get; set; }
        public decimal DueAmount { get; set; }
        public string Recognizing { get; set; }=string.Empty;
    }
}
