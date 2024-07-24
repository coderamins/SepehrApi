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

namespace Sepehr.Application.Features.EntrancePermits.Queries.GetEntrancePermitById
{
    public class GetEntrancePermitByIdQuery : IRequest<Response<EntrancePermitViewModel>>
    {
        public Guid Id { get; set; }

        public class GetEntrancePermitByIdQueryHandler : IRequestHandler<GetEntrancePermitByIdQuery, Response<EntrancePermitViewModel>>
        {
            private readonly IEntrancePermitRepositoryAsync _EntrancePermitRepository;
            private readonly IMapper _mapper;
            public GetEntrancePermitByIdQueryHandler(
                IEntrancePermitRepositoryAsync EntrancePermitRepository,
                IMapper mapper
            )
            {
                _EntrancePermitRepository = EntrancePermitRepository;
                _mapper= mapper;
            }

            public async Task<Response<EntrancePermitViewModel>>
            Handle(
                GetEntrancePermitByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var _entrancePermit = await _EntrancePermitRepository.GetEntrancePermitById(query.Id);
                if (_entrancePermit == null)
                    throw new ApiException($"سفارش یافت نشد !");

                var _entrancePermitVM=_mapper.Map<EntrancePermitViewModel>(_entrancePermit);

                return new Response<EntrancePermitViewModel>(_entrancePermitVM);
            }
        }
    }
}
