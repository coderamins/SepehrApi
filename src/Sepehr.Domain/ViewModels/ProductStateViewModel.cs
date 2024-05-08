using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ProductStateViewModel: BaseEntity<int>
    {
        public string Desc { get; set; }
    }
}
