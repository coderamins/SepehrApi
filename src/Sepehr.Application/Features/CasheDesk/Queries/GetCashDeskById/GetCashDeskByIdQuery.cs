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

namespace Sepehr.Application.Features.CashDesks.Queries.GetCashDeskById
{
    public class GetCashDeskByIdQuery : IRequest<Response<CashDesk>>
    {
        public int Id { get; set; }

        public class GetCashDeskByIdQueryHandler : IRequestHandler<GetCashDeskByIdQuery, Response<CashDesk>>
        {
            private readonly ICashDeskRepositoryAsync _cashDeskRepository;

            public GetCashDeskByIdQueryHandler(
                ICashDeskRepositoryAsync cashDeskRepository
            )
            {
                _cashDeskRepository = cashDeskRepository;
            }

            public async Task<Response<CashDesk>>
            Handle(
                GetCashDeskByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var cashDesk = await _cashDeskRepository.GetByIdAsync(query.Id);
                if (cashDesk == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("صندوق",ErrorType.NotFound));
                return new Response<CashDesk>(cashDesk);
            }
        }
    }
}
