using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CustomerBillingViewModel
    {
        public Guid CustomerId { get; set; }
        /// <summary>
        /// کل مانده حساب
        /// </summary>
        public decimal RemainingAmount { get; set; }
        /// <summary>
        /// کل مانده موعد شده
        /// </summary>
        public decimal TotalDueRemainingAmount { get; set; }

        /// <summary>
        /// تشخیص نهایی
        /// </summary>
        public string Recognize { get; set; }
        public List<CustomerBillingDetailViewModel> Details { get; set; }
    }

    public class CustomerBillingDetailViewModel
    {
        public DateTime Created { get; set; }
        public string Created_Shamsi { get; set; } = string.Empty;
        public DateTime? WeightingDate { get; set; }
        public string WeightingDate_Shamsi { get; set; } = string.Empty;
        public string DocType { get; set; } = string.Empty;
        public string DocDesc { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal DueRemainingAmount { get; set; }
        public string Recognizing { get; set; } = string.Empty;
    }
}
