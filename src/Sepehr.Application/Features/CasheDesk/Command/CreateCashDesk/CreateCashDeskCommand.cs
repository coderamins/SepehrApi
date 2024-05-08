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

namespace Sepehr.Application.Features.CashDesks.Command.CreateCashDesk
{
    public partial class CreateCashDeskCommand : IRequest<Response<CashDesk>>
    {
        public required string CashDeskDescription { get; set; }
    }
    public class CreateCashDeskCommandHandler : IRequestHandler<CreateCashDeskCommand, Response<CashDesk>>
    {
        private readonly ICashDeskRepositoryAsync _cashDeskRepository;
        private readonly IMapper _mapper;
        public CreateCashDeskCommandHandler(
            ICashDeskRepositoryAsync cashDeskRepository, 
            IMapper mapper)
        {
            _cashDeskRepository = cashDeskRepository;
            _mapper = mapper;
        }

        public async Task<Response<CashDesk>> Handle(CreateCashDeskCommand request, CancellationToken cancellationToken)
        {
            var cashDesks = _cashDeskRepository.GetAllAsQueryable();

            if (cashDesks.Any(s=>s.CashDeskDescription==request.CashDeskDescription))
                throw new ApiException(new ErrorMessageFactory().MakeError("صندوق", ErrorType.DuplicateForCreate));

            var pbrand = _mapper.Map<CashDesk>(request);
            await _cashDeskRepository.AddAsync(pbrand);

            return new Response<CashDesk>(pbrand, new ErrorMessageFactory().MakeError("صندوق", ErrorType.CreatedSuccess));
        }

    }
}