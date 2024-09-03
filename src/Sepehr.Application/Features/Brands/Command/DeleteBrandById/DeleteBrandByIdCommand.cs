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

namespace Sepehr.Application.Features.Brands.Command.DeleteBrandById
{
    public class DeleteBrandByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteBrandByIdCommandHandler
        : IRequestHandler<DeleteBrandByIdCommand, Response<bool>>
        {
            private readonly IBrandRepositoryAsync _brandRepository;
            

            public DeleteBrandByIdCommandHandler(
                IBrandRepositoryAsync brandRepository
                
            )
            {
                _brandRepository = brandRepository;
                
            }

            public async Task<Response<bool>>
            Handle(
                DeleteBrandByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var brand = await _brandRepository.GetByIdAsync(command.Id);
                if (brand == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برند", ErrorType.NotFound));

                await _brandRepository.DeleteAsync(brand);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("برند", ErrorType.DeletedSuccess));
            }
        }
    }
}
