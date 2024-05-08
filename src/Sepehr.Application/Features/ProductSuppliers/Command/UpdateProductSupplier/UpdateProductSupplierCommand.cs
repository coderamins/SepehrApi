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
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.ProductSuppliers.Command.UpdateProductSupplier
{
    public class UpdateProductSupplierCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
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

        public class UpdateProductSupplierCommandHandler : IRequestHandler<UpdateProductSupplierCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IProductSupplierRepositoryAsync _productSupplierRepository;
            public UpdateProductSupplierCommandHandler(IProductSupplierRepositoryAsync productSupplierRepository, IMapper mapper)
            {
                _productSupplierRepository = productSupplierRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateProductSupplierCommand command, CancellationToken cancellationToken)
            {
                var productSupplier = await _productSupplierRepository.GetByIdAsync(command.Id);
                productSupplier = _mapper.Map<UpdateProductSupplierCommand, ProductSupplier>(command, productSupplier);

                if (productSupplier == null)
                {
                    throw new ApiException($"محصول یافت نشد !");
                }
                else
                {
                    await _productSupplierRepository.UpdateAsync(productSupplier);
                    return new Response<string>(productSupplier.Id.ToString(), "");
                }
            }
        }
    }
}