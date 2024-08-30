using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CustomerBeginingBalanceViewModel:BaseViewModel<Guid>
    {
        public Guid CustomerId { get; set; }
        public string CustomerFullName { get; set; }=string.Empty;
        /// <summary>
        /// بدهکاری اول دوره
        /// </summary>
        public decimal DebitBalance { get; set; }
        /// <summary>
        /// بستانکاری اول دوره
        /// </summary>
        public decimal CreditBalance { get; set; }
    }
}
