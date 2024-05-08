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

namespace Sepehr.Application.Features.ProductStandards.Command.UpdateProductStandard
{
    public class UpdateProductStandardCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public bool IsActive { get; set; }

        public class UpdateProductStandardCommandHandler : IRequestHandler<UpdateProductStandardCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IProductStandardRepositoryAsync _productStandardRepository;
            public UpdateProductStandardCommandHandler(IProductStandardRepositoryAsync ProductStandardRepository, IMapper mapper)
            {
                _productStandardRepository = ProductStandardRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateProductStandardCommand command, CancellationToken cancellationToken)
            {
                var ProductStandard = await _productStandardRepository.GetByIdAsync(command.Id);
                ProductStandard = _mapper.Map<UpdateProductStandardCommand, ProductStandard>(command, ProductStandard);

                if (ProductStandard == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("استاندارد", ErrorType.NotFound));
                else
                {
                    await _productStandardRepository.UpdateAsync(ProductStandard);
                    return new Response<string>(ProductStandard.Id.ToString(), new ErrorMessageFactory().MakeError("استاندارد", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}