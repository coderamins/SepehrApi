using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Entities.UserEntities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Permissions.Command.CreatePermission
{
    public partial class CreatePermissionCommand : IRequest<Response<Permission>>
    {
        public required string Name { get; set; }
        public Guid ApplicationMenuId { get; set; }
        public string? Description { get; set; }

    }
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Response<Permission>>
    {
        private readonly IPermissionRepositoryAsync _permissionRepository;
        private readonly IMapper _mapper;
        public CreatePermissionCommandHandler(IPermissionRepositoryAsync permissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }        

        public async Task<Response<Permission>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var permission = _mapper.Map<Permission>(request);
                var checkDuplicate = await _permissionRepository.GetPermissionInfo(request.Name);
                if (checkDuplicate != null) { throw new ApiException("دسترسی با این مشخصات قبلا ایجاد است !"); }

                await _permissionRepository.AddAsync(permission);
                return new Response<Permission>(permission, "دسترسی جدید با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}