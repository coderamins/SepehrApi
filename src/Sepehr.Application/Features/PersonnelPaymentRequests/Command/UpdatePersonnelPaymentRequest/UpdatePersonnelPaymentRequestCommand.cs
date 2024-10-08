﻿using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.PersonnelPaymentRequests.Command.UpdatePersonnelPaymentRequest
{
    public class UpdatePersonnelPaymentRequestCommand :PaymentRequestDto, IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public Guid PersonnelId { get; set; }

        public class UpdatePersonnelPaymentRequestCommandHandler : IRequestHandler<UpdatePersonnelPaymentRequestCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IPersonnelPaymentRequestRepositoryAsync _personnelPaymentRequestRepository;
            private readonly IAuthenticatedUserService _userService;
            public UpdatePersonnelPaymentRequestCommandHandler(
                IPersonnelPaymentRequestRepositoryAsync personnelPaymentRequestRepository,
                IAuthenticatedUserService userService,
                IMapper mapper)
            {
                _personnelPaymentRequestRepository = personnelPaymentRequestRepository;
                _mapper = mapper;
                _userService = userService; 
            }
            public async Task<Response<string>> Handle(UpdatePersonnelPaymentRequestCommand command, CancellationToken cancellationToken)
            {
                var personnelPaymentRequests = _personnelPaymentRequestRepository.GetAllAsQueryable();

                var personnelPaymentRequest = await _personnelPaymentRequestRepository.GetByIdAsync(command.Id);
                personnelPaymentRequest = _mapper.Map<UpdatePersonnelPaymentRequestCommand, PersonnelPaymentRequest>(command, personnelPaymentRequest);

                if (personnelPaymentRequest == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.NotFound));
                else
                {
                    personnelPaymentRequest.ApproverId =Guid.Parse(_userService.UserId);
                    await _personnelPaymentRequestRepository.UpdatePaymentRequestAsync(personnelPaymentRequest);
                    return new Response<string>(personnelPaymentRequest.Id.ToString(), new ErrorMessageFactory().MakeError("درخواست پرداخت", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}