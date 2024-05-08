using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Serilog;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.ProductStates.Command.CreateProductState
{
    public partial class CreateProductStateCommand : IRequest<Response<ProductState>>
    {
        public string Desc { get; set; }
    }
    public class CreateProductStateCommandHandler : IRequestHandler<CreateProductStateCommand, Response<ProductState>>
    {
        private readonly IProductStateRepositoryAsync _productStateRepository;
        private readonly IMapper _mapper;
        public CreateProductStateCommandHandler(IProductStateRepositoryAsync productStateRepository, IMapper mapper)
        {
            _productStateRepository = productStateRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductState>> Handle(CreateProductStateCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate = await _productStateRepository.GetProductStateInfo(request.Desc);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("حالت", ErrorType.DuplicateForCreate));

            var productState = _mapper.Map<ProductState>(request);
            await _productStateRepository.AddAsync(productState);
            //throw new ApiException(new ErrorMessageFactory().MakeError("برند", ErrorType.NotFound);

            return new Response<ProductState>(productState, new ErrorMessageFactory().MakeError("حالت", ErrorType.CreatedSuccess));
        }

    }
}