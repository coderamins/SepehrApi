using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.ViewModels;

namespace Sepehr.Application.Features.Personnels.Queries.GetPersonnelById
{
    public class GetPersonnelByIdQuery : IRequest<Response<PersonnelViewModel>>
    {
        public Guid Id { get; set; }

        public class GetPersonnelByIdQueryHandler : IRequestHandler<GetPersonnelByIdQuery, Response<PersonnelViewModel>>
        {
            private readonly IPersonnelRepositoryAsync _personnelRepository;
            private readonly IMapper _mapper;

            public GetPersonnelByIdQueryHandler(
                IPersonnelRepositoryAsync personnelRepository,
                IMapper mapper
            )
            {
                _personnelRepository = personnelRepository;
                _mapper = mapper;
            }

            public async Task<Response<PersonnelViewModel>>
            Handle(
                GetPersonnelByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var personnel = await _personnelRepository.GetPersonnelInfo(query.Id);
                var personnelViewModel=_mapper.Map<PersonnelViewModel>(personnel);
                if (personnel == null)
                    throw new ApiException($"مشتری یافت نشد !");

                return new Response<PersonnelViewModel>(personnelViewModel);
            }
        }
    }
}
