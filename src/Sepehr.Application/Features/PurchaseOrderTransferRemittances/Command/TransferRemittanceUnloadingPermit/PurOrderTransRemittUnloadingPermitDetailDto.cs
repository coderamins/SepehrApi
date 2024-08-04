using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Features.TransferRemittances.Command.TransferRemittanceUnloadingPermit
{
    public class PurOrderTransRemittUnloadingPermitDetailDto
    {
        public int TransferRemittanceDetailId { get; set; }
        public decimal UnloadedAmount { get; set; }


    }
}
