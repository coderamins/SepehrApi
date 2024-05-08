using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace Sepehr.Application.Features.PettyCashs.Command.CreatePettyCash
{
    public partial class CreatePettyCashCommand : IRequest<Response<PettyCash>>
    {
        public required string MobileNo { get; set; }
        public required string PettyCashDescription { get; set; }
    }
    public class CreatePettyCashCommandHandler : IRequestHandler<CreatePettyCashCommand, Response<PettyCash>>
    {
        private readonly IPettyCashRepositoryAsync _PettyCashRepository;
        private readonly IMapper _mapper;
        public CreatePettyCashCommandHandler(
            IPettyCashRepositoryAsync PettyCashRepository, 
            IMapper mapper)
        {
            _PettyCashRepository = PettyCashRepository;
            _mapper = mapper;
        }

        public async Task<Response<PettyCash>> Handle(CreatePettyCashCommand request, CancellationToken cancellationToken)
        {
            var PettyCashs = _PettyCashRepository.GetAllAsQueryable();

            if (PettyCashs.Any(s=>s.MobileNo==request.MobileNo))
                throw new ApiException(new ErrorMessageFactory().MakeError("تنخواه گردان", ErrorType.DuplicateForCreate));

            var pbrand = _mapper.Map<PettyCash>(request);
            await _PettyCashRepository.AddAsync(pbrand);

            return new Response<PettyCash>(pbrand, new ErrorMessageFactory().MakeError("تنخواه گردان", ErrorType.CreatedSuccess));
        }

    }
}