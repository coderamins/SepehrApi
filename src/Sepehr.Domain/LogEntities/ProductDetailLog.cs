using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ProductDetailLog : BaseEntityLog<int>
    {
        public string? Size { get; set; }
        public string? Standard { get; set; }
        public string? ProductState { get; set; }
        public Guid ProductId { get; set; }

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }

    }
}
