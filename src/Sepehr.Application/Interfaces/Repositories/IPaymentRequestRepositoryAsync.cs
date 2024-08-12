using Sepehr.Application.Features.PaymentRequests.Queries.GetAllPaymentRequests;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IPaymentRequestRepositoryAsync : IGenericRepositoryAsync<PaymentRequest>
    {
        Task<IEnumerable<PaymentRequest>> GetAllPaymentRequestsAsync(GetAllPaymentRequestsParameter validFilter);
        Task<PaymentRequest?> GetPaymentRequestInfo(Guid PaymentRequestId);
    }
}