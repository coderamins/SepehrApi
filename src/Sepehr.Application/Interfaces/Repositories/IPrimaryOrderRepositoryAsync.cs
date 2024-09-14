using Sepehr.Application.Features.DraftOrders.Queries.GetAllDraftOrders;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IDraftOrderRepositoryAsync : IGenericRepositoryAsync<DraftOrder>
    {
        Task<List<DraftOrder>> GetAllDraftOrders(GetAllDraftOrdersParameter filter);
        Task<DraftOrder?> GetDraftOrderById(int id);
    }
}