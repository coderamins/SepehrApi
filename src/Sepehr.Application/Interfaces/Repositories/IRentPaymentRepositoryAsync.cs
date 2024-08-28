using Sepehr.Application.Features.RentPayments.Queries.GetAllRentPayments;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IRentPaymentRepositoryAsync : IGenericRepositoryAsync<RentPayment>
    {
        Task CreateRentPayment(RentPayment rentPayment);
        Task<IEnumerable<RentPayment>> GetAllRentPaymentsAsync(GetAllRentPaymentsParameter validFilter);
        Task<Tuple<List<LadingExitPermit>?, List<UnloadingPermit>?>> GetAllRentsAsync(
            GetAllRentsToPaymentParameter validParams);
        Task<RentPayment?> GetRentPaymentInfo(int RentPaymentId);
        Task UpdateRentPayment(RentPayment rentPayment);
    }
}