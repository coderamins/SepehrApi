using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    /// <summary>
    /// حساب اول دوره مشتری
    /// </summary>
    public class CustomerBeginingBalance:AuditableBaseEntity<Guid>
    {
        public Guid CustomerId { get; set; }
        /// <summary>
        /// بدهکاری اول دوره
        /// </summary>
        public decimal DebitBalance { get; set; }
        /// <summary>
        /// بستانکاری اول دوره
        /// </summary>
        public decimal CreditBalance { get; set; }

        public int FiscalYearId { get; set; }

        public virtual required FiscalYear FiscalYear { get; set; }
        public virtual required Customer Customer { get; set; }
    }
}
