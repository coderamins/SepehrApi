using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ProductTypeViewModel : BaseEntity<int>
    {
        public string Desc { get; set; }
        public string Color { get; set; }

        public List<ProductViewModel>? Products { get; set; }
    }
}
