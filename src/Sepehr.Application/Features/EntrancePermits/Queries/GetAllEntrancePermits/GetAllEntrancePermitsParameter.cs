using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits
{
    public class GetAllEntrancePermitsParameter : RequestParameter
    {
        public IEnumerable<int>? InvoiceTypeId { get; set; }
        public int? EntrancePermitStatusId { get; set; }
        public bool? IsNotTransferedToWarehouse { get; set; }
        public int? EntrancePermitNo { get; set; }
        public long? OrderCode { get; set; }
    }
}