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

namespace Sepehr.Application.Features.Warehouses.Queries.GetWarehouseById
{
    public class GetWarehouseByIdQuery : IRequest<Response<WarehouseViewModel>>
    {
        public int Id { get; set; }

        public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, Response<WarehouseViewModel>>
        {
            private readonly IWarehouseRepositoryAsync _WarehouseRepository;
            private readonly IMapper _mapper;

            public GetWarehouseByIdQueryHandler(
                IWarehouseRepositoryAsync WarehouseRepository,
                IMapper mapper
            )
            {
                _WarehouseRepository = WarehouseRepository;
                _mapper = mapper;
            }

            public async Task<Response<WarehouseViewModel>>
            Handle(
                GetWarehouseByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var Warehouse = await _WarehouseRepository.GetWarehouseInfo(query.Id);
                if (Warehouse == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("انبار",ErrorType.NotFound));

                return new Response<WarehouseViewModel>(_mapper.Map<WarehouseViewModel>(Warehouse));
            }
        }
    }
}
