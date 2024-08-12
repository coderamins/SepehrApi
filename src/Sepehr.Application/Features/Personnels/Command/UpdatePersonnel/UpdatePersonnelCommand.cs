using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Application.Features.Personnels.Command.CreatePersonnel;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Personnels.Command.UpdatePersonnel
{
    public class UpdatePersonnelCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public long PersonnelCode { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string FatherName { get; set; } = string.Empty;
        public string? OfficialName { get; set; }
        public string NationalId { get; set; } = string.Empty;
        public string NickName { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string Email { get; set; } = string.Empty;

        public IEnumerable<CreatePhonebookRequest>? Phonebook { get; set; }

        public class UpdatePersonnelCommandHandler : IRequestHandler<UpdatePersonnelCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelRepositoryAsync _personnelRepository;
            public UpdatePersonnelCommandHandler(IPersonnelRepositoryAsync personnelRepository, IMapper mapper)
            {
                _personnelRepository = personnelRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdatePersonnelCommand command, CancellationToken cancellationToken)
            {
                var personnel = await _personnelRepository.GetPersonnelInfo(command.Id);
                if (personnel == null)
                {
                    throw new ApiException($"مشتری یافت نشد !");
                }
                else
                {
                    if (personnel.Phonebook != null)
                        personnel.Phonebook.Clear();

                    var updated_personnel = _mapper.Map<UpdatePersonnelCommand, Personnel>(command, personnel);

                    await _personnelRepository.UpdatePersonnel(updated_personnel);
                    return new Response<string>(personnel.Id.ToString(), "");
                }
            }
        }
    }
}