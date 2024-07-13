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

namespace Sepehr.Application.Features.Warehouses.Command.CreateWarehouse
{
    public partial class CreateWarehouseCommand : IRequest<Response<Warehouse>>
    {
        public required string Name { get; set; }
        public required int WarehouseTypeId { get; set; }
        public Guid? CustomerId { get; set; }
}
    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Response<Warehouse>>
    {
        private readonly IWarehouseRepositoryAsync _warehouseRepository;
        private readonly IMapper _mapper;
        public CreateWarehouseCommandHandler(
            IWarehouseRepositoryAsync brandRepository, 
            IMapper mapper)
        {
            _warehouseRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<Response<Warehouse>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var allwarehouses =await _warehouseRepository.GetAllAsync();
            if (allwarehouses.Any(w=>w.Name.Equals(request.Name)))
                throw new ApiException(new ErrorMessageFactory().MakeError("انبار", ErrorType.DuplicateForCreate));

            var pbrand = _mapper.Map<Warehouse>(request);
            await _warehouseRepository.CreateWarehouse(pbrand);

            return new Response<Warehouse>(pbrand, new ErrorMessageFactory().MakeError("انبار", ErrorType.CreatedSuccess));
        }

    }
}