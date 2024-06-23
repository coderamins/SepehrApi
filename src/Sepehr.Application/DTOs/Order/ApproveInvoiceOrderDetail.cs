using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs.Order
{
    public class ApproveInvoiceOrderDetail
    {
        public int Id { get; set; }

        /// <summary>
        /// کد کالای جایگزین
        /// </summary>
        public int? AlternativeProductBrandId { get; set; }
        /// <summary>,
        /// مقدار کالای جایگزین
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal AlternativeProductAmount { get; set; }
        /// <summary>
        /// قیمت کالای جایگزین
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal AlternativeProductPrice { get; set; }
        public decimal ProximateAmount { get; set; }

    }
}
