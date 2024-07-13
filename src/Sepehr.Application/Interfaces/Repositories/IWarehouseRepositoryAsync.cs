using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IWarehouseRepositoryAsync : IGenericRepositoryAsync<Warehouse>
    {
        Task<Warehouse> CreateWarehouse(Warehouse pbrand);
        Task<List<Warehouse>> GetAllWarehousesAsync(int? WarehouseTypeId,Guid? CustomerId);
        Task<Warehouse> GetWarehouseInfo(string name);
        Task<Warehouse?> GetWarehouseInfo(int Id);
    }
}