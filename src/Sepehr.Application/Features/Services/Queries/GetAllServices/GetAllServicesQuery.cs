using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Serilog;
using Sepehr.Application.Features.Services.Queries.GetAllServices;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;
using Microsoft.Extensions.Logging;

namespace Sepehr.Application.Features.Services.Queries.GetAllServices
{
    public class GetAllServicesQuery : IRequest<PagedResponse<IEnumerable<ServiceViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllServicesQueryHandler :
         IRequestHandler<GetAllServicesQuery, PagedResponse<IEnumerable<ServiceViewModel>>>
    {
        private readonly IServiceRepositoryAsync _ServiceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllServicesQuery> _logger;
        public GetAllServicesQueryHandler(IServiceRepositoryAsync ServiceRepository,
            IMapper mapper, ILogger<GetAllServicesQuery> logger)
        {
            _ServiceRepository = ServiceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResponse<IEnumerable<ServiceViewModel>>> Handle(
            GetAllServicesQuery request,
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllServicesParameter>(request);
            var Service = await _ServiceRepository.GetAllAsync();

            var ServiceViewModel = _mapper.Map<IEnumerable<ServiceViewModel>>(Service);
            return new PagedResponse<IEnumerable<ServiceViewModel>>(
                ServiceViewModel.OrderByDescending(p => p.Id),
                validFilter.PageNumber,
                validFilter.PageSize);
        }
    }
}