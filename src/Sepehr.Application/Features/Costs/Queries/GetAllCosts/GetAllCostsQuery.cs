using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.Costs.Queries.GetAllCosts;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Costs.Queries.GetAllCosts
{
    public class GetAllCostsQuery : IRequest<Response<IEnumerable<CostViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllCostsQueryHandler :
         IRequestHandler<GetAllCostsQuery, Response<IEnumerable<CostViewModel>>>
    {
        private readonly ICostRepositoryAsync _costRepository;
        private readonly IMapper _mapper;
        public GetAllCostsQueryHandler(ICostRepositoryAsync costRepository, IMapper mapper)
        {
            _costRepository = costRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CostViewModel>>> Handle(
            GetAllCostsQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var costs = await _costRepository.GetAllAsync();                
                var costViewModel = _mapper.Map<IEnumerable<CostViewModel>>(costs);
                return new Response<IEnumerable<CostViewModel>>(costViewModel);
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}