using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Domain.Common;

namespace Sepehr.Domain.Entities
{
    public class ProductType:BaseEntity<int>
    {
        public required string Desc { get; set; }
        public string? TypeColor { get; set; }
        public int ProductCodeSeedStart { get; set; }
        public byte[]? Image { get; set; }
    }
}