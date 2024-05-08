using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.Brands.Queries.GetAllBrands;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Brands.Queries.GetAllBrands
{
    public class GetAllBrandsQuery : IRequest<PagedResponse<IEnumerable<BrandViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllBrandsQueryHandler :
         IRequestHandler<GetAllBrandsQuery, PagedResponse<IEnumerable<BrandViewModel>>>
    {
        private readonly IBrandRepositoryAsync _brandRepository;
        private readonly IMapper _mapper;
        public GetAllBrandsQueryHandler(IBrandRepositoryAsync brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<BrandViewModel>>> Handle(
            GetAllBrandsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var validFilter = _mapper.Map<GetAllBrandsParameter>(request);
                var brand = await _brandRepository.GetAllAsync();
                
                var brandViewModel = _mapper.Map<IEnumerable<BrandViewModel>>(brand);
                return new PagedResponse<IEnumerable<BrandViewModel>>(
                    brandViewModel.OrderByDescending(p=>p.Id),
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