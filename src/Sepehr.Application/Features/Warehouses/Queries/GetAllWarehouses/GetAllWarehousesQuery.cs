using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.Warehouses.Queries.GetAllWarehouses;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Warehouses.Queries.GetAllWarehouses
{
    public class GetAllWarehousesQuery : IRequest<Response<IEnumerable<WarehouseViewModel>>>
    {
        public int? WarehouseTypeId { get; set; }
        public Guid? CustomerId { get; set; }
    }
    public class GetAllWarehousesQueryHandler :
         IRequestHandler<GetAllWarehousesQuery, Response<IEnumerable<WarehouseViewModel>>>
    {
        private readonly IWarehouseRepositoryAsync _WarehouseRepository;
        private readonly IMapper _mapper;
        public GetAllWarehousesQueryHandler(IWarehouseRepositoryAsync WarehouseRepository, IMapper mapper)
        {
            _WarehouseRepository = WarehouseRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<WarehouseViewModel>>> Handle(
            GetAllWarehousesQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllWarehousesParameter>(request);
                var Warehouse = 
                    await _WarehouseRepository.GetAllWarehousesAsync(request.WarehouseTypeId,request.CustomerId);

                var WarehouseViewModel = _mapper.Map<IEnumerable<WarehouseViewModel>>(Warehouse);
                return new Response<IEnumerable<WarehouseViewModel>>(WarehouseViewModel);
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}