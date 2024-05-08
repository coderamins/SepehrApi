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

namespace Sepehr.Application.Features.ProductTypes.Command.UpdateProductType
{
    public class UpdateProductTypeCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }

        public class UpdateProductTypeCommandHandler : IRequestHandler<UpdateProductTypeCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IProductTypeRepositoryAsync _productTypeRepository;
            public UpdateProductTypeCommandHandler(IProductTypeRepositoryAsync productTypeRepository, IMapper mapper)
            {
                _productTypeRepository = productTypeRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateProductTypeCommand command, CancellationToken cancellationToken)
            {
                var productType = await _productTypeRepository.GetByIdAsync(command.Id);
                productType = _mapper.Map<UpdateProductTypeCommand, ProductType>(command, productType);

                if (productType == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("نوع کالا", ErrorType.NotFound));
                else
                {
                    await _productTypeRepository.UpdateAsync(productType);
                    return new Response<string>(productType.Id.ToString(), new ErrorMessageFactory().MakeError("نوع کالا", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}