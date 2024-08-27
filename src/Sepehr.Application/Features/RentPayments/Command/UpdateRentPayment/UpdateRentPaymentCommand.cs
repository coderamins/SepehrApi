using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core;
using MediatR;
using Sepehr.Application.DTOs.RentPayment;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Serilog;

namespace Sepehr.Application.Features.RentPayments.Command.UpdateRentPayment
{
    public class UpdateRentPaymentCommand : IRequest<Response<RentPayment>>
    {
        public int Id { get; set; }
        /// <summary>
        /// شماره مجوز تخلیه
        /// </summary>
        public IEnumerable<Guid> PuOrderTransRemittUnloadingPermitIds { get; set; } = new List<Guid>();
        /// <summary>
        /// شماره مجوز خروج اعلام بار
        /// </summary>
        public IEnumerable<Guid> LadingExitPermitIds { get; set; } = new List<Guid>();

        /// <summary>
        /// نوع مبدا پرداخت
        /// </summary>
        public required int PaymentOriginTypeId { get; set; }
        /// <summary>
        /// شناسه مبدا پرداخت
        /// </summary>
        public required string PaymentOriginId { get; set; }


        public required decimal TotalFareAmount { get; set; }
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public List<RentPaymentDetailDto> RentPaymentDetails { get; set; }

        public class UpdateRentPaymentCommandHandler : IRequestHandler<UpdateRentPaymentCommand, Response<RentPayment>>
        {
            private readonly IMapper _mapper;
            private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
            private readonly IUnloadingPermitRepositoryAsync _unloadingPermitRepo;
            private readonly ILadingExitPermitRepositoryAsync _ladingExitPermit;
            public UpdateRentPaymentCommandHandler(
                IRentPaymentRepositoryAsync rentPaymentRepository,
                ILadingExitPermitRepositoryAsync ladingExitPermitRepository,
                IMapper mapper,
                IUnloadingPermitRepositoryAsync unloadingPermitRepo)
            {
                _rentPaymentRepository = rentPaymentRepository;
                _ladingExitPermit = ladingExitPermitRepository;
                _mapper = mapper;
                _unloadingPermitRepo = unloadingPermitRepo;
            }
            public async Task<Response<RentPayment>> Handle(UpdateRentPaymentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    List<RentPaymentDetailDto> rentPaymentDtos = new List<RentPaymentDetailDto>();

                    #region ثبت پرداخت کرایه خروج بار
                    List<LadingExitPermit> ladingExitPermits = new List<LadingExitPermit>();
                    foreach (var item in request.LadingExitPermitIds)
                    {
                        var ladingExitPermit = await _ladingExitPermit.GetByIdAsync(item);
                        if (ladingExitPermit == null)
                            throw new ApiException("کرایه یافت نشد !");

                        //if (_ladingExitPermit.CheckFarePaymentType(ladingExitPermit.Id) != EFarePaymentType.FareByOurselves)
                        //    throw new ApiException("پرداخت کرایه بر عهده مشتری می باشد !");

                        //if (ladingExitPermit.FareAmountStatusId==(int)EFareAmountStatus.InProgress)
                        //    throw new ApiException("کرایه تایید نشده است !");

                        //if (ladingExitPermit.FareAmountStatusId== (int)EFareAmountStatus.Payed)
                        //    throw new ApiException("این کرایه قبلا پرداخت شده است !");

                        //ladingExitPermit.FareAmountStatusId = (int)EFareAmountStatus.Payed;
                        ladingExitPermits.Add(ladingExitPermit);

                        rentPaymentDtos.Add(new RentPaymentDetailDto
                        {
                            LadingExitPermitId = item,
                        });
                    }
                    #endregion

                    #region ثبت پرداخت کرایه تخلیه بار
                    List<UnloadingPermit> unloadingPermits = new List<UnloadingPermit>();
                    foreach (var item in request.PuOrderTransRemittUnloadingPermitIds)
                    {
                        var unloadingPermit = await _unloadingPermitRepo.GetByIdAsync(item);
                        if (unloadingPermit == null)
                            throw new ApiException("کرایه یافت نشد !");

                        if (unloadingPermit.FareAmountStatusId == (int)EFareAmountStatus.InProgress)
                            throw new ApiException("کرایه تایید نشده است !");

                        if (unloadingPermit.FareAmountStatusId == (int)EFareAmountStatus.Payed)
                            throw new ApiException("این کرایه قبلا پرداخت شده است !");

                        if (_unloadingPermitRepo.CheckFarePaymentType(unloadingPermit.Id) != EFarePaymentType.FareByOurselves)
                            throw new ApiException("پرداخت کرایه بر عهده مشتری می باشد !");

                        unloadingPermit.FareAmountStatusId = (int)EFareAmountStatus.Payed;
                        unloadingPermits.Add(unloadingPermit);

                        rentPaymentDtos.Add(new RentPaymentDetailDto
                        {
                            UnloadingPermitId = item,
                        });
                    }
                    #endregion

                    await _unloadingPermitRepo.UpdateAsync(unloadingPermits);
                    await _ladingExitPermit.UpdateAsync(ladingExitPermits);

                    request.RentPaymentDetails = rentPaymentDtos;
                    var rentPayment = _mapper.Map<RentPayment>(request);

                    //var rentPaymentDetails = _mapper.Map<List<RentPaymentDetail>>(rentPaymentDtos);


                    await _rentPaymentRepository.UpdateRentPayment(rentPayment);

                    return new Response<RentPayment>(rentPayment,
                        new ErrorMessageFactory()
                        .MakeError("کرایه", ErrorType.UpdatedSuccess));
                }
                catch (Exception e)
                {
                    Log.Write(Serilog.Events.LogEventLevel.Error, e.Message ?? e.InnerException.Message);
                    throw;
                }
            }
        }
    }
}