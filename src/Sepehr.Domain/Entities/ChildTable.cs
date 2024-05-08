using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ChildTable
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public int ParentTableId { get; set; }
        public ParentTable ParentTable { get; set; }
    }
}