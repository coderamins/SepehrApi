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

namespace Sepehr.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Response<string>>
    {
        public required Guid Id { get; set; }
        public required string ProductName { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductSize { get; set; } = string.Empty;
        public string ProductThickness { get; set; } = string.Empty;
        public decimal ApproximateWeight { get; set; }
        public int NumberInPackage { get; set; }
        public int? ProductStandardId { get; set; }
        public int? ProductStateId { get; set; }
        public int ProductMainUnitId { get; set; }
        public int? ProductSubUnitId { get; set; }
        public decimal? ExchangeRate { get; set; }

        public int MaxInventory { get; set; }//---حداکثر موجودی
        public int MinInventory { get; set; }//---حداقل موجودی
        public int InventotyCriticalPoint { get; set; }//---نقطه بحرانی


        public string? Description { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepositoryAsync _productRepository;
            public UpdateProductCommandHandler(IProductRepositoryAsync productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);
                product = _mapper.Map<UpdateProductCommand, Product>(command, product);

                if (product == null)
                {
                    throw new ApiException($"محصول یافت نشد !");
                }
                else
                {
                    await _productRepository.UpdateAsync(product);
                    return new Response<string>(product.Id.ToString(), "");
                }
            }
        }
    }
}