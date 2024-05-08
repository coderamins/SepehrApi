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

namespace Sepehr.Application.Features.ProductBrands.Command.CreateProductBrand
{
    public partial class CreateProductBrandCommand : IRequest<Response<ProductBrand>>
    {
        public Guid ProductId { get; set; }
        public int BrandId { get; set; }
    }
    public class CreateProductBrandCommandHandler : IRequestHandler<CreateProductBrandCommand, Response<ProductBrand>>
    {
        private readonly IProductBrandRepositoryAsync _brandRepository;
        private readonly IMapper _mapper;
        public CreateProductBrandCommandHandler(
            IProductBrandRepositoryAsync brandRepository, 
            IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductBrand>> Handle(CreateProductBrandCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate =await _brandRepository.GetProductBrandInfo(request.ProductId, request.BrandId);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("برند", ErrorType.DuplicateForCreate));

            //---اگه محصول/برند در جدول موجودی انبار وجود نداشته باشد، رکورد آن ایجاد خواهد شد

            var pbrand = _mapper.Map<ProductBrand>(request);
            await _brandRepository.AddAsync(pbrand);

            return new Response<ProductBrand>(pbrand, new ErrorMessageFactory().MakeError("برند", ErrorType.CreatedSuccess));
        }

    }
}