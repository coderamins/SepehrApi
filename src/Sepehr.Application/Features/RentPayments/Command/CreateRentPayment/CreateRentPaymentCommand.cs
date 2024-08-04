using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.RentPayment;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;
using Serilog;

namespace Sepehr.Application.Features.RentPayments.Command.CreateRentPayment
{
    public partial class CreateRentPaymentCommand : IRequest<Response<List<RentPayment>>>
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

        public required decimal TotalFareAmount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
    public class CreateRentPaymentCommandHandler : IRequestHandler<CreateRentPaymentCommand, Response<List<RentPayment>>>
    {
        private readonly IRentPaymentRepositoryAsync _rentPaymentRepository;
        private readonly ILadingExitPermitRepositoryAsync _ladingExitPermit;
        private readonly IPuOrderTransRemitUnloadPermitRepositoryAsync _puOrderUnloadPermitRepository;
        private readonly IMapper _mapper;
        public CreateRentPaymentCommandHandler(
            IRentPaymentRepositoryAsync rentPaymentRepository,
            IMapper mapper,
            ILadingExitPermitRepositoryAsync ladingExitPermit,
            IPuOrderTransRemitUnloadPermitRepositoryAsync puOrderUnloadPermitRepository)
        {
            _rentPaymentRepository = rentPaymentRepository;
            _mapper = mapper;
            _ladingExitPermit = ladingExitPermit;
            _puOrderUnloadPermitRepository = puOrderUnloadPermitRepository;
        }

        public async Task<Response<List<RentPayment>>> Handle(CreateRentPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                List<RentPaymentDto> rentPaymentDtos= new List<RentPaymentDto>();

                #region ثبت پرداخت کرایه خروج بار
                List<LadingExitPermit> ladingExitPermits = new List<LadingExitPermit>();
                foreach (var item in request.LadingExitPermitIds)
                {
                    var ladingExitPermit = await _ladingExitPermit.GetByIdAsync(item);
                    if (ladingExitPermit.FareAmountPayStatus)
                        throw new ApiException("این کرایه قبلا پرداخت شده است !");

                    ladingExitPermit.FareAmountPayStatus = true;
                    ladingExitPermits.Add(ladingExitPermit);

                    rentPaymentDtos.Add(new RentPaymentDto
                    {
                        ReceivePaymentOriginId = request.ReceivePaymentOriginId,
                        LadingExitPermitId = item,
                        TotalFareAmount = request.TotalFareAmount,
                        Description = request.Description,
                    });
                }
                #endregion

                #region ثبت پرداخت کرایه تخلیه بار
                List<UnloadingPermit> unloadingPermits = new List<UnloadingPermit>();
                foreach (var item in request.PuOrderTransRemittUnloadingPermitIds)
                {
                    var unloadingPermit = await _puOrderUnloadPermitRepository.GetByIdAsync(item);
                    if (unloadingPermit.FareAmountPayStatus)
                        throw new ApiException("این کرایه قبلا پرداخت شده است !");

                    unloadingPermit.FareAmountPayStatus = true;
                    unloadingPermits.Add(unloadingPermit);

                    rentPaymentDtos.Add(new RentPaymentDto
                    {
                        ReceivePaymentOriginId = request.ReceivePaymentOriginId,
                        TransferRemittanceUnloadingPermitId = item,
                        TotalFareAmount = request.TotalFareAmount,
                        Description = request.Description,
                    });
                }
                #endregion

                await _puOrderUnloadPermitRepository.UpdateAsync(unloadingPermits);
                await _ladingExitPermit.UpdateAsync(ladingExitPermits);

                var rentPayments = _mapper.Map<List<RentPayment>>(rentPaymentDtos);
                await _rentPaymentRepository.AddAsync(rentPayments);

                return new Response<List<RentPayment>>(rentPayments, 
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