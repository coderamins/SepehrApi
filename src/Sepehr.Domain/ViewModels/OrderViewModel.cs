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
    public class OrderViewModel: OrderBaseVM
    {
        public OrderType OrderTypeId { get; set; }
        public string DeliverDate { get; set; } = string.Empty;
        public string OrderTypeDesc { get; set; } = string.Empty;

        /// <summary>
        /// مجموع مقدار بارگیری شده
        /// </summary>
        public decimal TotalLoadedAmount { get; set; }
        public decimal RemainingLadingAmount { get; set; }
        public string CreatorName { get; set; } = string.Empty;

        public CustomerViewModel Customer { get; set; } = new CustomerViewModel();
        public CustomerOfficialCompanyViewModel CustomerOfficialCompany { get; set; } = new CustomerOfficialCompanyViewModel();
        public List<OrderPaymentViewModel> OrderPayments{get;set;}=new List<OrderPaymentViewModel>();
        public List<OrderServiceViewModel> OrderServices{get;set;}=new List<OrderServiceViewModel>();
        public List<OrderDetailViewModel> Details { get; set; } = new List<OrderDetailViewModel>();
        public List<OrderPaymentViewModel> OrderPayment { get; set; } = new List<OrderPaymentViewModel>();
        public List<AttachmentViewModel> Attachments { get; set; } = new List<AttachmentViewModel>();
        public List<CargoAnncViewModel> CargoAnnounces { get; set; } = new List<CargoAnncViewModel>();
    }
}
