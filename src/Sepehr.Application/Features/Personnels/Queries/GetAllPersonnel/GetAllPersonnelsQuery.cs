using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Personnels.Queries.GetAllPersonnels
{
    public class GetAllPersonnelsQuery : IRequest<PagedResponse<IEnumerable<PersonnelViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? PersonnelCode { get; set; }
        public string? PersonnelName { get; set; } 
        public string? PhoneNumber { get; set; } 
        public string? NationalCode { get; set; } 
    }
    public class GetAllPersonnelQueryHandler :
         IRequestHandler<GetAllPersonnelsQuery, PagedResponse<IEnumerable<PersonnelViewModel>>>
    {
        private readonly IPersonnelRepositoryAsync _personnelRepository;
        private readonly IMapper _mapper;
        public GetAllPersonnelQueryHandler(IPersonnelRepositoryAsync personnelRepository, IMapper mapper)
        {
            _personnelRepository = personnelRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<PersonnelViewModel>>> Handle(
            GetAllPersonnelsQuery request, 
            CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllPersonnelsParameter>(request);
            var personnel = await _personnelRepository.GetAllPersonnels(validFilter);
              
            var personnelViewModel = _mapper.Map<IEnumerable<PersonnelViewModel>>(personnel);
            return new PagedResponse<IEnumerable<PersonnelViewModel>>(
                personnelViewModel, 
                validFilter.PageNumber, 
                validFilter.PageSize);
        }
    }
}