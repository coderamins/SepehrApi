using Sepehr.Application.Features.Personnels.Queries.GetAllPersonnels;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IPersonnelRepositoryAsync : IGenericRepositoryAsync<Personnel>
    {
        Task<List<Personnel>> GetAllPersonnels(GetAllPersonnelsParameter filter);
        Task<Personnel> GetPersonnelInfo(string nationalId);
        Task<Personnel?> GetPersonnelInfo(Guid Id);
        Task<Personnel> UpdatePersonnel(Personnel personnel);
    }
}