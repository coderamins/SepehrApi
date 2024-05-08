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
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ProductPrices.Command.UpdateProductPrice
{
    public class UpdateProductPriceCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IProductPriceRepositoryAsync _productPriceRepository;
            public UpdateProductPriceCommandHandler(IProductPriceRepositoryAsync productPriceRepository, IMapper mapper)
            {
                _productPriceRepository = productPriceRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateProductPriceCommand command, CancellationToken cancellationToken)
            {
                var productPrice = await _productPriceRepository.GetByIdAsync(command.Id);
                if (!productPrice.IsActive)
                    throw new ApiException("این قیمت فعال نیست و قابل ویرایش نمی باشد !");

                productPrice = _mapper.Map<UpdateProductPriceCommand, ProductPrice>(command, productPrice);

                if (productPrice == null)
                {
                    throw new ApiException($"قیمت محصول یافت نشد !");
                }
                else
                {
                    await _productPriceRepository.UpdateAsync(productPrice);
                    return new Response<string>(productPrice.Id.ToString(), "");
                }
            }
        }
    }
}