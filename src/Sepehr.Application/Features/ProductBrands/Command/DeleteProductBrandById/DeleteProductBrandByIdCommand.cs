using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.ProductBrands.Command.DeleteProductBrandById
{
    public class DeleteProductBrandByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteProductBrandByIdCommandHandler
        : IRequestHandler<DeleteProductBrandByIdCommand, Response<bool>>
        {
            private readonly IProductBrandRepositoryAsync _productBrandRepository;
            ITableRecordRemovalRepositoryAsync _tableRecordRemoval;

            public DeleteProductBrandByIdCommandHandler(
                IProductBrandRepositoryAsync productBrandRepository,
                ITableRecordRemovalRepositoryAsync tableRecordRemoval
            )
            {
                _productBrandRepository = productBrandRepository;
                _tableRecordRemoval = tableRecordRemoval;
            }

            public async Task<Response<bool>>
            Handle(
                DeleteProductBrandByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var productBrand = await _productBrandRepository.GetByIdAsync(command.Id);
                if (productBrand == null)
                    new ErrorMessageFactory().MakeError("برند", ErrorType.NotFound);

                await _tableRecordRemoval.AddAsync(new TableRecordRemovalInfo { RemovedRecordId = productBrand.Id.ToString(), TableName = "productBrand" });

                await _productBrandRepository.DeleteAsync(productBrand);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("برند", ErrorType.DeletedSuccess));
            }
        }
    }
}
