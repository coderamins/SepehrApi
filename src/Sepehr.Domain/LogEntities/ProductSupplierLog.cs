using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ProductSupplierLog:AuditableBaseEntityLog<Guid>
    {
        /// <summary>
        /// کد تامین کننده
        /// </summary>
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        /// <summary>
        /// کرایه تمام شده
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal RentAmount { get; set; }
        /// <summary>
        /// قیمت تمام شده
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal OverPrice { get; set; }
        public DateTime PriceDate { get; set; }
        public int Rate { get; set; }

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }

    }
}
