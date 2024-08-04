using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.CustomerLabels.Queries.GetCustomerLabelById
{
    public class GetCustomerLabelByIdQuery : IRequest<Response<CustomerLabel>>
    {
        public Guid Id { get; set; }

        public class GetCustomerLabelByIdQueryHandler : IRequestHandler<GetCustomerLabelByIdQuery, Response<CustomerLabel>>
        {
            private readonly ICustomerLabelRepositoryAsync _customerLabelRepository;

            public GetCustomerLabelByIdQueryHandler(
                ICustomerLabelRepositoryAsync customerLabelRepository
            )
            {
                _customerLabelRepository = customerLabelRepository;
            }

            public async Task<Response<CustomerLabel>>
            Handle(
                GetCustomerLabelByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var customerLabel = await _customerLabelRepository.GetByIdAsync(query.Id);
                if (customerLabel == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برچسب مشتری",ErrorType.NotFound));
                return new Response<CustomerLabel>(customerLabel);
            }
        }
    }
}
