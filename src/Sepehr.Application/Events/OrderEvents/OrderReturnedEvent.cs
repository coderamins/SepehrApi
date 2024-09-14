using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Events.OrderEvents
{
    public class OrderReturnedEvent : INotification
    {
        public Guid OrderId { get; set; }
        public List<Guid> ReturnedItemIds { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ReturnReason { get; set; }
    }
}
