using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sepehr.Application.DTOs.LadingPermit;
using Sepehr.Application.DTOs.LadingPermits;
using Sepehr.Application.Exceptions;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Application.Wrappers;
using Sepehr.Domain.Common;
using Sepehr.Domain.Entities;

namespace Sepehr.Application.Features.DriverFareAmounts
{
    public partial class ApprovePurOrderTransRemittFareAmountCommand : IRequest<Response<DriverFareAmountApprove>>
    {
        public Guid PurOrderTransRemittUnloadingPermitId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
    public class ApprovePurOrderTransRemittFareAmountCommandHandler :
        IRequestHandler<ApprovePurOrderTransRemittFareAmountCommand, Response<DriverFareAmountApprove>>
    {
        private readonly IUnloadingPermitRepositoryAsync _puOrderTransRemitUnload;
        private readonly IDriverFareAmountApproveRepositoryAsync _driverFareAmountApprove;
        private readonly IMapper _mapper;
        public ApprovePurOrderTransRemittFareAmountCommandHandler(
            IUnloadingPermitRepositoryAsync puOrderTransRemitUnload,
            IDriverFareAmountApproveRepositoryAsync driverFareAmountApprove,
            IMapper mapper)
        {
            _puOrderTransRemitUnload = puOrderTransRemitUnload;
            _driverFareAmountApprove = driverFareAmountApprove;
            _mapper = mapper;
        }

        public async Task<Response<DriverFareAmountApprove>> Handle(ApprovePurOrderTransRemittFareAmountCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                var transferRemittUnloadPermit = await _puOrderTransRemitUnload.GetByIdAsync(request.PurOrderTransRemittUnloadingPermitId);
                if (transferRemittUnloadPermit == null)
                {
                    throw new ApiException("مجوز تخلیه یافت نشد !");
                }

                var driverFareAmount = _mapper.Map<DriverFareAmountApprove>(request);

                driverFareAmount = await _driverFareAmountApprove.AddAsync(driverFareAmount);

                if (transferRemittUnloadPermit.FareAmountApproved)
                    throw new ApiException("کرایه قبلا تایید شده است !");

                transferRemittUnloadPermit.FareAmountApproved = true;
                await _puOrderTransRemitUnload.UpdateAsync(transferRemittUnloadPermit);

                return new Response<DriverFareAmountApprove>(driverFareAmount,
                    "تایید کرایه با موفقیت انجام شد !");
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}