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

namespace Sepehr.Application.Features.Incomes.Command.UpdateIncome
{
    public class UpdateIncomeCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public required string IncomeDescription { get; set; }
        public bool IsActive { get; set; }

        public class UpdateIncomeCommandHandler : IRequestHandler<UpdateIncomeCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IIncomeRepositoryAsync _incomeRepository;
            public UpdateIncomeCommandHandler(IIncomeRepositoryAsync incomeRepository, IMapper mapper)
            {
                _incomeRepository = incomeRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateIncomeCommand command, CancellationToken cancellationToken)
            {
                var income = await _incomeRepository.GetByIdAsync(command.Id);
                income = _mapper.Map<UpdateIncomeCommand, Income>(command, income);

                if (income == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درآمد", ErrorType.NotFound));
                else
                {
                    await _incomeRepository.UpdateAsync(income);
                    return new Response<string>(income.Id.ToString(), new ErrorMessageFactory().MakeError("درآمد", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}