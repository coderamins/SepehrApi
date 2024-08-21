using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Serilog;

namespace Sepehr.Application.Features.Brands.Command.CreateBrand
{
    public partial class CreateBrandCommand : IRequest<Response<Brand>>
    {
        public string Name { get; set; }
    }
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Response<Brand>>
    {
        private readonly IBrandRepositoryAsync _brandRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public CreateBrandCommandHandler(
            IBrandRepositoryAsync brandRepository, 
            ILogger logger,
            IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<Brand>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            _logger.Information(JsonConvert.SerializeObject(request));

            var checkDuplicate =await _brandRepository.GetBrandInfo(request.Name);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("برند", ErrorType.DuplicateForCreate));

            var brand = _mapper.Map<Brand>(request);
            await _brandRepository.AddAsync(brand);

            return new Response<Brand>(brand, new ErrorMessageFactory().MakeError("برند", ErrorType.CreatedSuccess));
        }

    }
}