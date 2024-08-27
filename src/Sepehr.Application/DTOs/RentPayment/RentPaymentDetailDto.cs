using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.RentPayment
{
    public class RentPaymentDetailDto
    {
        public Guid? UnloadingPermitId { get; set; }
        public Guid? LadingExitPermitId { get; set; }
    }
}
