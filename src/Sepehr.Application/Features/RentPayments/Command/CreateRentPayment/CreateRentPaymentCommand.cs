using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs;
using Sepehr.Application.DTOs.RentPayment;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Sepehr.Domain.Enums;
using Serilog;
using System.Text.Json.Serialization;

namespace Sepehr.Application.Features.RentPayments.Command.CreateRentPayment
{
    public partial class CreateRentPaymentCommand : IRequest<Response<RentPayment>>
    {
        public int ReceivePaymentOriginId { get; set; }
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
        public List<AttachmentDto> Attachments { get; set; }
    }
    public class CreateRentPaymentCommandHandler : IRequestHandler<CreateRentPaymentCommand, Response<RentPayment>>
    {
        private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
        private readonly ILadingExitPermitRepositoryAsync _ladingExitPermit;
        private readonly IUnloadingPermitRepositoryAsync _unloadingPermitRepo;
        private readonly IMapper _mapper;
        public CreateRentPaymentCommandHandler(
            IRentPaymentRepositoryAsync rentPaymentRepository,
            IMapper mapper,
            ILadingExitPermitRepositoryAsync ladingExitPermit,
            IUnloadingPermitRepositoryAsync unloadPermitRepository)
        {
            _rentPaymentRepository = rentPaymentRepository;
            _mapper = mapper;
            _ladingExitPermit = ladingExitPermit;
            _unloadingPermitRepo = unloadPermitRepository;
        }

        public async Task<Response<RentPayment>> Handle(CreateRentPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<RentPaymentDetailDto> rentPaymentDtos= new List<RentPaymentDetailDto>();

                #region ثبت پرداخت کرایه خروج بار
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

                    rentPaymentDtos.Add(new RentPaymentDetailDto
                    {
                        LadingExitPermitId = item,
                    });
                }
                #endregion

                #region ثبت پرداخت کرایه تخلیه بار
                foreach (var item in request.PuOrderTransRemittUnloadingPermitIds)
                {
                    var unloadingPermit = await _unloadingPermitRepo.GetByIdAsync(item);
                    if (unloadingPermit == null)
                        throw new ApiException("کرایه یافت نشد !");

                    if (unloadingPermit.FareAmountStatusId==(int)EFareAmountStatus.InProgress)
                        throw new ApiException("کرایه تایید نشده است !");

                    if (unloadingPermit.FareAmountStatusId==(int)EFareAmountStatus.Payed)
                        throw new ApiException("این کرایه قبلا پرداخت شده است !");

                    if (_unloadingPermitRepo.CheckFarePaymentType(unloadingPermit.Id) != EFarePaymentType.FareByOurselves)
                        throw new ApiException("پرداخت کرایه بر عهده مشتری می باشد !");

                    rentPaymentDtos.Add(new RentPaymentDetailDto
                    {
                        UnloadingPermitId=item,
                    });
                }
                #endregion

                request.RentPaymentDetails= rentPaymentDtos;
                var rentPayment= _mapper.Map<RentPayment>(request);

                //var rentPaymentDetails = _mapper.Map<List<RentPaymentDetail>>(rentPaymentDtos);
                

                await _rentPaymentRepository.CreateRentPayment(rentPayment);

                return new Response<RentPayment>(rentPayment, 
                    new ErrorMessageFactory()
                    .MakeError("کرایه", ErrorType.CreatedSuccess));
            }
            catch (Exception e)
            {
                Log.Write(Serilog.Events.LogEventLevel.Error, e.Message ?? e.InnerException.Message);
                throw;
            }
        }

    }
}