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

namespace Sepehr.Application.Features.Incomes.Queries.GetIncomeById
{
    public class GetIncomeByIdQuery : IRequest<Response<Income>>
    {
        public Guid Id { get; set; }

        public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQuery, Response<Income>>
        {
            private readonly IIncomeRepositoryAsync _incomeRepository;

            public GetIncomeByIdQueryHandler(
                IIncomeRepositoryAsync incomeRepository
            )
            {
                _incomeRepository = incomeRepository;
            }

            public async Task<Response<Income>>
            Handle(
                GetIncomeByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var income = await _incomeRepository.GetByIdAsync(query.Id);
                if (income == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درآمد",ErrorType.NotFound));
                return new Response<Income>(income);
            }
        }
    }
}
