using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ProductSupplierViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }       
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal RentAmount { get; set; }
        public decimal OverPrice { get; set; }
        public string PriceDate { get; set; }
        public int Rate { get; set; }
        public bool IsActive { get; set; }
    }
}
