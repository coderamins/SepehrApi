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

namespace Sepehr.Application.Features.Brands.Command.UpdateBrand
{
    public class UpdateBrandCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IBrandRepositoryAsync _brandRepository;
            public UpdateBrandCommandHandler(IBrandRepositoryAsync brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateBrandCommand command, CancellationToken cancellationToken)
            {
                var brand = await _brandRepository.GetByIdAsync(command.Id);
                brand = _mapper.Map<UpdateBrandCommand, Brand>(command, brand);

                if (brand == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("برند", ErrorType.NotFound));
                else
                {
                    await _brandRepository.UpdateAsync(brand);
                    return new Response<string>(brand.Id.ToString(), new ErrorMessageFactory().MakeError("برند", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}