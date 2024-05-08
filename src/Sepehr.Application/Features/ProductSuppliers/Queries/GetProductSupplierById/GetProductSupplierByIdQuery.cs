using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ProductSuppliers.Queries.GetProductSupplierById
{
    public class GetProductSupplierByIdQuery : IRequest<Response<ProductSupplier>>
    {
        public Guid Id { get; set; }

        public class GetProductSupplierByIdQueryHandler : IRequestHandler<GetProductSupplierByIdQuery, Response<ProductSupplier>>
        {
            private readonly IProductSupplierRepositoryAsync _productSupplierRepository;

            public GetProductSupplierByIdQueryHandler(
                IProductSupplierRepositoryAsync productSupplierRepository
            )
            {
                _productSupplierRepository = productSupplierRepository;
            }

            public async Task<Response<ProductSupplier>>
            Handle(
                GetProductSupplierByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var productSupplier = await _productSupplierRepository.GetByIdAsync(query.Id);
                if (productSupplier == null)
                    throw new ApiException($"محصول یافت نشد !");
                return new Response<ProductSupplier>(productSupplier);
            }
        }
    }
}
