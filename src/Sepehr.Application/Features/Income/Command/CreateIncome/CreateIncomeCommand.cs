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

namespace Sepehr.Application.Features.Incomes.Command.CreateIncome
{
    public partial class CreateIncomeCommand : IRequest<Response<Income>>
    {
        public string IncomeDescription { get; set; }
    }
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, Response<Income>>
    {
        private readonly IIncomeRepositoryAsync _incomeRepository;
        private readonly IMapper _mapper;
        public CreateIncomeCommandHandler(IIncomeRepositoryAsync incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }

        public async Task<Response<Income>> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate =await _incomeRepository.GetIncomeInfo(request.IncomeDescription);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("درآمد", ErrorType.DuplicateForCreate));

            var income = _mapper.Map<Income>(request);
            await _incomeRepository.AddAsync(income);

            return new Response<Income>(income, new ErrorMessageFactory().MakeError("درآمد", ErrorType.CreatedSuccess));
        }

    }
}