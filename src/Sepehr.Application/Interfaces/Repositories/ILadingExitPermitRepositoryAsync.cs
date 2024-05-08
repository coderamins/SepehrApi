using Sepehr.Application.Features.LadingExitPermits.Queries.GetAllLadingExitPermits;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ILadingExitPermitRepositoryAsync : IGenericRepositoryAsync<LadingExitPermit>
    {
        Task<IQueryable<LadingExitPermit>> GetAllLadingExitPermits(GetAllLadingExitPermitsParameter validFilter);
        Task<LadingExitPermit?> GetLadingExitPermitInfo(Guid LadingExitPermitId);
        Task<LadingExitPermit> UpdateLadingExitPermit(LadingExitPermit ladingExitPermit);
    }
}