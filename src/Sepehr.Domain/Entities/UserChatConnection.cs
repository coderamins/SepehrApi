using Sepehr.Domain.Common;
using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class UserChatConnection
    {
        public string UserName { get; set; }
        public DateTime JoinedAt { get; set; }
    }
}
