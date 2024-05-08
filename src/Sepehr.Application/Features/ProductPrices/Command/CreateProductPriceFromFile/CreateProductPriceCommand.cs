using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Sepehr.Application.Features.ProductPrices.Command.CreateProductPriceFromFile
{
    public partial class CreateProductPriceFromFileCommand : IRequest<Response<List<ProductPrice>>>
    {
        public required IFormFile PriceFile { get; set; }
    }
    public class CreateProductPriceFromFileCommandHandler : IRequestHandler<CreateProductPriceFromFileCommand, Response<List<ProductPrice>>>
    {
        private readonly IProductPriceRepositoryAsync _productPriceRepository;
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public CreateProductPriceFromFileCommandHandler(IProductPriceRepositoryAsync productPriceRepository,
            IProductRepositoryAsync productRepository,
            IMapper mapper)
        {
            _productPriceRepository = productPriceRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<Response<List<ProductPrice>>> Handle(CreateProductPriceFromFileCommand request, CancellationToken cancellationToken)
        {
            if (request.PriceFile == null || request.PriceFile.Length <= 0)
            {
                throw new ApiException("فایل خالی می باشد !");
            }

            if (!Path.GetExtension(request.PriceFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                throw new ApiException("فرمت فایل نامعتیر می باشد !");
            }

            var priceList = new List<ProductPrice>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await request.PriceFile.CopyToAsync(stream, cancellationToken);

                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        ProductPrice newPrice = new ProductPrice();
                        long productCode = 0;
                        decimal price = 0;
                        int productBrandId = 0;

                        if (
                            !long.TryParse(worksheet.Cells[row, 1].Value.ToString()?.Trim(), out productCode) ||
                            !int.TryParse(worksheet.Cells[row, 2].Value.ToString()?.Trim(), out productBrandId) ||
                            !decimal.TryParse(worksheet.Cells[row, 3].Value.ToString()?.Trim(), out price))
                            throw new ApiException("فرمت فایل نامعتیر می باشد !");

                        var pInfo = await _productRepository.GetProductInfoAsync(productCode);
                        if(pInfo== null)
                            throw new ApiException($"کد کالای {productCode} نامعتبر می باشد !");

                        priceList.Add(new ProductPrice
                        {
                            //ProductId = pInfo.Id,
                            ProductBrandId = productBrandId,
                            Price = price,
                        });
                    }
                }
            }

            if (priceList.GroupBy(g => new { g.ProductBrandId }).Any(g => g.Count() > 1))
                throw new ApiException("فایل دارای رکورد تکراری می باشد !");

            await _productPriceRepository.AddAsync(priceList);
            return new Response<List<ProductPrice>>(priceList, "قیمت محصول با موفقیت ایجاد گردید .");
        }

    }
}