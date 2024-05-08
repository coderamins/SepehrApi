using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ProductPrices.Command.CreateProductPrice
{
    public partial class CreateProductPriceCommand : IRequest<Response<ProductPrice>>
    {
        public decimal Price { get; set; }
        //public Guid ProductId { get; set; }
        public int ProductBrandId { get; set; }
    }
    public class CreateProductPriceCommandHandler : IRequestHandler<CreateProductPriceCommand, Response<ProductPrice>>
    {
        private readonly IProductPriceRepositoryAsync _productPriceRepository;
        private readonly IMapper _mapper;
        public CreateProductPriceCommandHandler(IProductPriceRepositoryAsync productPriceRepository, IMapper mapper)
        {
            _productPriceRepository = productPriceRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductPrice>> Handle(CreateProductPriceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productPrice = _mapper.Map<ProductPrice>(request);
                await _productPriceRepository.AddAsync(productPrice);
                return new Response<ProductPrice>(productPrice, "قیمت محصول با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}