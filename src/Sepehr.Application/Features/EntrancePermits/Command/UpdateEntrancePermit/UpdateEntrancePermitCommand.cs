using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.Order;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.EntrancePermits.Command.UpdateEntrancePermit
{
    public class UpdateEntrancePermitCommand : IRequest<Response<EntrancePermit>>
    {
        public Guid Id { get; set; }


        public class UpdateEntrancePermitCommandHandler : IRequestHandler<UpdateEntrancePermitCommand, Response<EntrancePermit>>
        {
            private readonly IEntrancePermitRepositoryAsync __entrancePermitRepository;
            private readonly IMapper _mapper;

            public UpdateEntrancePermitCommandHandler(IEntrancePermitRepositoryAsync _entrancePermitRepository, IMapper mapper)
            {
                __entrancePermitRepository = _entrancePermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<EntrancePermit>> Handle(UpdateEntrancePermitCommand command, CancellationToken cancellationToken)
            {
                try
                {
                    var _entrancePermit = await __entrancePermitRepository.GetByIdAsync(command.Id);

                    if (_entrancePermit == null)
                    {
                        throw new ApiException($"مجوز ورود یاقت نشد !");
                    }
                    else
                    {
                        _entrancePermit = _mapper.Map(command, _entrancePermit);
                        _entrancePermit = await __entrancePermitRepository.UpdateEntrancePermit(_entrancePermit);

                        return new Response<EntrancePermit>(_entrancePermit, "");
                    }
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }
    }
}