using Sepehr.Application.DTOs.PurchaseOrder;
using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Order
{
    public class CreatePurchaseOrderDto
    {
        public Guid? Id { get; set; }
        public required Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public int PurchaseOrderSendTypeId { get; set; }
        public int InvoiceTypeId { get; set; }
        [JsonIgnore]
        public bool IsIntermediary { get; set; } = true;

        public required virtual List<CreatePurchaseOrderDetailRequest> Details { get; set; }
    }
}
