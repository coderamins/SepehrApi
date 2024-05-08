using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.ReceivePays.Queries.GetAllReceivePays
{
    public class GetAllReceivePaysParameter : RequestParameter
    {
        public IsApprovalReceivePay IsApproved { get; set; }
        public string FromDate { get; set; }=string.Empty;
        public string ToDate { get; set; }=string.Empty;
        public int? StatusId { get; set; }
        public int? AccountingDocNo { get; set; }
        public long? ReceivePayCode { get; set; }
    }
}