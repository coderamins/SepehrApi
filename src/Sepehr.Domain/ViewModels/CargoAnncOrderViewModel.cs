using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CargoAnncOrderViewModel: BaseViewModel<Guid>
    {
        public string? CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public long OrderCode { get; set; }
        public bool ConfirmedStatus { get; set; }
        public string? Description { get; set; }
        public int OrderSendTypeId { get; set; }
        public string OrderSendTypeDesc { get; set; } = string.Empty;
        public string DeliverDate { get; set; }
        public string OrderTypeDesc { get; set; } = string.Empty;
        public int FarePaymentTypeId { get; set; }
        public string PaymentTypeDesc { get; set; } = string.Empty;
        public string? OfficialName { get; set; }
        public int InvoiceTypeId { get; set; }
        public string InvoiceTypeDesc { get; set; } = string.Empty;
        public string? FreightName { get; set; }
        public string SettlementDate { get; set; } = string.Empty;
        public int OrderStatusId { get; set; }
        public string OrderStatusDesc { get; set; } = string.Empty;
        public string CustomerFirstName { get; set; } = string.Empty;
        public string CustomerLastName { get; set; } = string.Empty;
        public string RegisterDate { get; set; } = string.Empty;
        public bool IsTemporary { get; set; }
        public string? DischargePlaceAddress { get; set; }
        public string? FreightDriverName { get; set; }
        public string? CarPlaque { get; set; }
    }
}
