using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Sepehr.Application.Features.ProductInventories.Command.CreateProductInventoryFromFile
{
    public partial class CreateProductInventoryFromFileCommand : IRequest<Response<bool>>
    {
        public required string InventoryDate { get; set; }
        public required IFormFile PriceFile { get; set; }
    }
    public class CreateProductInventoryFromFileCommandHandler : IRequestHandler<CreateProductInventoryFromFileCommand, Response<bool>>
    {
        private readonly IProductInventoryRepositoryAsync _productInventoryRepository;
        private readonly IProductBrandRepositoryAsync _productBrandRepository;
        private readonly IAuthenticatedUserService _userService;
        private readonly IMapper _mapper;
        public CreateProductInventoryFromFileCommandHandler(
            IProductInventoryRepositoryAsync productInventoryRepository,
            IProductBrandRepositoryAsync productBrandRepository,
            IAuthenticatedUserService userService,
            IMapper mapper)
        {
            _productInventoryRepository = productInventoryRepository;
            _mapper = mapper;
            _productBrandRepository = productBrandRepository;
            _userService = userService;
        }

        public async Task<Response<bool>> Handle(CreateProductInventoryFromFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PriceFile == null || request.PriceFile.Length <= 0)
                {
                    throw new ApiException("فایل خالی می باشد !");
                }

                string fileExtension = Path.GetExtension(request.PriceFile.FileName);
                string[] validExtensions = { ".xls", ".csv", ".xlsx" };
                if (!validExtensions.Contains(Path.GetExtension(request.PriceFile.FileName).ToLower()))
                {
                    throw new ApiException("فرمت فایل نامعتیر می باشد !");
                }

                var inventoryHistoryList = new List<ProductInventoryHistory>();
                var newPInventoryList = new List<ProductInventory>();
                var existPInventoryList = new List<ProductInventory>();
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
                            ProductInventory newPrice = new ProductInventory();

                            int productBrandId = 0;
                            int productCode = 0;
                            string productName = "";
                            string BrandName = "";
                            int WarehouseId = 0;
                            int BrandId = 0;
                            decimal ApproximateInventory = 0;
                            double FloorInventory = 0;
                            long MaxInventory = 0;
                            double MinInventory = 0;
                            decimal Thickness = 0;
                            double OrderPoint = 0;
                            string priceDate = "";

                            if (
                                !int.TryParse(worksheet.Cells[row, 1].Value.ToString()?.Trim(), out productBrandId) ||
                                !int.TryParse(worksheet.Cells[row, 2].Value.ToString()?.Trim(), out productCode) ||
                                !int.TryParse(worksheet.Cells[row, 4].Value.ToString()?.Trim(), out BrandId) ||
                                !decimal.TryParse(worksheet.Cells[row, 6].Value.ToString()?.Trim(), out ApproximateInventory))
                                throw new ApiException("فرمت فایل نامعتیر می باشد !");

                            productName = worksheet.Cells[row, 3].Value.ToString()?.Trim() ?? "";
                            BrandName = worksheet.Cells[row, 5].Value.ToString()?.Trim() ?? "";

                            var checkHistory = await _productInventoryRepository.CheckInventoryUploadHsitory(productBrandId);
                            if (checkHistory != null && checkHistory.priceDate==request.InventoryDate)
                                throw new ApiException($"بارگذاری موجودی برای کالابرند {productName}({BrandName}) در این تاریخ انجام شده است !");

                            var productBrandInfo =await _productBrandRepository.GetProductBrandInfo(productCode, BrandId);
                            if (productBrandInfo == null)
                                throw new ApiException($"کد کالای {productCode} یافت نشد !");

                            else if(!productBrandInfo.ProductInventories.Any(x=>x.WarehouseId==1))
                                throw new ApiException($"کد کالای {productCode} در انبار سپهر یافت نشد !");

                            ProductInventory? pbInventory = await _productInventoryRepository.GetProductInventory(productBrandId, 1);
                            if (pbInventory == null)
                            {
                                newPInventoryList.Add(new ProductInventory
                                {
                                    ProductBrandId = productBrandId,
                                    MaxInventory = MaxInventory,
                                    WarehouseId = 1,
                                    ApproximateInventory = ApproximateInventory,
                                    FloorInventory = FloorInventory,
                                    OrderPoint = OrderPoint,
                                    MinInventory = MinInventory,
                                    IsActive = true,
                                    CreatedBy = Guid.Parse(_userService.UserId),
                                });
                            }
                            else
                            {
                                pbInventory.ProductBrandId = productBrandId;
                                pbInventory.MaxInventory = MaxInventory;
                                pbInventory.WarehouseId = 1;
                                pbInventory.ApproximateInventory = ApproximateInventory;
                                pbInventory.FloorInventory = FloorInventory;
                                pbInventory.OrderPoint = OrderPoint;
                                pbInventory.MinInventory = MinInventory;
                                pbInventory.IsActive = true;

                                existPInventoryList.Add(pbInventory);
                            }

                            inventoryHistoryList.Add(new ProductInventoryHistory
                            {
                                productBrandId = productBrandId,
                                productCode = productCode,
                                productName = productName,
                                ApproximateInventory = (int)ApproximateInventory,
                                BrandName = productName,
                                BrandId = BrandId,
                                CreatedBy = Guid.Parse(_userService.UserId),
                                priceDate = request.InventoryDate,
                                Created = DateTime.Now,
                                IsActive = true,
                            });
                        }
                    }
                }

                if (newPInventoryList.GroupBy(g => new { g.ProductBrandId }).Any(g => g.Count() > 1))
                    throw new ApiException("فایل دارای رکورد تکراری می باشد !");

                await _productInventoryRepository.CreateInventoryHistory(inventoryHistoryList);

                if (newPInventoryList.Count() > 0)
                    await _productInventoryRepository.AddAsync(newPInventoryList);

                if (existPInventoryList.Count() > 0)
                    await _productInventoryRepository.UpdateAsync(existPInventoryList);


                return new Response<bool>(true, "موجودی محصول با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}