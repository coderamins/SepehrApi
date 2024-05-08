using Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ILadingLicenseRepositoryAsync : IGenericRepositoryAsync<LadingLicense>
    {
        Task<bool> AttachFiles(ICollection<Attachment>? attachments, int ladingLicenseId);
        Task<List<LadingLicense>> GetAllLadingLicenses(GetAllLadingPermitsParameter filter);
        Task<LadingLicense?> GetLadingLicenseInfo(string desc);
        Task<LadingLicense?> GetLadingLicenseInfo(int Id);
    }
}