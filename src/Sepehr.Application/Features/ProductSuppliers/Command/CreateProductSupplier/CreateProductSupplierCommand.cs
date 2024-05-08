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

namespace Sepehr.Application.Features.ProductSuppliers.Command.CreateProductSupplier
{
    public partial class CreateProductSupplierCommand : IRequest<Response<ProductSupplier>>
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// کرایه تمام شده
        /// </summary>
        public decimal RentAmount { get; set; }
        /// <summary>
        /// قیمت تمام شده
        /// </summary>
        public decimal OverPrice { get; set; }
        public string PriceDate { get; set; }
        public int Rate { get; set; }
    }
    public class CreateProductSupplierCommandHandler : IRequestHandler<CreateProductSupplierCommand, Response<ProductSupplier>>
    {
        private readonly IProductSupplierRepositoryAsync _productSupplierRepository;
        private readonly IMapper _mapper;
        public CreateProductSupplierCommandHandler(IProductSupplierRepositoryAsync productSupplierRepository, IMapper mapper)
        {
            _productSupplierRepository = productSupplierRepository;
            _mapper = mapper;
        }        

        public async Task<Response<ProductSupplier>> Handle(CreateProductSupplierCommand request, CancellationToken cancellationToken)
        {
            var productSupplier = _mapper.Map<ProductSupplier>(request);
            await _productSupplierRepository.AddAsync(productSupplier);
            return new Response<ProductSupplier>(productSupplier, "اطلاعات مشتری جدید با موفقیت ایجاد گردید .");
        }

    }
}