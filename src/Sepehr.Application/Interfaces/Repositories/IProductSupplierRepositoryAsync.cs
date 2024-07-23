using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IProductSupplierRepositoryAsync : IGenericRepositoryAsync<ProductSupplier>
    {
        Task<List<ProductSupplier>> GetAllProductSuppliers();
        Task<ProductSupplier?> GetProductSupplierById(Guid suppId);
    }
}