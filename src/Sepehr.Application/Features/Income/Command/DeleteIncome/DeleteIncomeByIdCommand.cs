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

namespace Sepehr.Application.Features.Incomes.Command.DeleteIncomeById
{
    public class DeleteIncomeByIdCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public class
        DeleteIncomeByIdCommandHandler
        : IRequestHandler<DeleteIncomeByIdCommand, Response<bool>>
        {
            private readonly IIncomeRepositoryAsync _incomeRepository;
            

            public DeleteIncomeByIdCommandHandler(
                IIncomeRepositoryAsync incomeRepository
                
            )
            {
                _incomeRepository = incomeRepository;
                
            }

            public async Task<Response<bool>>
            Handle(
                DeleteIncomeByIdCommand command,
                CancellationToken cancellationToken
            )
            {
                var income = await _incomeRepository.GetByIdAsync(command.Id);
                if (income == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درآمد", ErrorType.NotFound));

                await _incomeRepository.DeleteAsync(income);
                return new Response<bool>(true, new ErrorMessageFactory().MakeError("درآمد", ErrorType.DeletedSuccess));
            }
        }
    }
}
