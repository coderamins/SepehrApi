using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Domain.Common
{
    public abstract class BaseEntityLog<T>
    {
        public T Id { get; set; }
        public T MainId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}