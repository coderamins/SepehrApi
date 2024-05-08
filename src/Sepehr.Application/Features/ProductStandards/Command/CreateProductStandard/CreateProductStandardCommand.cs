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

namespace Sepehr.Application.Features.ProductStandards.Command.CreateProductStandard
{
    public partial class CreateProductStandardCommand : IRequest<Response<ProductStandard>>
    {
        public string Desc { get; set; }
    }
    public class CreateProductStandardCommandHandler : IRequestHandler<CreateProductStandardCommand, Response<ProductStandard>>
    {
        private readonly IProductStandardRepositoryAsync _productStandardRepository;
        private readonly IMapper _mapper;
        public CreateProductStandardCommandHandler(IProductStandardRepositoryAsync ProductStandardRepository, IMapper mapper)
        {
            _productStandardRepository = ProductStandardRepository;
            _mapper = mapper;
        }

        public async Task<Response<ProductStandard>> Handle(CreateProductStandardCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate = await _productStandardRepository.GetProductStandardInfo(request.Desc);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("استاندارد", ErrorType.DuplicateForCreate));

            var ProductStandard = _mapper.Map<ProductStandard>(request);
            await _productStandardRepository.AddAsync(ProductStandard);
            //throw new ApiException(new ErrorMessageFactory().MakeError("استاندارد", ErrorType.NotFound);

            return new Response<ProductStandard>(ProductStandard, new ErrorMessageFactory().MakeError("استاندارد", ErrorType.CreatedSuccess));
        }

   }
}