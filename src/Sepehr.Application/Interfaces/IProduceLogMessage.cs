using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces
{
    public interface IProduceLogMessage
    {
        Task CreateMessage(string tableName,object data);
    }
}
