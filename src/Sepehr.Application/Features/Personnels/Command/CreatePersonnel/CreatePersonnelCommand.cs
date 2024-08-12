using AutoMapper;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Features.Customers.Command.CreateCustomer;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Personnels.Command.CreatePersonnel
{
    public partial class CreatePersonnelCommand : IRequest<Response<Personnel>>
    {
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
    }
    public class CreatePersonnelCommandHandler : IRequestHandler<CreatePersonnelCommand, Response<Personnel>>
    {
        private readonly IPersonnelRepositoryAsync _personnelRepository;
        private readonly IMapper _mapper;
        public CreatePersonnelCommandHandler(IPersonnelRepositoryAsync personnelRepository, IMapper mapper)
        {
            _personnelRepository = personnelRepository;
            _mapper = mapper;
        }        

        public async Task<Response<Personnel>> Handle(CreatePersonnelCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var personnel = _mapper.Map<Personnel>(request);
                var checkDuplicate = await _personnelRepository.GetPersonnelInfo(request.NationalId);
                if (checkDuplicate != null) { throw new ApiException("پرسنل با این کد ملی قبلا ثبت شده است !"); }

                await _personnelRepository.AddAsync(personnel);
                return new Response<Personnel>(personnel, "اطلاعات پرسنل جدید با موفقیت ایجاد گردید .");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}