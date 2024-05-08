using AutoMapper;
using Sepehr.Application.Features.ReceivePays.Queries.GetAllReceivePays;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IReceivePayRepositoryAsync : IGenericRepositoryAsync<ReceivePay>
    {
        Task<List<ReceivePay>> GetAllReceivePays(GetAllReceivePaysParameter filter);
        Task<ReceivePay> GetReceivePayByIdAsync(Guid id);
        Task<IList<ReceivePay>> GetReceivePays(IEnumerable<Guid> receivePays);
        Task<ReceivePay> UpdateReceivePayAsync(ReceivePay receivePayRepository);
    }
}