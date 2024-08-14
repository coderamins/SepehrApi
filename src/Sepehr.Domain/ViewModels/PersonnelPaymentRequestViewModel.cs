using Sepehr.Domain.Entities.BaseViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class PersonnelPaymentRequestViewModel: PaymentRequestBaseViewModel
    {
        public Guid PersonnelId { get; set; }
        public string PersonnelName { get; set; } = string.Empty;
    }
}
