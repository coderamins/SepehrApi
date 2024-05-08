using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum ProductSortBase
    {
        None=0,
        Ascending=1,
        Descending=2,
        ByLowInventory=3, 
        ByHighInventory=4,
    }
}
