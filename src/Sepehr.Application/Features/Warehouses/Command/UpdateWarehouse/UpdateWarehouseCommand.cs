using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.Warehouses.Command.UpdateWarehouse
{
    public class UpdateWarehouseCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int WarehouseTypeId { get; set; }
        public Guid? CustomerId { get; set; }

        public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, Response<string>>
        {
            private readonly IMapper _mapper;   
            private readonly IWarehouseRepositoryAsync _WarehouseRepository;
            public UpdateWarehouseCommandHandler(IWarehouseRepositoryAsync WarehouseRepository, IMapper mapper)
            {
                _WarehouseRepository = WarehouseRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken)
            {
                var Warehouse = await _WarehouseRepository.GetByIdAsync(command.Id);
                Warehouse = _mapper.Map<UpdateWarehouseCommand, Warehouse>(command, Warehouse);

                if (Warehouse == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("انبار", ErrorType.NotFound));
                else
                {
                    await _WarehouseRepository.UpdateAsync(Warehouse);
                    return new Response<string>(Warehouse.Id.ToString(), new ErrorMessageFactory().MakeError("انبار", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}