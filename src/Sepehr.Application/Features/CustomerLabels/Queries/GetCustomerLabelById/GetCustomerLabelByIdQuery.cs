using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.CustomerLabels.Queries.GetCustomerLabelById
{
    public class GetCustomerLabelByIdQuery : IRequest<Response<CustomerLabelViewModel>>
    {
        public int Id { get; set; }

        public class GetCustomerLabelByIdQueryHandler : IRequestHandler<GetCustomerLabelByIdQuery, Response<CustomerLabelViewModel>>
        {
            private readonly ICustomerLabelRepositoryAsync _customerLabelRepository;
            private readonly IMapper _mapper;

            public GetCustomerLabelByIdQueryHandler(
                ICustomerLabelRepositoryAsync customerLabelRepository,
                IMapper mapper
            )
            {
                _customerLabelRepository = customerLabelRepository;
                _mapper = mapper;
            }

            public async Task<Response<CustomerLabelViewModel>>
            Handle(
                GetCustomerLabelByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var customerLabel = await _customerLabelRepository.GetCustomerLabelInfo(query.Id);
                if (customerLabel == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برچسب مشتری",ErrorType.NotFound));

               var mappedCustoemrLabel= _mapper.Map<CustomerLabelViewModel>(customerLabel);

                return new Response<CustomerLabelViewModel>(mappedCustoemrLabel);
            }
        }
    }
}
