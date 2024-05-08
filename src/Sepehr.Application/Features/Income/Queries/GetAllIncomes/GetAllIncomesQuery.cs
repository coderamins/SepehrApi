using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Features.Incomes.Queries.GetAllIncomes;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Incomes.Queries.GetAllIncomes
{
    public class GetAllIncomesQuery : IRequest<Response<IEnumerable<IncomeViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllIncomesQueryHandler :
         IRequestHandler<GetAllIncomesQuery, Response<IEnumerable<IncomeViewModel>>>
    {
        private readonly IIncomeRepositoryAsync _incomeRepository;
        private readonly IMapper _mapper;
        public GetAllIncomesQueryHandler(IIncomeRepositoryAsync incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<IncomeViewModel>>> Handle(
            GetAllIncomesQuery request, 
            CancellationToken cancellationToken)
        {
            try
            {
                var incomes = await _incomeRepository.GetAllAsync();                
                var incomeViewModel = _mapper.Map<IEnumerable<IncomeViewModel>>(incomes);
                return new Response<IEnumerable<IncomeViewModel>>(incomeViewModel);
            }
            catch (Exception e) 
            {

                throw;
            }
        }
    }
}