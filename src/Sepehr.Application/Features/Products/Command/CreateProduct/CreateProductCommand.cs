using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.Products.Command.CreateProduct
{
    public partial class CreateProductCommand : IRequest<Response<Product>>
    {
        public required string ProductName { get; set; }
        public int? ProductTypeId { get; set; }
        public string ProductSize { get; set; } = string.Empty;
        public string ProductThickness { get; set; } = string.Empty;
        public decimal ApproximateWeight { get; set; }
        public int NumberInPackage { get; set; }
        public int? ProductStandardId { get; set; }
        public int? ProductStateId { get; set; }
        public required int ProductMainUnitId { get; set; }
        public int? ProductSubUnitId { get; set; }
        public decimal? ExchangeRate { get; set; }

        public int? MaxInventory { get; set; }//---حداکثر موجودی
        public int? MinInventory { get; set; }//---حداقل موجودی
        public int? InventotyCriticalPoint { get; set; }//---نقطه بحرانی

        public string? Description { get; set; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<Product>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IProductRepositoryAsync productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //request.ProductCode = await GenerateProductCode();

            try
            {
                var product = _mapper.Map<Product>(request);

                //-- ایجاد کد جدید براساس نوع محصول
                int newProdCode = await _productRepository.GenerateNewProductCode(request.ProductTypeId);
                product.ProductCode = newProdCode;

                await _productRepository.AddAsync(product);
                return new Response<Product>(product, "محصول جدید با موفقیت ایجاد گردید .");
            }
            catch (Exception r)
            {

                throw;
            }
        }

    }
}