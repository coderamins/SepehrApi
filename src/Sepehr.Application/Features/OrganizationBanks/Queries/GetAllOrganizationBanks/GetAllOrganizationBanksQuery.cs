using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.OrganizationBanks.Queries.GetAllOrganizationBanks
{
    public class GetAllOrganizationBanksQuery : IRequest<Response<IEnumerable<OrganizationBankViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllOrganizationBanksQueryHandler :
         IRequestHandler<GetAllOrganizationBanksQuery, Response<IEnumerable<OrganizationBankViewModel>>>
    {
        private readonly IOrganizationBankRepositoryAsync _organizationBankRepository;
        private readonly IMapper _mapper;
        public GetAllOrganizationBanksQueryHandler(IOrganizationBankRepositoryAsync organizationBankRepository, IMapper mapper)
        {
            _organizationBankRepository = organizationBankRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<OrganizationBankViewModel>>> Handle(
            GetAllOrganizationBanksQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var organizationBanks =
                    await _organizationBankRepository
                    .LoadAllWithRelatedAsync<OrganizationBank>(
                    p => p.Bank);

                var organizationBankViewModel = _mapper.Map<IEnumerable<OrganizationBankViewModel>>(organizationBanks);
                return new Response<IEnumerable<OrganizationBankViewModel>>(organizationBankViewModel);
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}