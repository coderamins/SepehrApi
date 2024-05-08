using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ParentTable
    {
        public int Id { get; set; }
        public string ParentName { get; set; }
        public ChildTable ChildTable { get; set; }
    }


}