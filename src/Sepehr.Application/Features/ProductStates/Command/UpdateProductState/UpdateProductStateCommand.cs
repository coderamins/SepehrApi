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
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ProductStates.Command.UpdateProductState
{
    public class UpdateProductStateCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public class UpdateProductStateCommandHandler : IRequestHandler<UpdateProductStateCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IProductStateRepositoryAsync _productStateRepository;
            public UpdateProductStateCommandHandler(IProductStateRepositoryAsync productStateRepository, IMapper mapper)
            {
                _productStateRepository = productStateRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateProductStateCommand command, CancellationToken cancellationToken)
            {
                var productState = await _productStateRepository.GetByIdAsync(command.Id);
                productState = _mapper.Map<UpdateProductStateCommand, ProductState>(command, productState);

                if (productState == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برند", ErrorType.NotFound));
                else
                {
                    await _productStateRepository.UpdateAsync(productState);
                    return new Response<string>(productState.Id.ToString(), new ErrorMessageFactory().MakeError("برند", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}