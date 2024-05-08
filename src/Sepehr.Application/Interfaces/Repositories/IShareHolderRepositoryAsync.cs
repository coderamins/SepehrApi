using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IShareHolderRepositoryAsync : IGenericRepositoryAsync<ShareHolder>
    {
        Task<ShareHolder?> GetShareHolderInfo(int ShareHolderCode);
    }
}