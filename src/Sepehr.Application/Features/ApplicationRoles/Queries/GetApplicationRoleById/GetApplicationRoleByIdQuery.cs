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

namespace Sepehr.Application.Features.ApplicationRoles.Queries.GetApplicationRoleById
{
    public class GetApplicationRoleByIdQuery : IRequest<Response<ApplicationRoleViewModel>>
    {
        public string Id { get; set; }

        public class GetApplicationRoleByIdQueryHandler : IRequestHandler<GetApplicationRoleByIdQuery, Response<ApplicationRoleViewModel>>
        {
            private readonly IApplicationRoleRepositoryAsync _applicationRoleRepository;
            private readonly IMapper _mapper;

            public GetApplicationRoleByIdQueryHandler(
                IApplicationRoleRepositoryAsync applicationRoleRepository,
                IMapper mapper
            )
            {
                _applicationRoleRepository = applicationRoleRepository;
                _mapper = mapper;
            }

            public async Task<Response<ApplicationRoleViewModel>>
            Handle(
                GetApplicationRoleByIdQuery query,
                CancellationToken cancellationToken
            )
            {
                var applicationRole = await _applicationRoleRepository.GetApplicationRoleInfo(query.Id);
                var applicationRoleViewModel=_mapper.Map<ApplicationRoleViewModel>(applicationRole);
                if (applicationRole == null)
                    throw new ApiException($"نقش یافت نشد !");

                return new Response<ApplicationRoleViewModel>(applicationRoleViewModel);
            }
        }
    }
}
