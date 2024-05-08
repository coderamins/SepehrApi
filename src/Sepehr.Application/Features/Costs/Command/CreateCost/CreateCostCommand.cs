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

namespace Sepehr.Application.Features.Costs.Command.CreateCost
{
    public partial class CreateCostCommand : IRequest<Response<Cost>>
    {
        public required string CostDescription { get; set; }
    }
    public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, Response<Cost>>
    {
        private readonly ICostRepositoryAsync _costRepository;
        private readonly IMapper _mapper;
        public CreateCostCommandHandler(ICostRepositoryAsync costRepository, IMapper mapper)
        {
            _costRepository = costRepository;
            _mapper = mapper;
        }

        public async Task<Response<Cost>> Handle(CreateCostCommand request, CancellationToken cancellationToken)
        {
            var checkDuplicate =await _costRepository.GetCostInfo(request.CostDescription);
            if (checkDuplicate != null)
                throw new ApiException(new ErrorMessageFactory().MakeError("هزینه", ErrorType.DuplicateForCreate));

            var cost = _mapper.Map<Cost>(request);
            await _costRepository.AddAsync(cost);

            return new Response<Cost>(cost, new ErrorMessageFactory().MakeError("هزینه", ErrorType.CreatedSuccess));
        }

    }
}