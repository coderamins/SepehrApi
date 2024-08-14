using Sepehr.Application.Features.PersonnelPaymentRequests.Queries.GetAllPersonnelPaymentRequests;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Interfaces.Repositories
{
    public interface IPersonnelPaymentRequestRepositoryAsync : IGenericRepositoryAsync<PersonnelPaymentRequest>
    {
        Task<IEnumerable<PersonnelPaymentRequest>> GetAllPersonnelPaymentRequestsAsync(GetAllPersonnelPaymentRequestsParameter validFilter);
        Task<PersonnelPaymentRequest?> GetPersonnelPaymentRequestInfo(Guid PersonnelPaymentRequestId);
    }
}