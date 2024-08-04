using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.RentPayments.Command.UpdateRentPayment
{
    public class UpdateRentPaymentCommand : IRequest<Response<string>>
    {
        public int ReceivePaymentOriginId { get; set; }
        public Guid Id { get; set; }
        /// <summary>
        /// شماره مجوز تخلیه
        /// </summary>
        public Guid? UnloadingPermitId { get; set; }
        /// <summary>
        /// شماره مجوز خروج اعلام بار
        /// </summary>
        public Guid? LadingExitPermitId { get; set; }
        public required decimal TotalFareAmount { get; set; }
        public string Description { get; set; } = string.Empty;

        public class UpdateRentPaymentCommandHandler : IRequestHandler<UpdateRentPaymentCommand, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
            public UpdateRentPaymentCommandHandler(IRentPaymentRepositoryAsync rentPaymentRepository, IMapper mapper)
            {
                _rentPaymentRepository = rentPaymentRepository;
                _mapper = mapper;
            }
            public async Task<Response<string>> Handle(UpdateRentPaymentCommand command, CancellationToken cancellationToken)
            {
                var rentPayments = _rentPaymentRepository.GetAllAsQueryable();

                if (rentPayments.Any(s => s.UnloadingPermitId == command.UnloadingPermitId 
                    || s.LadingExitPermitId==command.LadingExitPermitId))
                    throw new ApiException("کرایه با این مشخصات قبلا ایجاد شده است !");

                var rentPayment = await _rentPaymentRepository.GetByIdAsync(command.Id);
                rentPayment = _mapper.Map<UpdateRentPaymentCommand, RentPayment>(command, rentPayment);

                if (rentPayment == null)
                    throw new ApiException(new ErrorMessageFactory().MakeError("کرایه", ErrorType.NotFound));
                else
                {
                    await _rentPaymentRepository.UpdateAsync(rentPayment);
                    return new Response<string>(rentPayment.Id.ToString(), new ErrorMessageFactory().MakeError("کرایه", ErrorType.UpdatedSuccess));
                }
            }
        }
    }
}