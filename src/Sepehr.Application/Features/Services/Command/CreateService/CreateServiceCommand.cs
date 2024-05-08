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

namespace Sepehr.Application.Features.Services.Command.CreateService
{
    public partial class CreateServiceCommand : IRequest<Response<Service>>
    {
        public string? Description { get; set; }
        public bool IsActive { get; set; }

    }
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Response<Service>>
    {
        private readonly IServiceRepositoryAsync _ServiceRepository;
        private readonly IMapper _mapper;
        public CreateServiceCommandHandler(IServiceRepositoryAsync ServiceRepository, IMapper mapper)
        {
            _ServiceRepository = ServiceRepository;
            _mapper = mapper;
        }

        public async Task<Response<Service>> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate = await _ServiceRepository.GetServiceInfo(request.Description);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("خدمات سفارش", ErrorType.DuplicateForCreate));

            var Service = _mapper.Map<Service>(request);
            await _ServiceRepository.AddAsync(Service);
            //throw new ApiException(new ErrorMessageFactory().MakeError("خدمات سفارش", ErrorType.NotFound);

            return new Response<Service>(Service, new ErrorMessageFactory().MakeError("خدمات سفارش", ErrorType.CreatedSuccess));
        }

   }
}