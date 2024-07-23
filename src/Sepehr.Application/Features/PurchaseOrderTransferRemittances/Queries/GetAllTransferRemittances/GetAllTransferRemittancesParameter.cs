using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.TransferRemittances.Queries.GetAllTransferRemittances
{
    public class GetAllTransferRemittancesParameter : RequestParameter
    {
        public int? Id { get; set; }
        public string? RegisterDate { get; set; }
        public int? OriginWarehouseId { get; set; }
        public int? DestinationWarehouseId { get; set; }
        public int? TransferRemittStatusId { get; set; }
        /// <summary>
        /// ورود حواله انجام شده یا نه
        /// </summary>
        public bool? IsEntranced { get; set; }
        public int? TransferEntransePermitNo { get; set; }
        public Guid? MarketerId { get; set; }
    }
}