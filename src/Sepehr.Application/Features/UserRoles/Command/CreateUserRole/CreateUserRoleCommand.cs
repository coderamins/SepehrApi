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

namespace Sepehr.Application.Features.UserRoles.Command.CreateUserRole
{
    public partial class CreateUserRoleCommand : IRequest<Response<UserRole>>
    {
        public required Guid UserId { get; set; }
        public required Guid RoleId { get; set;}
    }
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, Response<UserRole>>
    {
        private readonly IUserRoleRepositoryAsync _userRoleRepository;
        private readonly IMapper _mapper;
        public CreateUserRoleCommandHandler(IUserRoleRepositoryAsync userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }        

        public async Task<Response<UserRole>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userRole = _mapper.Map<UserRole>(request);
                var checkDuplicate = await _userRoleRepository.GetUserRoleInfo(request.UserId,request.RoleId);
                if (checkDuplicate != null) { throw new ApiException("دسترسی با این مشخصات قبلا ایجاد است !"); }

                await _userRoleRepository.AddAsync(userRole);
                return new Response<UserRole>(userRole, "دسترسی جدید با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}