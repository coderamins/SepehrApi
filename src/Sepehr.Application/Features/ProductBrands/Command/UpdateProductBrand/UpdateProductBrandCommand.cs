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

namespace Sepehr.Application.Features.ProductBrands.Command.UpdateProductBrand
{
    public class UpdateProductBrandCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public int BrandId { get; set; }
        public bool IsActive { get; set; }

        public class UpdateProductBrandCommandHandler : IRequestHandler<UpdateProductBrandCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IProductBrandRepositoryAsync _productBrandRepository;
            public UpdateProductBrandCommandHandler(IProductBrandRepositoryAsync productBrandRepository, IMapper mapper)
            {
                _productBrandRepository = productBrandRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateProductBrandCommand command, CancellationToken cancellationToken)
            {
                var productBrand = await _productBrandRepository.GetByIdAsync(command.Id);
                productBrand = _mapper.Map<UpdateProductBrandCommand, ProductBrand>(command, productBrand);

                if (productBrand == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برند", ErrorType.NotFound));
                else
                {
                    await _productBrandRepository.UpdateAsync(productBrand);
                    return new Response<string>(productBrand.Id.ToString(), new ErrorMessageFactory().MakeError("برند", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}