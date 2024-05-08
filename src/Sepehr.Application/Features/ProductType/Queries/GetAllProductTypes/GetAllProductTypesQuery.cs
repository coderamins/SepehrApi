using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.ProductTypes.Queries.GetAllProductTypes;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.ProductTypes.Queries.GetAllProductTypes
{
    public class GetAllProductTypesQuery : IRequest<PagedResponse<IEnumerable<ProductTypeViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductTypesQueryHandler :
         IRequestHandler<GetAllProductTypesQuery, PagedResponse<IEnumerable<ProductTypeViewModel>>>
    {
        private readonly IProductTypeRepositoryAsync _productTypeRepository;
        private readonly IMapper _mapper;
        public GetAllProductTypesQueryHandler(IProductTypeRepositoryAsync productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<ProductTypeViewModel>>> Handle(
            GetAllProductTypesQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllProductTypesParameter>(request);
                var productType = await _productTypeRepository.GetAllProductTypes();
                
                var productTypeViewModel = _mapper.Map<IEnumerable<ProductTypeViewModel>>(productType);
                return new PagedResponse<IEnumerable<ProductTypeViewModel>>(
                    productTypeViewModel,
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