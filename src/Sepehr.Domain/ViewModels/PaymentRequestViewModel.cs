using Sepehr.Domain.Entities.BaseViewModels;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class PaymentRequestViewModel:PaymentRequestBaseViewModel
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
    }
}
