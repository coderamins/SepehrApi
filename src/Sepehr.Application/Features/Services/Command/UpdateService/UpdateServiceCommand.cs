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

namespace Sepehr.Application.Features.Services.Command.UpdateService
{
    public class UpdateServiceCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IServiceRepositoryAsync _ServiceRepository;
            public UpdateServiceCommandHandler(IServiceRepositoryAsync ServiceRepository, IMapper mapper)
            {
                _ServiceRepository = ServiceRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
            {
                var Service = await _ServiceRepository.GetByIdAsync(command.Id);
                Service = _mapper.Map<UpdateServiceCommand, Service>(command, Service);

                if (Service == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("خدمات سفارش", ErrorType.NotFound));

                else
                {
                    await _ServiceRepository.UpdateAsync(Service);
                    return new Response<string>(Service.Id.ToString(), new ErrorMessageFactory().MakeError("خدمات سفارش", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}