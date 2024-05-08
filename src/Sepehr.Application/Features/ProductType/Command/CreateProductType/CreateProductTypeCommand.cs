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

namespace Sepehr.Application.Features.ProductTypes.Command.CreateProductType
{
    public partial class CreateProductTypeCommand : IRequest<Response<ProductType>>
    {
        public string Desc { get; set; }
    }
    public class CreateProductTypeCommandHandler : IRequestHandler<CreateProductTypeCommand, Response<ProductType>>
    {
        private readonly IProductTypeRepositoryAsync _productTypeRepository;
        private readonly IMapper _mapper;
        public CreateProductTypeCommandHandler(IProductTypeRepositoryAsync productTypeRepository, IMapper mapper)
        {
            _productTypeRepository = productTypeRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductType>> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate = await _productTypeRepository.GetProductTypeInfo(request.Desc);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("نوع کالا", ErrorType.DuplicateForCreate));

            var productType = _mapper.Map<ProductType>(request);
            await _productTypeRepository.AddAsync(productType);
            //throw new ApiException(new ErrorMessageFactory().MakeError("نوع کالا", ErrorType.NotFound);

            return new Response<ProductType>(productType, new ErrorMessageFactory().MakeError("نوع کالا", ErrorType.CreatedSuccess));
        }

   }
}