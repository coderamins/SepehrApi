using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CustomerBalanceViewModel
    {
        public Guid CustoemrId { get; set; }
        /// <summary>
        /// تاریخ
        /// </summary>
        public string BalanceDate { get; set; }=string.Empty;
        /// <summary>
        /// تاریخ وزن
        /// </summary>
        public string WeightingDate { get; set; } = string.Empty;
        /// <summary>
        /// نوع سند
        /// </summary>
        public EBalanceType BalanceType { get; set; }
        public string BalanceDesc { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        /// <summary>
        /// تشخیص (بد بس)
        /// </summary>
        public EBalanceResult BalanceResult { get; set; }
        /// <summary>
        /// مانده
        /// </summary>
        public decimal BalancedAmount { get; set; }
        /// <summary>
        /// مانده موعد شده تا این تاریخ
        /// </summary>
        public decimal MaturityAmountOfDate { get; set; }
        public string Description { get; set; } = string.Empty;

        public CustomerBeginingBalanceViewModel CustomerBeginingBalanceViewModel { get; set; }


    }
}
