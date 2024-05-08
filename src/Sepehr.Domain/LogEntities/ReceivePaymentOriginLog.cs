using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class ReceivePaymentOriginLog:BaseEntityLog<int>
    {
        public string Desc { get; set; } = string.Empty;
        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }

    }
}
