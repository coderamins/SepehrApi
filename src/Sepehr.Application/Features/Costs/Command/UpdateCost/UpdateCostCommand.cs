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

namespace Sepehr.Application.Features.Costs.Command.UpdateCost
{
    public class UpdateCostCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public required string CostDescription { get; set; }
        public bool IsActive { get; set; }

        public class UpdateCostCommandHandler : IRequestHandler<UpdateCostCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly ICostRepositoryAsync _costRepository;
            public UpdateCostCommandHandler(ICostRepositoryAsync costRepository, IMapper mapper)
            {
                _costRepository = costRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateCostCommand command, CancellationToken cancellationToken)
            {
                var cost = await _costRepository.GetByIdAsync(command.Id);
                cost = _mapper.Map<UpdateCostCommand, Cost>(command, cost);

                if (cost == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("هزینه", ErrorType.NotFound));
                else
                {
                    await _costRepository.UpdateAsync(cost);
                    return new Response<string>(cost.Id.ToString(), new ErrorMessageFactory().MakeError("هزینه", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}