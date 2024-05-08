using Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface ICargoAnnouncementRepositoryAsync : IGenericRepositoryAsync<CargoAnnounce>
    {
        Task<bool> CreateLadingPermit(Guid cargoAnnounceId);
        Task<IQueryable<CargoAnnounce>> GetAllCargoAnnounceAsync(GetAllCargoAnncsParameter validFilter);
        Task<CargoAnnounce> GetCargoAnnounceInfo(Guid id);
        Task<List<CargoAnnounceDetail>> GetCargoAnnouncesByOrderDetailId(int? id);
        Task<CargoAnnounce> UpdateCargoAnncAsync(CargoAnnounce cargoAnnc);
        Task<bool> ValidationForCargoAnnc(Guid orderId);
    }
}