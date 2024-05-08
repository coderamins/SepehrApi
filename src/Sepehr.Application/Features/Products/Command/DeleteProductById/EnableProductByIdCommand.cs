using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Products.Command.UpdateProduct;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.Products.Command.DeleteProductById
{
    public class EnableProductByIdCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public class
        EnableProductByIdCommandHandler
        : IRequestHandler<EnableProductByIdCommand, Response<bool>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IMapper _mapper;

            public EnableProductByIdCommandHandler(
                IProductRepositoryAsync productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<Response<bool>>
            Handle(
                EnableProductByIdCommand command,
                CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);

                if (product == null)
                    throw new ApiException($"محصول یافت نشد !");

                if(product.IsActive==command.Active)
                    throw new ApiException(string.Format("محصول قبلا {0} شده است !", command.Active ? "فعال" : "غیر فعال"));


                product = _mapper.Map<EnableProductByIdCommand, Product>(command, product);
                product.IsActive = command.Active;
                await _productRepository.UpdateAsync(product);

                return new Response<bool>(true,string.Format("محصول با موفقیت {0} شد .",command.Active ? "فعال":"غیر فعال"));
            }
        }
    }
}
