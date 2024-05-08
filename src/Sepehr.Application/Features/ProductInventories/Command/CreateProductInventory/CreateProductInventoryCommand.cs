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

namespace Sepehr.Application.Features.ProductPrices.Command.CreateProductPrice
{
    public partial class CreateProductInventoryCommand : IRequest<Response<ProductInventory>>
    {
        public int ProductBrandId { get; set; }
        public int Inventory { get; set; }
    }
    public class CreateProductInventoryCommandHandler : IRequestHandler<CreateProductInventoryCommand, Response<ProductInventory>>
    {
        private readonly IProductPriceRepositoryAsync _productPriceRepository;
        private readonly IProductInventoryRepositoryAsync _productInventoryRepository;
        private readonly IMapper _mapper;
        public CreateProductInventoryCommandHandler(
            IProductPriceRepositoryAsync productPriceRepository,
            IProductInventoryRepositoryAsync productInventoryRepository,
            IMapper mapper)
        {
            _productPriceRepository = productPriceRepository;
            _productInventoryRepository = productInventoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductInventory>> Handle(CreateProductInventoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ProductInventory? pbInventory = await _productInventoryRepository
                    .GetProductInventory(request.ProductBrandId, 1);

                var checkHistory = await _productInventoryRepository
                    .CheckInventoryUploadHsitory(request.ProductBrandId);

                if (checkHistory != null)
                    throw new ApiException($"بارگذاری موجودی برای کالابرند {request.ProductBrandId}) در تاریخ امروز انجام شده است !");

                if (pbInventory == null)
                    throw new ApiException($"کد کالای {request.ProductBrandId} در انبار سپهر یافت نشد !");

                pbInventory = _mapper.Map(request, pbInventory);
                await _productInventoryRepository.UpdateAsync(pbInventory);

                return new Response<ProductInventory>(pbInventory,
                    "موجودی محصول با موفقیت بروزرسانی گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}