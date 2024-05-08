using Sepehr.Domain.Entities;
using Sepehr.Domain.LogEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ITableRecordRemovalRepositoryAsync : IGenericRepositoryLogAsync<TableRecordRemovalInfo>
    {
    }
}
