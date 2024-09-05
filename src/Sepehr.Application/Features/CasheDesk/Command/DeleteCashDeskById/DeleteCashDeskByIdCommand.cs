using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.LogEntities;

namespace Sepehr.Application.Features.CashDesks.Command.DeleteCashDeskById
{
    public class DeleteCashDeskByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteCashDeskByIdCommandHandler
        : IRequestHandler<DeleteCashDeskByIdCommand, Response<bool>>
        {
            private readonly ICashDeskRepositoryAsync _cashDeskRepository;

            public DeleteCashDeskByIdCommandHandler(
                ICashDeskRepositoryAsync cashDeskRepository
                
            )
            {
                _cashDeskRepository = cashDeskRepository;
                
            }

            public async Task<Response<bool>>
            Handle(
                DeleteCashDeskByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var cashDesk = await _cashDeskRepository.GetByIdAsync(command.Id);
                if (cashDesk == null)
                    new ErrorMessageFactory().MakeError("صندوق", ErrorType.NotFound);

                await _cashDeskRepository.DeleteAsync(cashDesk);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("صندوق", ErrorType.DeletedSuccess));
            }
        }
    }
}
