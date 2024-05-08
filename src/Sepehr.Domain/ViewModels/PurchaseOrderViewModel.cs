using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class PurchaseOrderViewModel: OrderBaseVM
    {
        public int OriginWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public string OriginWarehouseDesc { get; set; } = string.Empty;
        public string DestinationWarehouseDesc { get; set; }=string.Empty;

        public CustomerViewModel Customer { get; set; } = new CustomerViewModel();
        public CustomerOfficialCompanyViewModel CustomerOfficialCompany { get; set; }=new CustomerOfficialCompanyViewModel();
        public List<OrderPaymentViewModel> OrderPayments{get;set;}=new List<OrderPaymentViewModel>();
        public List<OrderServiceViewModel> OrderServices{get;set;}=new List<OrderServiceViewModel>();
        public List<PurchaseOrderDetailViewModel> Details { get; set; } = new List<PurchaseOrderDetailViewModel>();
        public List<OrderPaymentViewModel> PurchaseOrderPayment { get; set; } = new List<OrderPaymentViewModel>();
        public List<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();
        public List<CargoAnncViewModel> CargoAnnounces { get; set; } = new List<CargoAnncViewModel>();

    }
}
