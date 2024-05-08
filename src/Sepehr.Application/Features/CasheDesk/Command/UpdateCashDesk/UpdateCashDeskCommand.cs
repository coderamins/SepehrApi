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

namespace Sepehr.Application.Features.CashDesks.Command.UpdateCashDesk
{
    public class UpdateCashDeskCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public required string CashDeskDescription { get; set; } 

        public class UpdateCashDeskCommandHandler : IRequestHandler<UpdateCashDeskCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly ICashDeskRepositoryAsync _cashDeskRepository;
            public UpdateCashDeskCommandHandler(ICashDeskRepositoryAsync cashDeskRepository, IMapper mapper)
            {
                _cashDeskRepository = cashDeskRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateCashDeskCommand command, CancellationToken cancellationToken)
            {
                var cashDesks = _cashDeskRepository.GetAllAsQueryable();

                if (cashDesks.Any(s => s.CashDeskDescription == command.CashDeskDescription && command.Id!=s.Id))
                    throw new ApiException("صندوق با این مشخصات قبلا ایجاد شده است !");

                var cashDesk = await _cashDeskRepository.GetByIdAsync(command.Id);
                cashDesk = _mapper.Map<UpdateCashDeskCommand, CashDesk>(command, cashDesk);

                if (cashDesk == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("صندوق", ErrorType.NotFound));
                else
                {
                    await _cashDeskRepository.UpdateAsync(cashDesk);
                    return new Response<string>(cashDesk.Id.ToString(), new ErrorMessageFactory().MakeError("صندوق", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}