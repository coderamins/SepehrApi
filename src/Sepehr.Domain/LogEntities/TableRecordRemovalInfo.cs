using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.LogEntities
{
    public class TableRecordRemovalInfo
    {
        public int Id { get; set; }
        public required string RemovedRecordId { get; set; }
        public string? TableName { get; set; }
        public DateTime Created { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
