using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sepehr.Application.DTOs
{
    public class AttachmentDto
    {
        public required string FileData { get; set; }
        [JsonIgnore]
        public AttachmentType AttachmentType { get; set; }
    }
}
