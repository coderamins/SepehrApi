using Sepehr.Domain.Common;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class AttachmentLog:BaseEntityLog<Guid>
    {
        public required byte[] FileData { get; set; }
        public Guid? ReceivePayId { get; set; }

        public int LogTypeId { get; set; }
        public virtual LogType LogType { get; set; }


    }
}
