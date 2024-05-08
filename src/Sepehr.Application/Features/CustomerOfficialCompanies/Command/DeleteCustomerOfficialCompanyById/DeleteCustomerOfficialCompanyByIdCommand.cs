using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;

namespace Sepehr.Application.Features.CustomerOfficialCompanys.Command.DeleteCustomerOfficialCompanyById
{
    public class DeleteCustomerOfficialCompanyByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteCustomerOfficialCompanyByIdCommandHandler
        : IRequestHandler<DeleteCustomerOfficialCompanyByIdCommand, Response<bool>>
        {
            private readonly ICustomerOfficialCompanyRepositoryAsync _customerOfficialCompanyRepository;

            public DeleteCustomerOfficialCompanyByIdCommandHandler(
                ICustomerOfficialCompanyRepositoryAsync customerOfficialCompanyRepository
            )
            {
                _customerOfficialCompanyRepository = customerOfficialCompanyRepository;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteCustomerOfficialCompanyByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var customerOfficialCompany = await _customerOfficialCompanyRepository.GetByIdAsync(command.Id);
                if (customerOfficialCompany == null)
                    throw new ApiException($"محصول یافت نشد !");
                 await _customerOfficialCompanyRepository.DeleteAsync(customerOfficialCompany);
                return new Response<bool>(true,"محصول با موفقیت حذف شد .");
            }
        }
    }
}
