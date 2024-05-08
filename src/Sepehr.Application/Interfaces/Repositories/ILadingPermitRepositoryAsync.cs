using Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ILadingPermitRepositoryAsync : IGenericRepositoryAsync<LadingPermit>
    {
        Task<bool> AttachFiles(ICollection<Attachment>? attachments, int ladingPermitId);
        Task<IQueryable<LadingPermit>> GetAllLadingPermits(GetAllLadingPermitsParameter filter);
        Task<LadingPermit?> GetLadingPermitInfo(string desc);
        Task<LadingPermit?> GetLadingPermitInfo(int Id);
    }
}