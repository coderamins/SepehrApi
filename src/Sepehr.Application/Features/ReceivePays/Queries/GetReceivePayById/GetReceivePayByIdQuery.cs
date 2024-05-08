using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ReceivePays.Queries.GetReceivePayById
{
    public class GetReceivePayByIdQuery : IRequest<Response<ReceivePayViewModel>>
    {
        public Guid Id { get; set; }

        public class GetReceivePayByIdQueryHandler : IRequestHandler<GetReceivePayByIdQuery, Response<ReceivePayViewModel>>
        {
            private readonly IReceivePayRepositoryAsync _receivePayRepository;
            private readonly IMapper _mapper;
            public GetReceivePayByIdQueryHandler(
                IReceivePayRepositoryAsync receivePayRepository,
                IMapper mapper
            )
            {
                _receivePayRepository = receivePayRepository;
                _mapper = mapper;
            }

            public async Task<Response<ReceivePayViewModel>> Handle(GetReceivePayByIdQuery query,CancellationToken cancellationToken)
            {
                var receivePay = await _receivePayRepository.GetReceivePayByIdAsync(query.Id);
                var receivePayViewModel = _mapper.Map<ReceivePayViewModel>(receivePay);

                if (receivePay == null)
                    throw new ApiException($"رکوردی یافت نشد !");
                return new Response<ReceivePayViewModel>(receivePayViewModel);
            }
        }
    }
}
