using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.PettyCashs.Command.UpdatePettyCash
{
    public class UpdatePettyCashCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        [StringLength(11)]
        public required string MobileNo { get; set; } 
        public required string PettyCashDescription { get; set; } 

        public class UpdatePettyCashCommandHandler : IRequestHandler<UpdatePettyCashCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPettyCashRepositoryAsync _PettyCashRepository;
            public UpdatePettyCashCommandHandler(IPettyCashRepositoryAsync PettyCashRepository, IMapper mapper)
            {
                _PettyCashRepository = PettyCashRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdatePettyCashCommand command, CancellationToken cancellationToken)
            {
                var PettyCashs = _PettyCashRepository.GetAllAsQueryable();

                if (PettyCashs.Any(s => s.MobileNo == command.MobileNo && command.Id!=s.Id))
                    throw new ApiException("تنخواه گردان با این شماره موبایل قبلا ایجاد شده است !");

                var PettyCash = await _PettyCashRepository.GetByIdAsync(command.Id);
                PettyCash = _mapper.Map<UpdatePettyCashCommand, PettyCash>(command, PettyCash);

                if (PettyCash == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("تنخواه گردان", ErrorType.NotFound));
                else
                {
                    await _PettyCashRepository.UpdateAsync(PettyCash);
                    return new Response<string>(PettyCash.Id.ToString(), new ErrorMessageFactory().MakeError("تنخواه گردان", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}