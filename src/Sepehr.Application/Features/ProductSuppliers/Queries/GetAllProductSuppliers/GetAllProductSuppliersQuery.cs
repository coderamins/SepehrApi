using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.ProductSuppliers.Queries.GetAllProducts;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductSuppliers.Queries.GetAllProductSuppliers
{
    public class GetAllProductSuppliersQuery : IRequest<PagedResponse<IEnumerable<ProductSupplierViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductSupplierQueryHandler :
         IRequestHandler<GetAllProductSuppliersQuery, PagedResponse<IEnumerable<ProductSupplierViewModel>>>
    {
        private readonly IProductSupplierRepositoryAsync _productSupplierRepository;
        private readonly IMapper _mapper;
        public GetAllProductSupplierQueryHandler(IProductSupplierRepositoryAsync productSupplierRepository, IMapper mapper)
        {
            _productSupplierRepository = productSupplierRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProductSupplierViewModel>>> Handle(
            GetAllProductSuppliersQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllProductSuppliersParameter>(request);
                var productSupplier = await _productSupplierRepository.GetAllProductSuppliers();

                var productSupplierViewModel = _mapper.Map<IEnumerable<ProductSupplierViewModel>>(productSupplier);
                return new PagedResponse<IEnumerable<ProductSupplierViewModel>>(
                    productSupplierViewModel,
                    validFilter.PageNumber,
                    validFilter.PageSize);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}