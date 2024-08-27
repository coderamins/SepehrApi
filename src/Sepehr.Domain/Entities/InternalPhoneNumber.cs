using Sepehr.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Entities
{
    public class InternalPhoneNumber:BaseEntity<int>
    {
        public int PhonebookId { get; set; }
        public string InternalNumber { get; set; }
        public string PersonName { get; set; }

        public virtual Phonebook Phonebook { get; set; }    
    }
}
