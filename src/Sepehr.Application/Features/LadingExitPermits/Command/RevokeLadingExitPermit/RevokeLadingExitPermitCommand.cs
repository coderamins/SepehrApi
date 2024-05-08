using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.LadingPermit;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.LadingExitPermits.Command.RevokeLadingExitPermit
{
    public class RevokeLadingExitPermitCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public class RevokeLadingExitPermitCommandHandler : 
            IRequestHandler<RevokeLadingExitPermitCommand, Response<bool>>
        {
            private readonly IMapper _mapper;
            private readonly ILadingExitPermitRepositoryAsync _ladingExitPermitRepository;
            public RevokeLadingExitPermitCommandHandler(
                ILadingExitPermitRepositoryAsync ladingExitPermitRepository,
                IMapper mapper)
            {
                _ladingExitPermitRepository = ladingExitPermitRepository;
                _mapper = mapper;
            }
            public async Task<Response<bool>> Handle(RevokeLadingExitPermitCommand command, 
                CancellationToken cancellationToken)
            {
                var ladingExitPermit = await _ladingExitPermitRepository.GetByIdAsync(command.Id);
                if (ladingExitPermit == null)
                    throw new ApiException("مجوز خروج یافت نشد !");              

                if (ladingExitPermit == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("مجوز خروج", ErrorType.NotFound));
                else
                {   
                    ladingExitPermit = _mapper.Map(command, ladingExitPermit);
                    
                    ladingExitPermit.IsActive=false;
                    await _ladingExitPermitRepository.UpdateAsync(ladingExitPermit);

                    return new Response<bool>(true,"مجوز خروج با موفقیت ابطال شد !");
                }
            }
        }
    }
}