using Sepehr.Application.Features.EntrancePermits.Queries.GetAllEntrancePermits;
using Sepehr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IEntrancePermitRepositoryAsync : IGenericRepositoryAsync<EntrancePermit>
    {
        Task<EntrancePermit> CreateEntrancePermit(EntrancePermit entrancePermit);
        Task<List<EntrancePermit>> GetAllEntrancePermitsAsync(GetAllEntrancePermitsParameter validFilter);
    }
}
